/*******************************************************************************
* 命名空间: SF.Module.LodgingLog.Data.Repository
*
* 功 能： N/A
* 类 名： LodgingRepository
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
using System.Linq;

namespace SF.Module.SimpleData.Data.Repository
{
    public class LodgingRepository : EFCoreQueryableRepository<Lodging, long>, ILodgingRepository
    {
        public LodgingRepository(DbContext context) : base(context)
        {
        }
        public override IQueryable<Lodging> QueryById(long id)
        {
            return Query().Where(e => e.LodgingId == id);
        }


    }

}
