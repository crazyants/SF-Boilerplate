/*******************************************************************************
* 命名空间: SF.Core.EFCore.UoW
*
* 功 能： N/A
* 类 名： IEFCoreWorkArea
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:56:18 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using SF.Core.Abstraction.UoW;
 

namespace SF.Core.EFCore.UoW
{
    /// <summary>
    /// Represents a work area that can be used for aggregating
    /// UoW properties, specialized for the Entity Framework Core
    /// </summary>
    /// <typeparam name="TDbContext">The database context type</typeparam>
    public interface IEFCoreWorkArea<out TDbContext> : IWorkArea
        where TDbContext : DbContext
    {
        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        TDbContext Context { get; }
    }

    /// <summary>
    /// Represents a work area that can be used for aggregating
    /// UoW properties, specialized for the Entity Framework Core
    /// </summary>
    public interface IEFCoreWorkArea : IEFCoreWorkArea<DbContext>
    {

    }
}
