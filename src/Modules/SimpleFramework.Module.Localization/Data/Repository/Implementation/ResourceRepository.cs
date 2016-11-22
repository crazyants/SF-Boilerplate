/*******************************************************************************
* 命名空间: SimpleFramework.Module.Localization.Data.Repository
*
* 功 能： N/A
* 类 名： ResourceRepository
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 15:18:03 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using SimpleFramework.Core.EFCore.UoW;
using SimpleFramework.Module.Localization.Models;
using System.Linq;


namespace SimpleFramework.Module.Localization.Data.Repository
{
    public class ResourceRepository : EFCoreQueryableRepository<ResourceEntity, long>, IResourceRepository
    {
        public ResourceRepository(DbContext context) : base(context)
        {
        }
        public override IQueryable<ResourceEntity> QueryById(long id)
        {
            return Query().Where(e => e.Id == id);
        }

        
    }

}
