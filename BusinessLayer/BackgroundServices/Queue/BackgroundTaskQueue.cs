using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.BackgroundServices.Queue
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private ConcurrentQueue<Func<CancellationToken, Task>> _workItems = new ConcurrentQueue<Func<CancellationToken, Task>>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                if (workItem == null)
                {
                    throw new ArgumentNullException(nameof(workItem));
                }

                this._workItems.Enqueue(workItem);
                this._signal.Release();

            }).Start();  
        }

        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            await this._signal.WaitAsync(cancellationToken);
            this._workItems.TryDequeue(out var workItem);

            return workItem;
        }
    }
}
