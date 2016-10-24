using System.Threading.Tasks;
using SimpleFramework.Core.Scheduler.Contracts;

namespace SimpleFramework.Core.Scheduler
{
    /// <summary>
    /// DNTScheduler needs a ping service to keep it alive.
    /// </summary>
    public class PingTask : IScheduledTask
    {
        private readonly IThisApplication _thisApplication;

        /// <summary>
        /// DNTScheduler needs a ping service to keep it alive.
        /// </summary>
        public PingTask(IThisApplication thisApplication)
        {
            _thisApplication = thisApplication;
        }

        /// <summary>
        /// Is ASP.Net app domain tearing down?
        /// If set to true by the coordinator, the task should cleanup and return.
        /// </summary>
        public bool IsShuttingDown { get; set; }

        /// <summary>
        /// Scheduled task's logic.
        /// </summary>
        public async Task RunAsync()
        {
            if (this.IsShuttingDown)
            {
                return;
            }

            await _thisApplication.WakeUp().ConfigureAwait(false);
        }
    }
}