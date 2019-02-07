using BusinessLayer.DTO;
using RabbitMQ.Client.Events;
using RabbitMQEvents.BaseEvent;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQEvents.User
{
    public interface IAddEvent : IBaseEventT<UserDTO>
    {

    }
}
