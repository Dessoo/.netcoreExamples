using System;
using System.ServiceProcess;
using BusinessLayer.BackgroundServices.Queue;
using BusinessLayer.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalRSelfHost.BackgroundServices;
using SignalRSelfHost.Connection;
using SignalRSelfHost.Hubs;

namespace SignalRSelfHost
{
    public class Program
    {
        public const string ServiceName = "MyService";

        public class Service : ServiceBase
        {
            public Service()
            {
                ServiceName = Program.ServiceName;
            }

            protected override void OnStart(string[] args)
            {
                Program.Start();
            }

            protected override void OnStop()
            {
                Program.Stop();
            }
        }
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

            if (!Environment.UserInteractive)
                // running as service
                using (var service = new Service())
                    ServiceBase.Run(service);
            else
            {
                // running as console app

                Start();

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey(true);

                Stop();
            }
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

        private static void Start()
        {
            Logger.LogInformation($"Self Host SignalR Application Start");

            var messageRepeater = new SignalRMessageRepeaterService(serviceProvider.GetService<IBackgroundTaskQueue>(),
                                                    serviceProvider.GetService<ILogProvider>(), serviceProvider.GetService<IHubContext<TestHub>>(),
                                                    serviceProvider.GetService<IConnectionManager>());


            messageRepeater?.Start();
        }

        private static void Stop()
        {
            Logger.LogInformation($"Self Host SignalR Application Stop");
        }

        static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
