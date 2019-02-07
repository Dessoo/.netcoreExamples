using BusinessLayer.BackgroundServices;
using BusinessLayer.BackgroundServices.Queue;
using BusinessLayer.Core;
using BusinessLayer.DTO;
using BusinessLayer.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQEvents.User;
using System.Text;

namespace RabbitClient.BackgroundServices
{
    public class UserBus : BackgroundServiceBase
    {
        private readonly ILogger _logger;
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly IModel _channel;
        private readonly IAddEvent _consumerAdd;
        private readonly IUpdateEvent _consumerUpdate;
        private readonly IDeleteEvent _consumerDelete;
        private readonly IUserService _userService;
        private string _consumerTagAdd { get; set; }
        private string _consumerTagUpdate { get; set; }
        private string _consumerTagDelete { get; set; }
        
        public UserBus(IConnection connection, IUserService userService, IBackgroundTaskQueue taskQueue, ILogProvider logProvider) : base(taskQueue, logProvider)
        {           
            this._userService = userService;
            this._taskQueue = taskQueue;
            this._logger = logProvider.CreateLogger<UserBus>();
            this._channel = connection.CreateModel();
            this._consumerAdd = new AddEvent(this._channel);
            this._consumerUpdate = new UpdateEvent(this._channel);
            this._consumerDelete = new DeleteEvent(this._channel);
        }

        
        public override void Start()
        {
            base.IsRunning = true;

            this._consumerAdd.ReceivedData += this.ConsumerAdd_Received;
            this._consumerUpdate.ReceivedData += this.ConsumerUpdate_Received;
            this._consumerDelete.ReceivedData += this.ConsumerDelete_Received;

            this._consumerTagAdd = this._channel.BasicConsume(queue: "User/Add", autoAck: true, consumer: this._consumerAdd.GetEventingBasicConsumer);
            this._consumerTagUpdate = this._channel.BasicConsume(queue: "User/Update", autoAck: true, consumer: this._consumerUpdate.GetEventingBasicConsumer);
            this._consumerTagDelete = this._channel.BasicConsume(queue: "User/Delete", autoAck: true, consumer: this._consumerDelete.GetEventingBasicConsumer);
        }

        public override void Stop()
        {
            base.IsRunning = false;

            this._consumerAdd.ReceivedData -= this.ConsumerAdd_Received;
            this._consumerUpdate.ReceivedData -= this.ConsumerUpdate_Received;
            this._consumerDelete.ReceivedData -= this.ConsumerDelete_Received;

            this._channel.BasicCancel(this._consumerTagAdd);
            this._channel.BasicCancel(this._consumerTagUpdate);
            this._channel.BasicCancel(this._consumerTagDelete);
        }

        private void ConsumerAdd_Received(object sender, UserDTO e)
        {
            if (base.IsRunning)
            {
                this.Add(e);
            }
        }

        private void ConsumerUpdate_Received(object sender, UserDTO e)
        {
            if (base.IsRunning)
            {
                this.Update(e);
            }
        }

        private void ConsumerDelete_Received(object sender, UserDTO e)
        {
            if (base.IsRunning)
            {
                this.Delete(e);
            }
        }

        private void Add(UserDTO user)
        {
            //Add 
            this._userService.AddUser(user);
        }

        private void Update(UserDTO user)
        {
            //Update
            this._userService.UpdateUser(user);
        }

        private void Delete(UserDTO user)
        {
            //Delete
            this._userService.DeleteUser(user);
        }
    }
}
