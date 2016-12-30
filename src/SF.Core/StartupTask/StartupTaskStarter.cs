/*******************************************************************************
* 命名空间: SF.Core.StartupTask
*
* 功 能： N/A
* 类 名： StartupTaskStarter
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/30 10:41:54 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.StartupTask
{
    /// <summary>
    /// A KickStart extension to run startup tasks on application start.
    /// </summary>
    public class StartupTaskStarter : ISFStarter
    {
        private readonly ILogger _logger;
        private readonly IEnumerable<IStartupTask> _startupTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupTaskStarter"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public StartupTaskStarter(ILogger<StartupTaskStarter> logger
            , IEnumerable<IStartupTask> startupTask)
        {
            _logger = logger;
            _startupTask = startupTask;
        }

        /// <summary>
        /// Runs the application KickStart extension with specified <paramref name="context" />.
        /// </summary>
        /// <param name="context">The KickStart <see cref="Context" /> containing assemblies to scan.</param>
        public void Run()
        {
            var startupTasks = _startupTask
                .OrderBy(t => t.Priority)
                .ToList();

            var watch = new Stopwatch();

            foreach (var startupTask in startupTasks)
            {

                _logger.LogTrace($"Execute Startup Task; Type: '{startupTask}'");

                watch.Restart();
                startupTask.Run();
                watch.Stop();

                _logger.LogTrace($"Complete Startup Task; Type: '{startupTask}', Time: { watch.ElapsedMilliseconds}ms");
            }

        }
    }
}
