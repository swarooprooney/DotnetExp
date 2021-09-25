using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;
using TweetBook.Domain;
using TweetBook.Model;
using TweetBook.Services;
using static TweetBook.Contracts.V1.ApiRoutes;

namespace TweetBook.Controllers.V1
{
    [Produces("application/json")]
    public class IndentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IndentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(Identity.Register)]
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(AuthFailedResponse), 400)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest userRegistrationRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage));
                return BadRequest(new AuthFailedResponse { Errors = errors });
            }
            var user = new UserModel
            {
                Email = userRegistrationRequest.Email,
                Password = userRegistrationRequest.Password
            };
            var authResponse = await _identityService.RegisterUserAsync(user);
            return ConstructAutheticationResult(authResponse);
        }

        [HttpPost(Identity.Login)]
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(AuthFailedResponse), 400)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLoginRequest)
        {
            var user = new UserModel
            {
                Email = userLoginRequest.UserName,
                Password = userLoginRequest.Password
            };
            var authResponse = await _identityService.LoginAsync(user);
            return ConstructAutheticationResult(authResponse);
        }

        [HttpPost(Identity.Refresh)]
        [ProducesResponseType(typeof(AuthSuccessResponse), 200)]
        [ProducesResponseType(typeof(AuthFailedResponse), 400)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var refreshToken = new RefreshTokenModel
            {
                Token = refreshTokenRequest.Token,
                RefreshToken = refreshTokenRequest.RefreshToken
            };

            var authResponse = await _identityService.RefreshToken(refreshToken);
            return ConstructAutheticationResult(authResponse);
        }

        private IActionResult ConstructAutheticationResult(AuthenticationResult authResponse)
        {
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
    }
}
