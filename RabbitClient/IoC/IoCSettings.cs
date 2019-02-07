using BusinessLayer.BackgroundServices.Queue;
using BusinessLayer.Core;
using BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitClient.BackgroundServices;
using RabbitMQ.Client;

namespace RabbitClient.IoC
{
    internal static class IoCSettings
    {
        public static void InitIoC(IServiceCollection services)
        {
            IConfiguration builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            services.AddSingleton(builder);
            InitRabbitConnection(services);
            DependencyIoC.InjectDataAccess.Init(services);
            DependencyIoC.InjectBusinessLayer.Init(services);
            InitBackgroundServices(services);
        }

        public static void InitRabbitConnection(IServiceCollection services)
        {
            services.AddSingleton(new ConnectionFactory() { HostName = "localhost", UserName = "deso", Password = "123" }.CreateConnection());
        }

        public static void InitBackgroundServices(IServiceCollection services)
        {
            var serviceResolver = services.BuildServiceProvider();

            services.AddSingleton(new UserBus(serviceResolver.GetService<IConnection>(),
                                                serviceResolver.GetService<IUserService>(), 
                                                serviceResolver.GetService<IBackgroundTaskQueue>(),
                                                serviceResolver.GetService<ILogProvider>()));
        }
    }
}
