/*******************************************************************************
* 命名空间: SimpleFramework.Core.Data.Repository
*
* 功 能： N/A
* 类 名： IRoleRepository
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 15:18:03 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SimpleFramework.Core.Abstraction.Entitys.Pages;
using SimpleFramework.Core.Abstraction.UoW;
using SimpleFramework.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Data.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataItemRepository :
       IRepository<DataItemEntity, long>
    {
        IPagedList<DataItemEntity> QueryFilterByLevelWithPagination(string itemName, int page = 0, int pageSize = 50, CancellationToken ct = new CancellationToken());
    }
}
