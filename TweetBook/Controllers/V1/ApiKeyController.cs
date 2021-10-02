using Microsoft.AspNetCore.Mvc;
using TweetBook.Filters;

namespace TweetBook.Controllers.V1
{
    [ApiKeyAuth]
    public class ApiKeyController : Controller
    {
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return Ok("You can have my secret");
        }
    }
}
