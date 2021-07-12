using System.Threading.Tasks;

namespace RedisCacheExample
{
    public interface ICacheService
    {
        Task<string> GetCacheAsync(string key);

        Task StoreCacheAsync(string key, string value);
    }
}
