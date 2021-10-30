using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts.V1.Request.Queries;
using Tweetbook.Contracts.V1.Response;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;
using TweetBook.Domain;
using TweetBook.Filters;
using TweetBook.GeneralExtensions;
using TweetBook.Helpers;
using TweetBook.Services;
using static TweetBook.Contracts.V1.ApiRoutes;

namespace TweetBook.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Produces("application/json")]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IPostUriService _uriService;
        public PostsController(IPostService postService, IMapper mapper, IPostUriService uriService)
        {
            _postService = postService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(Posts.GetPosts)]
        [ProducesResponseType(typeof(IEnumerable<PostResponse>), 200)]
        //[Cached(600)]
        public async Task<IActionResult> GetPosts([FromQuery] PaginationQuery pagination)
        {
            var paginationFilter = _mapper.Map<PaginationFilter>(pagination);
            var posts = await _postService.GetAllPostsAsync(paginationFilter);
            var postsResponse = _mapper.Map<IEnumerable<PostResponse>>(posts);
            if (paginationFilter is null || paginationFilter.PageNumber < 1 || paginationFilter.PageSize < 1)
            {
                var pagedResponse = new PagedResponse<PostResponse>(postsResponse);
                return Ok(pagedResponse);
            }

            var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_uriService, paginationFilter, postsResponse);
        
            return Ok(paginationResponse);
        }

        [HttpGet(Posts.Get)]
        [ProducesResponseType(typeof(PostResponse), 200)]
        //[Cached(600)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);
            var postResponse = new Response<PostResponse>(_mapper.Map<PostResponse>(post));
            return Ok(postResponse);
        }

        [HttpPut(Posts.Update)]
        [ProducesResponseType(typeof(PostResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 403)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePost postToBeUpdated)
        {
            var isOwner = await _postService.CheckIfUserIsOwnerOfPostAsync(HttpContext.GetUserId(), postId);
            if (!isOwner)
            {
                return StatusCode(403, CreateError("You are not the owner of the post"));
            }
            var post = await _postService.GetPostByIdAsync(postId);
            post.Name = postToBeUpdated.Name;
            if (await _postService.UpdatePostAsync(post))
            {
                return Ok(_mapper.Map<PostResponse>(post));
            }
            return NotFound(CreateError("An error occured while updating the post, please try again later"));
        }

        [HttpPost(Posts.CreatePost)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest createPost)
        {
            var post = new Post
            {
                PostId = Guid.NewGuid(),
                Name = createPost.Name,
                UserId = HttpContext.GetUserId()
            };
            await _postService.CreatePostAsync(post);
            var locationUrl = _uriService.GetEntityUri(post.PostId.ToString());
            var response = new CreatePostResponse { PostId = post.PostId };
            return Created(locationUrl, response);
        }

        [HttpDelete(Posts.Delete)]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 403)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var isOwner = await _postService.CheckIfUserIsOwnerOfPostAsync(HttpContext.GetUserId(), postId);
            if (!isOwner)
            {
                return StatusCode(403, CreateError("You are not the owner of the post"));
            }
            if (await _postService.DeletePostAsync(postId))
            {
                return NoContent();
            }
            return NotFound(CreateError("Cannot find the post"));
        }

        private ErrorResponse CreateError(string message)
        {
            return new ErrorResponse
            {
                Errors = new List<ErrorModel>
                {
                    new ErrorModel
                    {
                        Message = message
                    }
                }
            };
        }
    }
}
