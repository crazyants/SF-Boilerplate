/*******************************************************************************
* 命名空间: SF.Core.StartupTask
*
* 功 能： N/A
* 类 名： ISFStarter
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/30 10:41:03 疯狂蚂蚁 初版
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
    /// An <see langword="interface"/> that defines an application SFStarter extension
    /// </summary>
    public interface ISFStarter
    {
        /// <summary>
        /// Runs the application SFStarter extension with specified <paramref name="context"/>.
        /// </summary>
        /// <param name="context">The SFStarter <see cref="Context"/> containing assemblies to scan.</param>
        void Run();
    }
}
