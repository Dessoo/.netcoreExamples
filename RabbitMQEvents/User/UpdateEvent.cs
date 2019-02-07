using BusinessLayer.DTO;
using RabbitMQ.Client;
using RabbitMQEvents.BaseEvent;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQEvents.User
{
    public class UpdateEvent : BaseEventT<UserDTO>, IUpdateEvent
    {
        public UpdateEvent(IModel model) : base(model)
        {

        }
    }
}
