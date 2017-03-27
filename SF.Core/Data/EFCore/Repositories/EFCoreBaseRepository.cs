/*******************************************************************************
* 命名空间: SF.Core.EFCore.Repository
*
* 功 能： N/A
* 类 名： IRoleRepository
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
using SF.Entitys.Abstraction;
using SF.Entitys.Abstraction.Pages;
using SF.Core.Abstraction.UoW;
using SF.Core.EFCore.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Core.EFCore.Repository
{
    public class EFCoreBaseRepository<TEntity> : EFCoreQueryableRepository<TEntity, long> where TEntity : BaseEntity
    {
        public EFCoreBaseRepository(DbContext context) : base(context)
        {
        }
        public override IQueryable<TEntity> QueryById(long id)
        {
            return Query().Where(e => e.Id == id);
        }
    }
    public class EFCoreBaseRepository<TEntity, TKey> : EFCoreQueryableRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public EFCoreBaseRepository(DbContext context) : base(context)
        {
        }
        public override IQueryable<TEntity> QueryById(TKey id)
        {
            return Query().Where(e => e.Id.Equals(id));
        }


    }

}
