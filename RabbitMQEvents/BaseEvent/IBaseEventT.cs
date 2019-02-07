using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQEvents.BaseEvent
{
    public interface IBaseEventT<Tdto> where Tdto : class
    {
        event EventHandler<Tdto> ReceivedData;

        EventingBasicConsumer GetEventingBasicConsumer { get; }
    }
}
