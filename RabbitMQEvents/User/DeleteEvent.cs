using BusinessLayer.DTO;
using RabbitMQ.Client;
using RabbitMQEvents.BaseEvent;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQEvents.User
{
    public class DeleteEvent : BaseEventT<UserDTO>, IDeleteEvent
    {
        public DeleteEvent(IModel model) : base(model)
        {

        }
    }
}
