using BusinessLayer.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
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
            throw new NotImplementedException();
        }

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
