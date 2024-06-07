using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace OnlineLibrary.Infrastructure.ExternalServices
{

    public class GeoService
    {
        private readonly IConnectionMultiplexer _redis;

        public GeoService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task AddGeoLocationAsync(string geoKey, double longitude, double latitude, string member)
        {
            var db = _redis.GetDatabase();
            await db.GeoAddAsync(geoKey, longitude, latitude, member);
        }

        public async Task<GeoPosition?> GetGeoLocationAsync(string geoKey, string member)
        {
            var db = _redis.GetDatabase();
            return await db.GeoPositionAsync(geoKey, member);
        }
    }

}
