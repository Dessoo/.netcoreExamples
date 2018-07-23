using BusinessLayer.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            this._cache = cache;
        }

        public object GetByKey(string cacheObjectName)
        {
            return this._cache.Get(cacheObjectName);
        }

        public void SetCache(string key, object value)
        {
            _cache.Set(key, value);
        }
    }
}
