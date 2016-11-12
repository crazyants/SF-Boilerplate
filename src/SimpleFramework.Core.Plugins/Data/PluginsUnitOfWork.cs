/*******************************************************************************
* 命名空间: SimpleFramework.Core.Data
*
* 功 能： N/A
* 类 名： PluginsUnitOfWork
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 15:14:46 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SimpleFramework.Core.Data;
using SimpleFramework.Core.Data.UoW;
using SimpleFramework.Core.Data.WorkArea;
using SimpleFramework.Core.Interceptors;
using SimpleFramework.Core.Plugins.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Plugins.Data
{

    public class PluginsUnitOfWork : EFCoreUnitOfWork<CoreDbContext>, IPluginsUnitOfWork
    {
        public PluginsUnitOfWork(CoreDbContext context, params IInterceptor[] interceptors) : base(context, interceptors)
        {
            Plugin = new PluginRepository(context);
        }

        public IPluginRepository Plugin { get; }
    }
}
