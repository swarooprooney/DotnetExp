using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisCacheExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCacheExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpPost]
        public async Task<IActionResult> StoreValue([FromBody] CacheModel model)
        {
            await _cacheService.StoreCacheAsync(model.Key, model.Value);
            return Ok("Cache has been stored");
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetValue(string key)
        {
            var result = await _cacheService.GetCacheAsync(key);
            return Ok(result);
        }
    }
}
