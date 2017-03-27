/*******************************************************************************
* 命名空间: SF.Web.Filters
*
* 功 能： N/A
* 类 名： FilterSql
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/24 10:52:52 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Web.Filters
{
    public class FilterSql
    {
        public List<FilterCriteria> Criteria { get; set; }
        public List<object> Values { get; set; }
        public StringBuilder WhereClause { get; set; }

        public override string ToString()
        {
            if (WhereClause != null && Values != null)
                return $"{WhereClause.ToString()}, { string.Join(", ", Values.ToArray())}";
            return "";
        }
    }

}
