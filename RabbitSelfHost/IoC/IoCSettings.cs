using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQEvents.BaseEvent;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitSender.IoC
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
            InitQueueModule(services);
        }

        public static void InitRabbitConnection(IServiceCollection services)
        {
            services.AddSingleton(new ConnectionFactory() { HostName = "localhost", UserName = "deso", Password = "123" }.CreateConnection().CreateModel());
            services.AddSingleton<IPublishBus>(s => new PublishBus(services.BuildServiceProvider().GetService<IModel>()));
        }

        public static void InitQueueModule(IServiceCollection services)
        {
            var serviceResolver = services.BuildServiceProvider();
            IModel rabbitMqChanel= serviceResolver.GetService<IModel>();

            rabbitMqChanel.QueueDeclare(queue: "User/Add",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            rabbitMqChanel.QueueDeclare(queue: "User/Update",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            rabbitMqChanel.QueueDeclare(queue: "User/Delete",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
        }
    }
}
