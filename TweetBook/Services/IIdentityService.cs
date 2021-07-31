using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public interface IIdentityService
    {
        public Task<AuthenticationResult> RegisterUserAsync(User user);

        public Task<AuthenticationResult> LoginAsync(User user);
    }
}
