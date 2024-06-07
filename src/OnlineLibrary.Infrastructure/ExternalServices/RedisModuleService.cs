using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace OnlineLibrary.Infrastructure.ExternalServices
{

    public class RedisModuleService
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisModuleService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task UseRedisJsonAsync()
        {
            var db = _redis.GetDatabase();
            await db.ExecuteAsync("JSON.SET", "doc", ".", "{\"name\":\"John\"}");
            var result = await db.ExecuteAsync("JSON.GET", "doc");
            Console.WriteLine(result.ToString());
        }
    }

}
