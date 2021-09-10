using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Data;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class TagService : ITagService
    {
        private readonly DataContext _dataContext;
        public TagService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateTagAsync(Tag tag)
        {
            await _dataContext.Tags.AddAsync(tag);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Tag>> GetTagsAsync()
        {
            return await _dataContext.Tags.ToListAsync();
        }
    }
}
