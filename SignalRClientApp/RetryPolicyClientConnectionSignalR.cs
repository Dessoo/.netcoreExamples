using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRClientApp
{
    public static class RetryPolicyClientConnectionSignalR
    {
        public static HubConnection RetryConnect(this HubConnection connection)
        {
            InstanceHolder instanceHolder = new InstanceHolder(connection);

            connection.Closed += instanceHolder.Connection_Closed;

            return connection;
        }
    }

    public class InstanceHolder
    {
        //you can user your database/config to manage this settings 
        private const string tryReconnect = "TryReconnect";
        private const int stopRetryCount = 100; //after 100 times 
        private const int repeatInterval = 4000; //every 4 seconds
        private readonly HubConnection _connection;

        public InstanceHolder(HubConnection connection)
        {
            this._connection = connection;
        }

        public Task Connection_Closed(Exception arg)
        {
            bool runTry = true;
            int count = 0;

            this._connection.On(tryReconnect, () =>
            {
                runTry = false;
            });

            var retryTask = new Task(() =>
            {
                while (runTry)
                {
                    count++;

                    if (count > stopRetryCount)
                    {
                        runTry = false;
                        break;
                    }

                    this._connection.StartAsync();
                    this._connection.SendAsync(tryReconnect);

                    Console.WriteLine($"TryReconnect {count} times !");
                    Thread.Sleep(repeatInterval);
                }
                Console.WriteLine("TryReconnect finish successfuly !");
            });
            retryTask.Start();
            return retryTask;
        }
    }
}
