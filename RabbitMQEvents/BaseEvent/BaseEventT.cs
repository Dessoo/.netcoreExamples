using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQEvents.BaseEvent
{
    public class BaseEventT<Tdto> : EventingBasicConsumer, IBaseEventT<Tdto>
        where Tdto : class
    {
        private Tdto _dto_Entity;

        public BaseEventT(IModel model) : base(model)
        {
            this.Received += this.GetEventData;
        }

        public EventingBasicConsumer GetEventingBasicConsumer { get { return this; } }

        public event EventHandler<Tdto> ReceivedData;

        private void GetEventData(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var userString = Encoding.UTF8.GetString(body);
            this._dto_Entity = JsonConvert.DeserializeObject<Tdto>(userString);
            this.ReceivedData.Invoke(new object(), this._dto_Entity);
        }
    }
}
