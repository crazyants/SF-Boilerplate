/*******************************************************************************
* 命名空间: SimpleFramework.Module.ActivityLog.Data
*
* 功 能： N/A
* 类 名： IActivityUnitOfWork
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 14:56:44 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SimpleFramework.Core.Abstraction.UoW;
using SimpleFramework.Module.ActivityLog.Data.Repository;

namespace SimpleFramework.Module.ActivityLog.Data
{
    public interface IActivityUnitOfWork : IUnitOfWork
    {
        #region Repository

        IActivityRepository Activity { get; }

        #endregion
    }
}
