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

        public async Task<List<Post>> GetAllPostsAsync(GetPostsFilter getPostsFilter = null, PaginationFilter paginationFilter = null)
        {
            var query = _dataContext.Posts.AsQueryable();

            query = ApplyFilters(getPostsFilter, query);

            if (paginationFilter is null)
            {
                return await query.Include(u => u.User)
                .ToListAsync();
            }
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await query.Include(u => u.User)
                .Skip(skip)
                .Take(paginationFilter.PageSize)
                .ToListAsync();
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

        public async Task<bool> CheckIfUserIsOwnerOfPostAsync(string userID, Guid postID)
        {
            var post = await _dataContext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.PostId == postID && x.UserId == userID);
            if (post == null)
            {
                return false;
            }
            return true;
        }

        private static IQueryable<Post> ApplyFilters(GetPostsFilter getPostsFilter, IQueryable<Post> query)
        {
            if (!string.IsNullOrEmpty(getPostsFilter?.UserId))
            {
                query = query.Where(x => x.UserId == getPostsFilter.UserId);
            }

            return query;
        }
    }
}
