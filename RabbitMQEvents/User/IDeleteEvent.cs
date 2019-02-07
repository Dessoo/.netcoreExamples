using BusinessLayer.DTO;
using RabbitMQEvents.BaseEvent;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQEvents.User
{
    public interface IDeleteEvent : IBaseEventT<UserDTO>
    {

    }
}
