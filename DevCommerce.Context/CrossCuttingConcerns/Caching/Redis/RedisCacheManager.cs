using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevCommerce.Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : IDistributedCache
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public byte[] Get(string key)
        {
            return _distributedCache.Get(key);
        }

        public Task<byte[]> GetAsync(string key, CancellationToken token = default(CancellationToken))
        {
            return _distributedCache.GetAsync(key, token);
        }

        public void Refresh(string key)
        {
            _distributedCache.Refresh(key);
        }

        public Task RefreshAsync(string key, CancellationToken token = default(CancellationToken))
        {
            return _distributedCache.RefreshAsync(key, token);
        }

        public void Remove(string key)
        {
            _distributedCache.Remove(key);
        }

        public Task RemoveAsync(string key, CancellationToken token = default(CancellationToken))
        {
            return _distributedCache.RemoveAsync(key, token);
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            _distributedCache.Set(key, value, options);
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken))
        {
            return _distributedCache.SetAsync(key, value, token);
        }
    }
}
