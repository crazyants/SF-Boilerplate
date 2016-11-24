/*******************************************************************************
* 命名空间: SF.Module.Backend.Services
*
* 功 能： N/A
* 类 名： DataItemService
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/8 14:41:27 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.Abstraction.Data;
using SF.Core.Data.Repository;
using SF.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Services
{
    public static class DataItemService
    {
        public static IQueryable<DataItemEntity> GetChildren(this IDataItemRepository repository, int id, int rootDataItemId)
        {
            var qry = repository.Query();

            if (id == 0)
            {
                if (rootDataItemId != 0)
                {
                    qry = qry.Where(a => a.ParentId == rootDataItemId);
                }
                else
                {
                    qry = qry.Where(a => a.ParentId == null || a.ParentId == 0);
                }
            }
            else
            {
                qry = qry.Where(a => a.ParentId == id);
            }
            return qry;
        }
    }
}
