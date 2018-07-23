using BusinessLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication.Infrastructure
{
    public static class CacheManager
    {
        const int value = 999;

        public static void ForceInitCache(IServiceCollection services)
        {
            var serviceResolver = services.BuildServiceProvider();

            ICacheService cacheService = serviceResolver.GetService<ICacheService>();
            cacheService.SetCache(Constants.keyCache, value);
        }
    }
}
