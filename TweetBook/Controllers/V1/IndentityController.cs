using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;
using TweetBook.Domain;
using TweetBook.Services;
using static TweetBook.Contracts.V1.ApiRoutes;

namespace TweetBook.Controllers.V1
{
    public class IndentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IndentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest userRegistrationRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage));
                return BadRequest(new AuthFailedResponse { Errors = errors });
            }
            var user = new User
            {
                Email = userRegistrationRequest.Email,
                Password = userRegistrationRequest.Password
            };
            var authResponse = await _identityService.RegisterUserAsync(user);
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse { Errors = authResponse.Errors });
            }
            return Ok(new AuthSuccessResponse { Token = authResponse.Token});
        }

        [HttpPost(Identity.Login)]
        public async Task<IActionResult> Login([FromBody]UserLoginRequest userLoginRequest)
        {
            var user = new User
            {
                Email = userLoginRequest.UserName,
                Password = userLoginRequest.Password
            };
            var authResponse = await _identityService.LoginAsync(user);
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse { Errors = authResponse.Errors });
            }
            return Ok(new AuthSuccessResponse { Token = authResponse.Token });
        }
    }
}
