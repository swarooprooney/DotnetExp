using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedisCacheExtensionsExample.Extensions;
using RedisCacheExtensionsExample.Model;

namespace RedisCacheExtensionsExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly IDistributedCache _cacheService;

        public CacheController(IDistributedCache cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpPost]
        public async Task<IActionResult> StoreValue([FromBody] CacheModel model)
        {
            await _cacheService.SetRecordAsync(model.Key, model.Person, new TimeSpan(1, 0, 0));
            return Ok("Cache has been stored");
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetValue(string key)
        {
            var result = await _cacheService.GetRecordAsync<Person>(key);
            return Ok(result);
        }
    }
}
