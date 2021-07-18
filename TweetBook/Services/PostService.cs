using System;
using System.Collections.Generic;
using System.Linq;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class PostService : IPostService
    {
        private readonly List<Post> _posts;
        public PostService()
        {
            _posts = new List<Post>();
            for (int i = 0; i < 5; i++)
            {
                _posts.Add(new Post
                {
                    PostId = Guid.NewGuid(),
                    Name = $"The is post number {i}"
                });
            }
        }
        public List<Post> GetAllPosts()
        {
            return _posts;
        }

        public Post GetPostById(Guid postId)
        {
            return _posts.FirstOrDefault(x => x.PostId == postId);
        }
    }
}
