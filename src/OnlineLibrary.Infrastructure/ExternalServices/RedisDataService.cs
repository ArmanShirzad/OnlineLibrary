using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;


namespace OnlineLibrary.Infrastructure.ExternalServices
{

    public class RedisDataService
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisDataService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task AddToListAsync(string listKey, string value)
        {
            var db = _redis.GetDatabase();
            await db.ListRightPushAsync(listKey, value);
        }

        public async Task<IEnumerable<string>> GetListAsync(string listKey)
        {
            var db = _redis.GetDatabase();
            var length = await db.ListLengthAsync(listKey);
            var values = await db.ListRangeAsync(listKey, 0, length - 1);
            return values.Select(v => v.ToString());
        }
    }

}
