using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiaryApp.API.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task CacheResponseAsync(this IDistributedCache cache, string cacheKey, object response, TimeSpan absoluteExpireTime)
        {
            if (response is null)
                return;

            await cache.SetRecordAsync(cacheKey, response, absoluteExpireTime);
        }

        public static async Task<string> GetCachedResponseAsync(this IDistributedCache cache, string cacheKey)
        {
            var cachedResponse = await cache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
        }

        public static async Task SetRecordAsync<T>(this IDistributedCache cache, 
            string recordId, T data, 
            TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60),
                SlidingExpiration = unusedExpireTime
            };

            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);

            if (jsonData is null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
