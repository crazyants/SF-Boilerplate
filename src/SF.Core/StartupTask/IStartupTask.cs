/*******************************************************************************
* 命名空间: SF.Core.StartupTask
*
* 功 能： N/A
* 类 名： IStartupTask
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/30 10:39:30 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.StartupTask
{
    /// <summary>
    /// 定义应用程序启动时应运行的任务的接口
    /// </summary>
    public interface IStartupTask
    {
        /// <summary>
        /// 获取此任务的优先级。较低的数字先运行。
        /// </summary>
        /// <value>
        /// The priority of this task.
        /// </value>
        int Priority { get; }

        /// <summary>
        /// Runs the startup task.
        /// </summary>
        Task Run();
    }
}
