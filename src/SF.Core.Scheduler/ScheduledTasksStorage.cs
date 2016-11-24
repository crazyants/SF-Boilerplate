using System;
using System.Collections.Generic;
using System.Linq;
using SF.Core.Scheduler.Contracts;
using SF.Core.Common;

namespace SF.Core.Scheduler
{
    /// <summary>
    /// Scheduled Tasks Storage
    /// </summary>
    public class ScheduledTasksStorage
    {
        /// <summary>
        /// Scheduled Tasks Storage
        /// </summary>
        public ScheduledTasksStorage()
        {
            Tasks = new HashSet<ScheduledTaskStatus>();
        }

        /// <summary>
        /// Gets the list of the scheduled tasks.
        /// </summary>
        public ISet<ScheduledTaskStatus> Tasks { get; }

        public void AddScheduledTask(Func<DateTime, bool> runAt,Type taskType, int order = 1) 
        {
            runAt.CheckArgumentNull(nameof(runAt));

            Tasks.Add(new ScheduledTaskStatus
            {
                TaskType = taskType,
                RunAt = runAt,
                Order = order
            });
        }
        /// <summary>
        /// Adds a new scheduled task.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="runAt">
        /// If the RunAt method returns true, the Run method will be executed.
        /// utcNow is expressed as the Coordinated Universal Time (UTC).
        /// It will be called every one second.
        /// </param>
        /// <param name="order">
        /// The priority of the task.
        /// If you have multiple jobs at the same time, this value indicates the order of their execution.
        /// </param>
        public void AddScheduledTask<T>(Func<DateTime, bool> runAt, int order = 1) where T : IScheduledTask
        {
            runAt.CheckArgumentNull(nameof(runAt));

            Tasks.Add(new ScheduledTaskStatus
            {
                TaskType = typeof(T),
                RunAt = runAt,
                Order = order
            });
        }

        /// <summary>
        /// Removes a task from the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">When the T is not found.</exception>
        public void RemoveScheduledTask<T>() where T : IScheduledTask
        {
            var task = Tasks.FirstOrDefault(taskTemplate => taskTemplate.TaskType == typeof(T));
            if (task == null)
            {
                throw new InvalidOperationException($"{typeof(T)} not found and is not registered.");
            }
            Tasks.Remove(task);
        }
    }
}