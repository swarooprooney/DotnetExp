using System.Threading.Tasks;
using TweetBook.Domain;
using TweetBook.Model;

namespace TweetBook.Services
{
    public interface IIdentityService
    {
        public Task<AuthenticationResult> RegisterUserAsync(UserModel user);

        public Task<AuthenticationResult> LoginAsync(UserModel user);
        public Task<AuthenticationResult> RefreshToken(RefreshTokenModel refreshTokenRequest);
    }
}
