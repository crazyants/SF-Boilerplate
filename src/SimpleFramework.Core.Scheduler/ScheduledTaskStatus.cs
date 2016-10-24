using System;
using SimpleFramework.Core.Scheduler.Contracts;

namespace SimpleFramework.Core.Scheduler
{
    /// <summary>
    /// Scheduled Task
    /// </summary>
    public class ScheduledTaskStatus
    {
        /// <summary>
        /// Status of last run.
        /// </summary>
        public bool? IsLastRunSuccessful { get; set; }

        /// <summary>
        /// Is still running
        /// </summary>
        public bool IsRunning { set; get; }

        /// <summary>
        /// Task's last run's exception.
        /// </summary>
        public Exception LastException { get; set; }

        /// <summary>
        /// Task's last run time.
        /// </summary>
        public DateTime? LastRun { get; set; }

        /// <summary>
        /// Priority of the task.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Run intervals logic.
        /// </summary>
        public Func<DateTime, bool> RunAt { get; set; }

        /// <summary>
        /// Instance of the task.
        /// </summary>
        public IScheduledTask TaskInstance { set; get; }

        /// <summary>
        /// Type of the task.
        /// </summary>
        public Type TaskType { set; get; }
        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var asThis = obj as ScheduledTaskStatus;
            return asThis != null && asThis.TaskType == this.TaskType;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.TaskType.GetHashCode();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return TaskType.FullName;
        }
    }
}