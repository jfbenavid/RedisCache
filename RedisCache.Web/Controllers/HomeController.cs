namespace RedisCache.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using System;
    using System.Collections.Generic;

    [Route("api")]
    [Route("[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IDistributedCache _database;

        public HomeController(IDistributedCache idatabase)
        {
            _database = idatabase;
        }

        [HttpGet("{key}")]
        public string Get(string key)
        {
            return _database.GetString(key);
        }

        [HttpPost("{minutesAlive}")]
        public void Post([FromBody] KeyValuePair<string, string> keyValue, double? minutesAlive = 5)
        {
            _database.SetString(keyValue.Key, keyValue.Value, new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(minutesAlive.Value) });
        }
    }
}