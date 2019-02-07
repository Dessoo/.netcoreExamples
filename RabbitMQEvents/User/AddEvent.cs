using BusinessLayer.DTO;
using RabbitMQ.Client;
using RabbitMQEvents.BaseEvent;

namespace RabbitMQEvents.User
{
    public class AddEvent : BaseEventT<UserDTO>, IAddEvent
    {
        public AddEvent(IModel model) : base(model)
        {

        }
    }
}
