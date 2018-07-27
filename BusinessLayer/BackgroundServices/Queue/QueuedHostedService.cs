using BusinessLayer.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.BackgroundServices.Queue
{
    public class QueuedHostedService : IHostedService
    {
        private readonly ILogger _logger;

        public QueuedHostedService(IBackgroundTaskQueue taskQueue, ILogProvider logProvider)
        {
            TaskQueue = taskQueue;
            _logger = logProvider.CreateLogger<QueuedHostedService>();
        }

        public IBackgroundTaskQueue TaskQueue { get; }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(
            "Consume Scoped Service Hosted Service is stopping.");

            return Task.CompletedTask;
        }

        async Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Queued Hosted Service is starting.");

            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(cancellationToken);

                try
                {
                    await workItem(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occurred executing {nameof(workItem)}.");
                }
            }

            _logger.LogInformation("Queued Hosted Service is stopping.");
        }
    }
}
