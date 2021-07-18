using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TweetBook.Domain;
using static TweetBook.Routes.V1.ApiRoutes;

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
    }
}
