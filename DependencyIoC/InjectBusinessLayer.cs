using BusinessLayer;
using BusinessLayer.BackgroundServices.Queue;
using BusinessLayer.Core;
using BusinessLayer.DTO;
using BusinessLayer.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.XmlProvider;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependencyIoC
{
    public static class InjectBusinessLayer
    {
        public static void Init(IServiceCollection services)
        {
            var serviceResolver = services.BuildServiceProvider();
            //resolve instance inject from DB layer inject class

            HostInformationDTO hostInformation = new HostInformationDTO(
                    serviceResolver.GetService<IConfiguration>().GetSection("HostInformation").GetSection("Name").Value,
                    serviceResolver.GetService<IConfiguration>().GetSection("HostInformation").GetSection("Server").Value
                );

            LoggerSettingsDTO loggerSettings = new LoggerSettingsDTO(
                    Convert.ToBoolean(serviceResolver.GetService<IConfiguration>().GetSection("Logging").GetSection("TurnOn").Value),
                    serviceResolver.GetService<IConfiguration>().GetSection("Logging").GetSection("LogLevel").Value,
                    Convert.ToInt32(serviceResolver.GetService<IConfiguration>().GetSection("Logging").GetSection("StorageType").Value)
                );
            //route source code to inject other providers like XML storage log provider
            //serviceResolver.GetService<ILoggerFactory>().AddProvider(new DatabaseLoggerProvider(serviceResolver.GetService<IEventLogRepository>(),
            //                                                                hostInformation,
            //                                                                loggerSettings));

            services.AddScoped<ILogProvider>(s => new LogProvider(serviceResolver.GetService<IEventLogRepository>(),
                                                                            serviceResolver.GetService<IXmlDataProvider<EventLog>>(),
                                                                            hostInformation,
                                                                            loggerSettings));

            services.AddScoped<ICacheService>(s => new CacheService(serviceResolver.GetService<IMemoryCache>()));
            services.AddScoped<IUserService>(s => 
                            new UserService(serviceResolver.GetService<IUserRepository>(), services.BuildServiceProvider().GetService<ILogProvider>()));

            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddHostedService<QueuedHostedService>();     
        }
    }
}
