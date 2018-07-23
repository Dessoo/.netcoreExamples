using BusinessLayer.Core;
using Microsoft.Extensions.DependencyInjection;
using SignalRSelfHost.Connection;

namespace SignalRSelfHost.IoC
{
    public static class IoCSettings
    {
        public static void InitIoC(IServiceCollection services)
        {
            services.AddSingleton<IConnectionManager>(new ConnectionManager(services.BuildServiceProvider().GetService<ILogProvider>()));
        }
    }
}
