/*******************************************************************************
* 命名空间: SF.Core.Data.Repository
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
using SF.Core.Abstraction.Entitys.Pages;
using SF.Core.Abstraction.UoW;
using SF.Core.EFCore.UoW;
using SF.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Core.Data.Repository
{
    public class DataItemRepository : EFCoreQueryableRepository<DataItemEntity, long>, IDataItemRepository
    {
        public DataItemRepository(DbContext context) : base(context)
        {
        }
        public override IQueryable<DataItemEntity> QueryById(long id)
        {
            return Query().Where(e => e.Id == id);
        }

        public IPagedList<DataItemEntity> QueryFilterByLevelWithPagination(string itemName, int page = 0, int pageSize = 50, CancellationToken ct = new CancellationToken())
        {
            Func<IQueryable<DataItemEntity>, IOrderedQueryable<DataItemEntity>> dataItemFunc = source => source.OrderBy(x => x.CreatedOn);
            return QueryPage(e => e.ItemName == itemName, dataItemFunc, null, page, pageSize);
        }
    }

}
