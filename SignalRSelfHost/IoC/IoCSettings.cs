using BusinessLayer.BackgroundServices.Queue;
using BusinessLayer.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SignalRSelfHost.BackgroundServices;
using SignalRSelfHost.Connection;
using SignalRSelfHost.Hubs;

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
