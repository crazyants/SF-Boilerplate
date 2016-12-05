using System.Threading.Tasks;

namespace SF.Core.Scheduler.Contracts
{
    /// <summary>
    /// Scheduled task's contract.
    /// </summary>
    public interface IScheduledTask
    {
        /// <summary>
        /// Is ASP.Net app domain tearing down?
        /// If set to true by the coordinator, the task should cleanup and return.
        /// </summary>
        bool IsShuttingDown { set; get; }

        /// <summary>
        /// Scheduled task's logic.
        /// </summary>
        Task RunAsync();
    }
}