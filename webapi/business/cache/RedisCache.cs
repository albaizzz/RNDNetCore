using System;
using StackExchange.Redis;

namespace webapi.business.cache
{
    public class RedisCache
    {
        private string _url;

        public RedisCache(string url)
        {
            _url = url;
        }

        public bool SetData(int lifeTime, string key, string objectString)
        {
            using (var redisConnection = ConnectionMultiplexer.Connect(_url))
            {
                return redisConnection.GetDatabase().StringSet(
                    key,
                    objectString,
                    TimeSpan.FromMinutes(lifeTime)
                );
            }
        }

        public string GetData(string key)
        {
            using (var redisConnection = ConnectionMultiplexer.Connect(_url))
            {
                string json = redisConnection.GetDatabase().StringGet(key);

                if (!string.IsNullOrEmpty(json))
                {
                    return json;
                }

                return null;
            }
        }

        public bool RemoveData(string key)
        {
            using (var redisConnection = ConnectionMultiplexer.Connect(_url))
            {
                return redisConnection.GetDatabase().KeyDelete(key);
            }
        }
    }
}