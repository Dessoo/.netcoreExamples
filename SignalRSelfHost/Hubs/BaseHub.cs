using BusinessLayer.BackgroundServices.Queue;
using BusinessLayer.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignalRSelfHost.Connection;
using SignalRSelfHost.Infrastructure;
using System;
using System.Threading.Tasks;

namespace SignalRSelfHost.Hubs
{
    public class BaseHub<THub> : Hub where THub : class
    {
        private readonly IConnectionManager _connectionManager;
        private readonly ILogger _logger;
        private readonly IBackgroundTaskQueue _queue;

        private string Email
        {
            get
            {
                return base.Context.GetHttpContext().Request.Query["Email"].ToString();
            }
        }

        private string UserName
        {
            get
            {
                return base.Context.GetHttpContext().Request.Query["UserName"].ToString();
            }
        }

        private string AuthToken
        {
            get
            {
                return base.Context.GetHttpContext().Request.Query["AuthToken"].ToString();
            }
        }

        private string ConnectionId { get { return Context.ConnectionId; } }

        public BaseHub(IConnectionManager connectionManager, ILogProvider logProvider, IBackgroundTaskQueue queue)
        {
            this._logger = logProvider.CreateLogger<BaseHub<THub>>();
            this._connectionManager = connectionManager;
            this._queue = queue;
        }

        public void TryReconnect()
        {
            this._queue.QueueBackgroundWorkItem(async token =>
            {
                this._logger.LogDebug($"{nameof(this.TryReconnect)} in {this.HubName} activate for client with ConnectionId {this.ConnectionId}");
                await Task.CompletedTask;
            });

            this.Clients.Clients(this.ConnectionId).SendAsync(nameof(this.TryReconnect));
        }

        public override async Task OnConnectedAsync()
        {
            HttpContext httpContext = base.Context.GetHttpContext();

            try
            {           
                this._connectionManager.AddConnection(this.HubName, this.ConnectionId, new AuthEntity() { Email = this.Email, UserName = this.UserName, AuthToken = this.AuthToken });               
                this._queue.QueueBackgroundWorkItem(async token =>
                {
                    this._logger.LogDebug($"Client with connectionId {this.ConnectionId} open chanel to hub {this.HubName}");
                    await Task.CompletedTask;
                });
                Console.WriteLine($"Client with connectionId {this.ConnectionId} open chanel to hub {this.HubName}");
                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                
                if (Environment.UserInteractive)
                {
                    throw ex;
                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            try
            {
                this._connectionManager.DeleteConnection(this.HubName, this.ConnectionId);
                this._queue.QueueBackgroundWorkItem(async token =>
                {
                    this._logger.LogDebug($"Client with connectionId {this.ConnectionId} close chanel to hub {this.HubName}");
                    await Task.CompletedTask;
                });
                
                Console.WriteLine($"Client with connectionId {this.ConnectionId} close chanel to hub {this.HubName}");
                await base.OnDisconnectedAsync(ex);
            }
            catch (Exception exception)
            {
                this._logger.LogError(ex, ex.Message);

                if (Environment.UserInteractive)
                {
                    throw exception;
                }
            }
        }

        private string HubName
        {
            get
            {
                try
                {
                    Type classType = typeof(THub);
                    object[] attribs = classType.GetCustomAttributes(typeof(HubNameAttribute), true);
                    if (attribs.Length > 0)
                    {
                        return ((HubNameAttribute)attribs[0])._hubName;
                    }     
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, ex.Message);

                    if (Environment.UserInteractive)
                    {
                        throw ex;
                    }
                }

                return string.Empty;
            }
        }     
    }
}
