using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;
using TweetBook.Domain;
using TweetBook.GeneralExtensions;
using TweetBook.Services;
using static TweetBook.Contracts.V1.ApiRoutes;

namespace TweetBook.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(Posts.GetPosts)]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await _postService.GetAllPostsAsync());
        }

        [HttpGet(Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            return Ok(await _postService.GetPostByIdAsync(postId));
        }

        [HttpPut(Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePost postToBeUpdated)
        {
            var isOwner = await _postService.CheckIfUserIsOwnerOfPostAsync(HttpContext.GetUserId(), postId);
            if (!isOwner)
            {
                return StatusCode(403, new { error = "You are not the owner of the post" });
            }
            var post = await _postService.GetPostByIdAsync(postId);
            post.Name = postToBeUpdated.Name;
            if (await _postService.UpdatePostAsync(post))
            {
                return Ok(post);
            }
            return NotFound();
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
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + Posts.Get.Replace("{postId}", post.PostId.ToString());
            var response = new CreatePostResponse { PostId = post.PostId };
            return Created(locationUrl, response);
        }

        [HttpDelete(Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var isOwner = await _postService.CheckIfUserIsOwnerOfPostAsync(HttpContext.GetUserId(), postId);
            if (!isOwner)
            {
                return StatusCode(403, new { error = "You are not the owner of the post" });
            }
            if (await _postService.DeletePostAsync(postId))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
