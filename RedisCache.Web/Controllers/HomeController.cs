namespace RedisCache.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using StackExchange.Redis;
    using System;
    using System.Collections.Generic;

    [Route("api")]
    [Route("[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IDatabase _database;

        public HomeController(IDatabase idatabase)
        {
            _database = idatabase;
        }

        [HttpGet("{key}")]
        public string Get(string key)
        {
            return _database.StringGet(key);
        }

        [HttpPost("{minutesAlive}")]
        public void Post([FromBody] KeyValuePair<string, string> keyValue, double? minutesAlive = 5)
        {
            _database.StringSet(keyValue.Key, keyValue.Value, TimeSpan.FromMinutes(minutesAlive.Value));
        }
    }
}