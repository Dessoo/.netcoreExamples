using BusinessLayer.BackgroundServices;
using BusinessLayer.BackgroundServices.Queue;
using BusinessLayer.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Shared.Models;
using SignalRSelfHost.Connection;
using SignalRSelfHost.Hubs;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SignalRSelfHost.BackgroundServices
{
    public class SignalRMessageRepeaterService : BackgroundServiceBase
    {
        private event EventHandler RepeatMessage;
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger _logger;
        private readonly IHubContext<TestHub> _testHub;
        private readonly IConnectionManager _connectionManager;

        public SignalRMessageRepeaterService(IBackgroundTaskQueue taskQueue, 
                                                    ILogProvider logProvider, 
                                                    IHubContext<TestHub> testHub,
                                                    IConnectionManager connectionManager) : base(taskQueue, logProvider)
        {
            this._logger = logProvider.CreateLogger<SignalRMessageRepeaterService>();
            this._taskQueue = taskQueue;
            this._testHub = testHub;
            this._connectionManager = connectionManager;
        }

        public override void Start()
        {
            this._logger.LogDebug("Start DoWork");
            base.IsRunning = true;
            this.DoWork();
            //this.RepeatMessage += this.SignalRMessageRepeaterService_RepeatMessage;
            //this.RepeatMessage.Invoke(this, EventArgs.Empty);
        }

        public override void Stop()
        {
            base.IsRunning = false;
            //this.RepeatMessage -= this.SignalRMessageRepeaterService_RepeatMessage;
        }

        //private void SignalRMessageRepeaterService_RepeatMessage(object sender, EventArgs e)
        //{
        //    while (IsRunning)
        //    {
        //        List<string> activeConnection = this._connectionManager.GetActiveConnectionForHub("TestHub");
        //        if (activeConnection != null)
        //        {
        //            this._testHub.Clients.Clients(activeConnection).SendAsync("SomeEvent", new EventMessage() { Message = "Test Message From Repeater Background Service" });
        //        }
        //        Thread.Sleep(3000);
        //    }
        //}

        //private void SignalRMessageRepeaterService_RepeatMessage(object sender, EventArgs e)
        //{
        //    this._taskQueue.QueueBackgroundWorkItem(async token =>
        //    {
        //        while (IsRunning)
        //        {
        //            List<string> activeConnection = this._connectionManager.GetActiveConnectionForHub("TestHub");
        //            if (activeConnection != null)
        //            {
        //                await this._testHub.Clients.Clients(activeConnection).SendAsync("SomeEvent", new EventMessage() { Message = "Test Message From Repeater Background Service" });
        //            }
        //            Thread.Sleep(3000);
        //        }
        //    });
        //}

        //private void DoWork()
        //{
        //    this._taskQueue.QueueBackgroundWorkItem(async token =>
        //    {
        //        while (base.IsRunning)
        //        {
        //            List<string> activeConnection = this._connectionManager.GetActiveConnectionForHub("TestHub");
        //            if (activeConnection != null)
        //            {
        //                if (this._testHub == null)
        //                {
        //                    continue;
        //                }
        //                await this._testHub.Clients.Clients(activeConnection).SendAsync("SomeEvent", new EventMessage() { Message = "Test Message From Repeater Background Service" });
        //            }
        //            Thread.Sleep(3000);
        //        }
        //    });
        //}

        private void DoWork()
        {
            while (IsRunning)
            {
                List<string> activeConnection = this._connectionManager.GetActiveConnectionForHub("TestHub");
                if (activeConnection != null)
                {
                    this._testHub.Clients.Clients(activeConnection).SendAsync("SomeEvent", new EventMessage() { Message = "Test Message From Repeater Background Service" });
                }
                Thread.Sleep(3000);
            }
        }
    }
}
