/*******************************************************************************
* 命名空间: SimpleFramework.Module.ActivityLog.Data.Repository
*
* 功 能： N/A
* 类 名： IActivityRepository
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 15:18:03 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/


using SimpleFramework.Core.Data.UoW;
using SimpleFramework.Module.ActivityLog.Models;

namespace SimpleFramework.Module.ActivityLog.Data.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IActivityRepository :
       IEFCoreQueryableRepository<ActivityEntity, long>
    {
         
    }
}
