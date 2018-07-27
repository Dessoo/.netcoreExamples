using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.BackgroundServices.Queue
{
    public interface IBackgroundTaskQueue
    {
        /// <summary>
        /// Add Queue to new thread host process incapsulate task 
        /// </summary>
        /// <param name="workItem"></param>
        void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem);

        /// <summary>
        /// Delete Queue
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Func<CancellationToken, Task>> DequeueAsync(
            CancellationToken cancellationToken);
    }
}
