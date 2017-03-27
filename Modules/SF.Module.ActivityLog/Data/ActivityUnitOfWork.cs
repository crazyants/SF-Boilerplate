/*******************************************************************************
* 命名空间: SF.Module.ActivityLog.Data
*
* 功 能： N/A
* 类 名： ActivityUnitOfWork
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 15:14:46 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.Abstraction.Interceptors;
using SF.Data;
using SF.Core.EFCore.UoW;
using SF.Core.Interceptors;
using SF.Module.ActivityLog.Data.Repository;
using System.Collections.Generic;

namespace SF.Module.ActivityLog.Data
{

    public class ActivityUnitOfWork : EFCoreUnitOfWork<CoreDbContext>, IActivityUnitOfWork
    {
        public ActivityUnitOfWork(CoreDbContext context, IEnumerable<IInterceptor> interceptors) : base(context, interceptors)
        {
            Activity = new ActivityRepository(context);
        }

        public IActivityRepository Activity { get; }
    }
}
