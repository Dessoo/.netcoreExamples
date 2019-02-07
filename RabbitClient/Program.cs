using Microsoft.Extensions.DependencyInjection;
using RabbitClient.BackgroundServices;
using RabbitClient.IoC;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            IoCSettings.InitIoC(services);

            var serviceResolver = services.BuildServiceProvider();
            UserBus userBus = serviceResolver.GetService<UserBus>();
            userBus?.Start();

            //Console.Write("Press <Enter> to exit... ");
            //while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
    }
}
