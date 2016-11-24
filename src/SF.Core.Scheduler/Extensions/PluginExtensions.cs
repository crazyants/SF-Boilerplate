using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SF.Core.Abstraction.Data;
using SF.Core.Scheduler.Contracts;
using System;
using System.Collections.Generic;


namespace SF.Core.Scheduler
{
    public class PluginExtensions : ModuleInitializerBase
    {
        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [10000] = this.AddSchedulers
                };
            }
        }

        public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IApplicationBuilder>>()
                {
                    [10000] = this.UseMvcScheduled,
                };
            }
        }



        public void UseMvcScheduled(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<SiteRootUrlMiddleware>();

            var scheduledTasksCoordinator = applicationBuilder.ApplicationServices.GetService<IScheduledTasksCoordinator>();
            scheduledTasksCoordinator.OnUnexpectedException = (ex, name) =>
            {
                logger.LogError(0, ex, $"Failed running {name}");
            };
            scheduledTasksCoordinator.Start();

        }

        /// <summary>
        /// 插件基本服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public void AddSchedulers(IServiceCollection services)
        {
            services.TryAddSingleton<IThisApplication, ThisApplication>();
            services.TryAddSingleton<IJobsRunnerTimer, JobsRunnerTimer>();
            services.TryAddSingleton<IScheduledTasksCoordinator, ScheduledTasksCoordinator>();

            var scheduledTasks = ExtensionManager.GetImplementations<IScheduledTask>();
            var sStorage = new ScheduledTasksStorage();
            //sStorage.AddScheduledTask<DoBackupTask>(
            //    runAt: utcNow =>
            //    {
            //        var now = utcNow.AddHours(3.5);
            //        return now.Day % 3 == 0 && now.Hour == 0 && now.Minute == 1 && now.Second == 1;
            //    },
            //    order: 2);
            foreach (var scheduled in scheduledTasks)
            {
                sStorage.AddScheduledTask(utcNow => utcNow.Second == 1, scheduled);
            }
        }

        private void configTasks(IServiceCollection services, ScheduledTasksStorage storage)
        {
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
