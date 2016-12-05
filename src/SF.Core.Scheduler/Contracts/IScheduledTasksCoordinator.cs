using System;
using System.Threading.Tasks;

namespace SF.Core.Scheduler.Contracts
{
    /// <summary>
    /// Scheduled Tasks Manager
    /// </summary>
    public interface IScheduledTasksCoordinator
    {
        /// <summary>
        /// Fires on unhandled exceptions.
        /// </summary>
        Action<Exception, string> OnUnexpectedException { set; get; }

        /// <summary>
        /// Starts the scheduler.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the scheduler.
        /// </summary>
        Task Stop();
    }
}