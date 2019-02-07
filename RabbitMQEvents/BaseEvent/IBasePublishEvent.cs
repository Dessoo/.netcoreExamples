using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQEvents.BaseEvent
{
    public interface IBasePublishEvent
    {
        string Queue();

        object Data();
    }
}
