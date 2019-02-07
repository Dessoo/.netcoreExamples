using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQEvents.BaseEvent
{
    public interface IPublishBus
    {
        void Publish(IBasePublishEvent publishedEvent);
    }
}
