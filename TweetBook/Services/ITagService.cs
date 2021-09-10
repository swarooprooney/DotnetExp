using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public interface ITagService
    {
        Task<bool> CreateTagAsync(Tag tag);

        Task<IEnumerable<Tag>> GetTagsAsync();
    }
}