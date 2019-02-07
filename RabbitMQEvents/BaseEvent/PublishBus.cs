using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQEvents.BaseEvent
{
    public class PublishBus : IPublishBus
    {
        private readonly IModel _channel;

        public PublishBus(IModel channel)
        {
            this._channel = channel;
        }

        public void Publish(IBasePublishEvent publishedEvent)
        {
            this._channel.BasicPublish(exchange: "",
                      routingKey: publishedEvent.Queue(),
                      basicProperties: null,
                      body: GetBody(publishedEvent.Data()));
        }

        private byte[] GetBody(object data)
        {
            string json2 = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(json2);
        }
    }
}
