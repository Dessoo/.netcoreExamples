using System;
using System.Collections.Generic;
using System.Threading;
using BusinessLayer.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalRSelfHost.Connection;
using SignalRSelfHost.Hubs;

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
        }

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {         
            logger.LogCritical(e.ExceptionObject.ToString());
            Console.WriteLine(e.ExceptionObject.ToString());
        }

        static void Start()
        {
            logger.LogInformation($"Self Host SignalR Application Start");
            var hub = serviceProvider.GetService<IHubContext<TestHub>>();
            IConnectionManager connectionManager = serviceProvider.GetService<IConnectionManager>();
            while (true)
            {
                List<string> activeConnection = connectionManager.GetActiveConnectionForHub("TestHub");
                if (activeConnection != null)
                {
                    hub.Clients.Clients(activeConnection).SendAsync("SomeEvent", "this is test message");
                }

                Thread.Sleep(4000);
            }
        }

        static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
