using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;
using TweetBook.Domain;
using TweetBook.Services;
using static TweetBook.Contracts.V1.ApiRoutes;

namespace TweetBook.Controllers.V1
{

    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(Posts.GetPosts)]
        public IActionResult GetPosts()
        {
            return Ok(_postService.GetAllPosts());
        }

        [HttpGet(Posts.Get)]
        public IActionResult Get([FromRoute] Guid postId)
        {
            return Ok(_postService.GetPostById(postId));
        }

        [HttpPut(Posts.Update)]
        public IActionResult Update([FromRoute] Guid postId,[FromBody] UpdatePost postToBeUpdated)
        {
            var post = new Post { PostId = postId, Name = postToBeUpdated.Name };
            if(_postService.UpdatePost(post))
            {
                return Ok(post);
            }
            return NotFound();
        }

        [HttpPost(Posts.CreatePost)]
        public IActionResult Create([FromBody]CreatePostRequest createPost)
        {
            if (createPost.PostId == Guid.Empty)
            {
                createPost.PostId = Guid.NewGuid();
            }
            var post = new Post { PostId = createPost.PostId };
            _postService.GetAllPosts().Add(post);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + Posts.Get.Replace("{postId}", post.PostId.ToString());
            var response = new CreatePostResponse { PostId = post.PostId };
            return Created(locationUrl, response);
        }

        [HttpDelete(Posts.Delete)]
        public IActionResult Delete([FromRoute] Guid postId)
        {
            if(_postService.DeletePost(postId))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
