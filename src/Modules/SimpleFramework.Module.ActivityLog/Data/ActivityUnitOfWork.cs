/*******************************************************************************
* 命名空间: SimpleFramework.Module.ActivityLog.Data
*
* 功 能： N/A
* 类 名： ActivityUnitOfWork
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
using SimpleFramework.Core.Interceptors;
using SimpleFramework.Module.ActivityLog.Data.Repository;

namespace SimpleFramework.Module.ActivityLog.Data
{

    public class ActivityUnitOfWork : EFCoreUnitOfWork<CoreDbContext>, IActivityUnitOfWork
    {
        public ActivityUnitOfWork(CoreDbContext context, params IInterceptor[] interceptors) : base(context, interceptors)
        {
            Activity = new ActivityRepository(context);
        }

        public IActivityRepository Activity { get; }
    }
}
