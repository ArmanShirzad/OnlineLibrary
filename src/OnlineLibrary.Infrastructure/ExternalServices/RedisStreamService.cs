using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace OnlineLibrary.Infrastructure.ExternalServices
{

    public class RedisStreamService
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisStreamService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task AddToStreamAsync(string streamKey, NameValueEntry[] values)
        {
            var db = _redis.GetDatabase();
            await db.StreamAddAsync(streamKey, values);
        }

        public async Task<StreamEntry[]> ReadStreamAsync(string streamKey, string position = "0-0")
        {
            var db = _redis.GetDatabase();
            return await db.StreamReadAsync(streamKey, position);
        }
    }

}
