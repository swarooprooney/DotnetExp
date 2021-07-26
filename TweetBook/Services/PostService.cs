using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Data;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _dataContext;

        public PostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            await _dataContext.Posts.AddAsync(post);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _dataContext.Posts.ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            return await _dataContext.Posts.SingleOrDefaultAsync(x => x.PostId == postId);
        }

        public async Task<bool> UpdatePostAsync(Post postToBeUpdated)
        {
            var existingPost = await GetPostByIdAsync(postToBeUpdated.PostId);
            if (existingPost == null)
            {
                return false;
            }
            _dataContext.Posts.Update(postToBeUpdated);
            var result = await _dataContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var existingPost = await GetPostByIdAsync(postId);
            if (existingPost == null)
            {
                return false;
            }
            _dataContext.Posts.Remove(existingPost);
            var result = await _dataContext.SaveChangesAsync();
            return result > 0;
        }


    }
}
