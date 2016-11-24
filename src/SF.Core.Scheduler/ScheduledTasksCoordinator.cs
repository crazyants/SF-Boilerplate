using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SF.Core.Scheduler.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SF.Core.Common;

namespace SF.Core.Scheduler
{
    /// <summary>
    /// Scheduled Tasks Manager
    /// </summary>
    public sealed class ScheduledTasksCoordinator : IScheduledTasksCoordinator
    {
        // the 30 seconds is for the entire app to tie up what it's doing.
        private const int TimeToFinish = 30 * 1000;

        private readonly IJobsRunnerTimer _jobsRunnerTimer;
        private readonly ILogger<ScheduledTasksCoordinator> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<ScheduledTasksStorage> _tasksStorage;
        private readonly IThisApplication _thisApplication;
        private bool _isShuttingDown;

        /// <summary>
        /// Scheduled Tasks Manager
        /// </summary>
        public ScheduledTasksCoordinator(
            ILogger<ScheduledTasksCoordinator> logger,
            IApplicationLifetime applicationLifetime,
            IOptions<ScheduledTasksStorage> tasksStorage,
            IJobsRunnerTimer jobsRunnerTimer,
            IThisApplication thisApplication,
            IServiceProvider serviceProvider)
        {
            logger.CheckArgumentNull(nameof(logger));
            applicationLifetime.CheckArgumentNull(nameof(applicationLifetime));
            tasksStorage.CheckArgumentNull(nameof(tasksStorage));
            jobsRunnerTimer.CheckArgumentNull(nameof(jobsRunnerTimer));
            thisApplication.CheckArgumentNull(nameof(thisApplication));
            serviceProvider.CheckArgumentNull(nameof(serviceProvider));

            _logger = logger;
            _tasksStorage = tasksStorage;
            _jobsRunnerTimer = jobsRunnerTimer;
            _thisApplication = thisApplication;
            _serviceProvider = serviceProvider;
            applicationLifetime.ApplicationStopping.Register(() =>
            {
                _logger.LogInformation("Application is stopping ... .");
                disposeResources().Wait();
            });
        }

        /// <summary>
        /// Fires on unhandled exceptions.
        /// </summary>
        public Action<Exception, string> OnUnexpectedException { set; get; }

        /// <summary>
        /// Starts the scheduler.
        /// </summary>
        public void Start()
        {
            if (_jobsRunnerTimer.IsRunning)
            {
                return;
            }

            _jobsRunnerTimer.OnThreadPoolTimerCallback = () =>
            {
                var now = DateTime.UtcNow;

                foreach (var taskStatus in _tasksStorage.Value.Tasks.Where(x => x.IsRunning && x.RunAt(now)))
                {
                    _logger.LogWarning($"Ignoring `{taskStatus}`. It's still running.");
                }

                var tasks = new List<Task>();
                foreach (var taskStatus in _tasksStorage.Value.Tasks.Where(x => !x.IsRunning && x.RunAt(now))
                                                              .OrderBy(x => x.Order))
                {
                    if (_isShuttingDown)
                    {
                        return;
                    }

                    tasks.Add(Task.Run(() => runTask(taskStatus)));
                }

                if (tasks.Any())
                {
                    Task.WaitAll(tasks.ToArray());
                }
            };
            _jobsRunnerTimer.Start();
        }

        /// <summary>
        /// Stops the scheduler.
        /// </summary>
        public Task Stop()
        {
            return disposeResources();
        }

        private async Task disposeResources()
        {
            if (_isShuttingDown)
            {
                return;
            }

            try
            {
                _isShuttingDown = true;

                foreach (var task in _tasksStorage.Value.Tasks.Where(x => x.TaskInstance != null))
                {
                    task.TaskInstance.IsShuttingDown = true;
                }

                var timeOut = TimeToFinish;
                while (_tasksStorage.Value.Tasks.Any(x => x.IsRunning) && timeOut >= 0)
                {
                    // still running ...
                    await Task.Delay(50).ConfigureAwait(false);
                    timeOut -= 50;
                }
            }
            finally
            {
                _jobsRunnerTimer.Stop();
                await _thisApplication.WakeUp().ConfigureAwait(false);
            }
        }

        private void runTask(ScheduledTaskStatus taskStatus)
        {
            using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var scheduledTask = (IScheduledTask)serviceScope.ServiceProvider.GetRequiredService(taskStatus.TaskType);
                taskStatus.TaskInstance = scheduledTask;
                var name = scheduledTask.GetType().GetTypeInfo().FullName;

                try
                {
                    if (_isShuttingDown)
                    {
                        return;
                    }

                    taskStatus.IsRunning = true;
                    taskStatus.LastRun = DateTime.UtcNow;

                    _logger.LogInformation($"Start running {name}");
                    scheduledTask.RunAsync().Wait();

                    _logger.LogInformation($"Finished running {name}");
                    taskStatus.IsLastRunSuccessful = true;
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(0, ex, $"Failed running {name}");
                    taskStatus.IsLastRunSuccessful = false;
                    taskStatus.LastException = ex;
                    OnUnexpectedException?.Invoke(ex, name);
                }
                finally
                {
                    taskStatus.IsRunning = false;
                    taskStatus.TaskInstance = null;
                }
            }
        }
    }
}