using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TweetBook.Contracts.V1;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;
using TweetBook.Domain;
using static TweetBook.Contracts.V1.ApiRoutes;

namespace TweetBook.Controllers.V1
{

    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly List<Post> _posts;
        public PostsController()
        {
            _posts = new List<Post>();
            for (int i = 0; i < 5; i++)
            {
                _posts.Add(new Post { PostId = Guid.NewGuid().ToString() });
            }
        }
        
        [HttpGet(Posts.GetPosts)]
        public IActionResult GetPosts()
        {
            return Ok(_posts);
        }

        [HttpPost(Posts.CreatePost)]
        public IActionResult Create([FromBody]CreatePostRequest createPost)
        {
            if (string.IsNullOrEmpty(createPost.PostId))
            {
                createPost.PostId = Guid.NewGuid().ToString();
            }
            var post = new Post { PostId = createPost.PostId };
            _posts.Add(post);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + Posts.Get.Replace("{postId}", post.PostId);
            var response = new CreatePostResponse { PostId = post.PostId };
            return Created(locationUrl, response);
        }
    }
}
