using BusinessLayer.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.BackgroundServices.Queue
{
    public class QueuedHostedService : BackgroundQueueService
    {
        private readonly ILogger _logger;

        public QueuedHostedService(IBackgroundTaskQueue taskQueue, ILogProvider logProvider)
        {
            this.TaskQueue = taskQueue;
            logProvider.UseQueue = false;
            this._logger = logProvider.CreateLogger<QueuedHostedService>();
        }

        public IBackgroundTaskQueue TaskQueue { get; }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            this._logger.LogInformation("Queued Hosted Service is starting.");

            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await this.TaskQueue.DequeueAsync(cancellationToken);

                try
                {
                    await workItem(cancellationToken);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            this._logger.LogInformation("Queued Hosted Service is stopping.");
        }
    }
}
