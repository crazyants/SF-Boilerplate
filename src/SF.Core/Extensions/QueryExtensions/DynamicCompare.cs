/*******************************************************************************
* 命名空间: SF.Core.QueryExtensions.Extensions
*
* 功 能： N/A
* 类 名： DynamicCompare
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/19 13:28:26 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.QueryExtensions.Extensions
{
    /// <summary>
    /// Define a comparison method.
    /// 定义比较方法
    /// </summary>
    public enum DynamicCompare
    {
        /// <summary>
        /// A equal comparison.
        /// </summary>
        Equal,

        /// <summary>
        /// A not equal comparison.
        /// </summary>
        NotEqual,

        /// <summary>
        /// A greater than comparison.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// A greater than or equal comparison.
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// A less than comparison.
        /// </summary>
        LessThan,

        /// <summary>
        /// A less than or equal comparison.
        /// </summary>
        LessThanOrEqual
    }
}
