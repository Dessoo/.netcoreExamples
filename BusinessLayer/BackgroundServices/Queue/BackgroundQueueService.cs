using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.BackgroundServices.Queue
{
    public abstract class BackgroundQueueService : IHostedService
    {

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return this.ExecuteAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return this.ExecuteAsync(cancellationToken);
        }

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
