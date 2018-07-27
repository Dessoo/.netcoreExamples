using BusinessLayer.BackgroundServices.Queue;
using BusinessLayer.Core;

namespace BusinessLayer.BackgroundServices
{
    public abstract class BackgroundServiceBase
    {
        public bool IsRunning { get; set; }

        public BackgroundServiceBase(IBackgroundTaskQueue taskQueue, ILogProvider logProvider)
        {

        }

        public abstract void Start();

        public abstract void Stop();
    }
}
