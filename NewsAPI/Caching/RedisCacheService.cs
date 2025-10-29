using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPI.Caching
{
    using Microsoft.Extensions.Caching.Distributed;
    using System.Text.Json;
    using System.Text;

    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var data = await _cache.GetAsync(key);
            if (data is null) return default;

            var json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? ttl = null)
        {
            var json = JsonSerializer.Serialize(value);
            var data = Encoding.UTF8.GetBytes(json);

            var options = new DistributedCacheEntryOptions();
            if (ttl.HasValue)
                options.AbsoluteExpirationRelativeToNow = ttl;

            await _cache.SetAsync(key, data, options);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }

}
