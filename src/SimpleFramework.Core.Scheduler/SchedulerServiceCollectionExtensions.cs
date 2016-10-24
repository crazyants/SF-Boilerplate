using System;
using SimpleFramework.Core.Scheduler.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SimpleFramework.Core.Common;

namespace SimpleFramework.Core.Scheduler
{
    /// <summary>
    ///  Scheduler ServiceCollection Extensions
    /// </summary>
    public static class SchedulerServiceCollectionExtensions
    {
        /// <summary>
        /// Adds default DNTScheduler providers.
        /// </summary>
        public static void AddScheduler(this IServiceCollection services, Action<ScheduledTasksStorage> options)
        {

            services.TryAddSingleton<IThisApplication, ThisApplication>();
            services.TryAddSingleton<IJobsRunnerTimer, JobsRunnerTimer>();
            services.TryAddSingleton<IScheduledTasksCoordinator, ScheduledTasksCoordinator>();

            configTasks(services, options);
        }

        private static void configTasks(IServiceCollection services, Action<ScheduledTasksStorage> options)
        {
            var storage = new ScheduledTasksStorage();
            options(storage);

            foreach (var task in storage.Tasks)
            {
                services.TryAddTransient(task.TaskType);
            }

            storage.AddScheduledTask<PingTask>(runAt: utcNow => utcNow.Second == 1);
            services.TryAddSingleton<PingTask>();

            services.TryAddSingleton(Options.Create(storage));
        }
    }
}