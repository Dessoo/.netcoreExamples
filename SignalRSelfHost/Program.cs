using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.BackgroundServices.Queue;
using BusinessLayer.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Models;
using SignalRSelfHost.BackgroundServices;
using SignalRSelfHost.Connection;
using SignalRSelfHost.Hubs;
using SignalRSelfHost.IoC;

namespace SignalRSelfHost
{
    class Program
    {
        private static IServiceProvider serviceProvider { get; set; }

        private static ILogger Logger
        {
            get
            {
                var logService = serviceProvider.GetService<ILogProvider>();
                return logService.CreateLogger<Program>();
            }
        }

        private static IBackgroundTaskQueue Queue
        {
            get
            {
                return serviceProvider.GetService<IBackgroundTaskQueue>();
            }
        }

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            var builder = CreateWebHostBuilder(args);
            var host = builder.Build();
            host.RunAsync();
            serviceProvider = host.Services;

            Start();
            
            Console.Write("Press <Enter> to exit... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {         
            Logger.LogCritical(e.ExceptionObject.ToString());
            Console.WriteLine(e.ExceptionObject.ToString());
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            var messageRepeater = new SignalRMessageRepeaterService(serviceProvider.GetService<IBackgroundTaskQueue>(),
                                                    serviceProvider.GetService<ILogProvider>(), serviceProvider.GetService<IHubContext<TestHub>>(),
                                                    serviceProvider.GetService<IConnectionManager>());

            messageRepeater?.Stop();
        }

        static void Start()
        {
            Queue.QueueBackgroundWorkItem(async token =>
            {
                Logger.LogInformation($"Self Host SignalR Application Start");
                await Task.CompletedTask;
            });
           
            var messageRepeater = new SignalRMessageRepeaterService(serviceProvider.GetService<IBackgroundTaskQueue>(),
                                                    serviceProvider.GetService<ILogProvider>(), serviceProvider.GetService<IHubContext<TestHub>>(),
                                                    serviceProvider.GetService<IConnectionManager>());


            messageRepeater?.Start();
        }

        static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
