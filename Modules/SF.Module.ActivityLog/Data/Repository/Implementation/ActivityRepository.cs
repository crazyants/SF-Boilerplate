/*******************************************************************************
* 命名空间: SF.Module.ActivityLog.Data.Repository
*
* 功 能： N/A
* 类 名： ActivityRepository
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 15:18:03 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using SF.Core.EFCore.UoW;
using SF.Module.ActivityLog.Models;
using System.Linq;

namespace SF.Module.ActivityLog.Data.Repository
{
    public class ActivityRepository : EFCoreQueryableRepository<ActivityEntity, long>, IActivityRepository
    {
        public ActivityRepository(DbContext context) : base(context)
        {
        }
        public override IQueryable<ActivityEntity> QueryById(long id)
        {
            return Query().Where(e => e.Id == id);
        }

        
    }

}
