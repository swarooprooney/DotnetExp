using System;
using System.Collections.Generic;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public interface IPostService
    {
        public List<Post> GetAllPosts();

        public Post GetPostById(Guid postId);

        public bool UpdatePost(Post postToBeUpdated);
    }
}
