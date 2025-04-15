using StackExchange.Redis;

namespace Login_OAuth.Infrastructure.Caching
{
    // کلاس مدیریت کش با استفاده از Redis
    public class CacheService
    {
        private readonly IDatabase _cache;

        public CacheService(IConnectionMultiplexer redis)
        {
            _cache = redis.GetDatabase(); // دریافت دیتابیس Redis
        }

        // دریافت مقدار از کش بر اساس کلید
        public async Task<string> GetCachedValueAsync(string key)
        {
            return await _cache.StringGetAsync(key);
        }

        // ذخیره مقدار در کش
        public async Task SetCacheValueAsync(string key, string value, TimeSpan expiration)
        {
            await _cache.StringSetAsync(key, value, expiration);
        }
    }
}
