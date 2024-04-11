using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Caching
{
    public class CacheService : ICacheService
    {
        private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetOrCreateAsync<T>(string key,
            Func<CancellationToken, Task<T>> factory,
            TimeSpan? expiration = null,
            CancellationToken cancellationToken = default)
        {
            var cachedData = await _distributedCache.GetAsync(key, cancellationToken);

            if (cachedData != null)
            {
                return Deserialize<T>(cachedData);
            }

            T result = await factory(cancellationToken);

            var options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(expiration ?? DefaultExpiration);

            await _distributedCache.SetAsync(key, Serialize(result), options, cancellationToken);

            return result;
        }

        private byte[] Serialize<T>(T value)
        {
            string serializedValue = JsonSerializer.Serialize(value);
            return Encoding.UTF8.GetBytes(serializedValue);
        }

        private T Deserialize<T>(byte[] bytes)
        {
            string serializedValue = Encoding.UTF8.GetString(bytes);
            return JsonSerializer.Deserialize<T>(serializedValue);
        }
    }
}
