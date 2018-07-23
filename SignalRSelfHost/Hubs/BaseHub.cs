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

        public BaseHub(IConnectionManager connectionManager, ILogProvider logProvider)
        {
            this._logger = logProvider.CreateLogger<BaseHub<THub>>();
            this._connectionManager = connectionManager;
        }

        public override async Task OnConnectedAsync()
        {
            HttpContext httpContext = Context.GetHttpContext();

            try
            {           
                this.Email = httpContext.Request.Query["Email"].ToString();
                this.UserName = httpContext.Request.Query["UserName"].ToString();
                this.AuthToken = httpContext.Request.Query["AuthToken"].ToString();

                this._connectionManager.AddConnection(this.HubName, this.ConnectionId, new AuthEntity() { Email = this.Email, UserName = this.UserName, AuthToken = this.AuthToken });
                this._logger.LogDebug($"Client with connectionId {this.ConnectionId} open chanel to hub {this.HubName}");
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
                this._logger.LogDebug($"Client with connectionId {this.ConnectionId} close chanel to hub {this.HubName}");
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

        private string Email { get; set; }

        private string UserName { get; set; }

        private string AuthToken { get; set; }

        private string ConnectionId { get { return Context.ConnectionId; } }
    }
}
