using System;
using System.Collections.Generic;
using System.Threading;
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

        private static ILogger logger
        {
            get
            {
                var logService = serviceProvider.GetService<ILogProvider>();
                return logService.CreateLogger<Program>();
            }
        }

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
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
            logger.LogCritical(e.ExceptionObject.ToString());
            Console.WriteLine(e.ExceptionObject.ToString());
        }

        static void Start()
        {
            logger.LogInformation($"Self Host SignalR Application Start");

            var messageRepeater = new SignalRMessageRepeaterService(serviceProvider.GetService<IBackgroundTaskQueue>(),
                                                    serviceProvider.GetService<ILogProvider>(), serviceProvider.GetService<IHubContext<TestHub>>(),
                                                    serviceProvider.GetService<IConnectionManager>());

            new Thread(() =>
            {
                messageRepeater?.Start();
            }).Start();

            Console.Write("Service Wait 40 seconds ");
            Thread.Sleep(40000);
            messageRepeater?.Stop();
            Console.Write("Service call stop method ");
        }

        static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
