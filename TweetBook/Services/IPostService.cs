using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public interface IPostService
    {
        public Task<bool> CreatePostAsync(Post post);

        public Task<List<Post>> GetAllPostsAsync(PaginationFilter paginationFilter);

        public Task<Post> GetPostByIdAsync(Guid postId);

        public Task<bool> UpdatePostAsync(Post postToBeUpdated);

        public Task<bool> DeletePostAsync(Guid postId);

        public Task<bool> CheckIfUserIsOwnerOfPostAsync(string userID, Guid postID);
    }
}
