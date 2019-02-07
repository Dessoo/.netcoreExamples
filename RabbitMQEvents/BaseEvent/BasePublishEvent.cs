using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQEvents.BaseEvent
{
    public class BasePublishEvent : IBasePublishEvent
    {
        private readonly string _queue;
        private readonly object _data;

        public BasePublishEvent(object data, string queue)
        {
            this._queue = queue;
            this._data = data;
        }

        public object Data()
        {
            return this._data;
        }

        public string Queue()
        {
            return this._queue;
        }
    }
}
