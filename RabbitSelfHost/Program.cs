using BusinessLayer.DTO;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQEvents.BaseEvent;
using RabbitSender.IoC;
using System;

namespace RabbitSender
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            IoCSettings.InitIoC(services);

            var serviceResolver = services.BuildServiceProvider();

            //resolve my publisher wrapper instance 
            IPublishBus publishBus = serviceResolver.GetService<IPublishBus>();

            //send a message in the queue

            BasePublishEvent AddPublishEvent = new BasePublishEvent(new UserDTO() { FirstName = "Publish2", LastName = "Event2"}, "User/Add");
            publishBus.Publish(AddPublishEvent);

            //Console.Write("Press <Enter> to exit... ");
            //while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
    }
}
