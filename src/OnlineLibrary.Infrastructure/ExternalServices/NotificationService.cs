using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace OnlineLibrary.Infrastructure.ExternalServices
{
    public class NotificationService
    {
        private readonly IConnectionMultiplexer _redis;

        public NotificationService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void PublishMessage(string channel, string message)
        {
            var pub = _redis.GetSubscriber();
            pub.Publish(channel, message);
        }

        public void SubscribeMessage(string channel)
        {
            var sub = _redis.GetSubscriber();
            sub.Subscribe(channel, (ch, msg) =>
            {
                Console.WriteLine($"Received message: {msg}");
            });
        }
    }

}
