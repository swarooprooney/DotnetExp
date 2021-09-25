using Refit;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;

namespace Tweetbook.Sdk
{
    public interface IIndentityApi
    {
        [Post("/api/v1/identity/register")]
        Task<ApiResponse<AuthSuccessResponse>> RegisterAsync([Body] UserRegistrationRequest userRegistrationRequest);

        [Post("/api/v1/identity/login")]
        Task<ApiResponse<AuthSuccessResponse>> LoginAsync([Body] UserLoginRequest userLoginRequest);

        [Post("/api/v1/identity/refresh")]
        Task<ApiResponse<AuthSuccessResponse>> RefreshAsync([Body] RefreshTokenRequest refreshTokenRequest);
    }
}
