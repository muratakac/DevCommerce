using ServiceStack.Redis;
using System;

namespace DevCommerce.Core.CrossCuttingConcerns.Cache.Redis
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly RedisEndpoint _endPoint;

        public RedisCacheProvider() : this("127.0.0.1", 6379, null, 0)
        {
        }

        public RedisCacheProvider(string host, int port) : this(host, port, null, 0)
        {
        }

        public RedisCacheProvider(string host, int port, string password) : this(host, port, password, 0)
        {
            _endPoint = new RedisEndpoint(host, port, password);
        }

        public RedisCacheProvider(string host, int port, string password, long db)
        {
            _endPoint = new RedisEndpoint(host, port, password, db);
        }

        public void Set(string key, string value)
        {
            this.Set(key, value, TimeSpan.Zero);
        }

        public void Set(string key, string value, TimeSpan timeout)
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                client.SetValue(key, value, timeout);
            }
        }

        public string Get(string key)
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                return client.GetValue(key);
            }
        }

        public bool Remove(string key)
        {
            bool removed = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                removed = client.Remove(key);
            }

            return removed;
        }

        public bool IsInCache(string key)
        {
            bool isInCache = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                isInCache = client.ContainsKey(key);
            }

            return isInCache;
        }
    }
}
