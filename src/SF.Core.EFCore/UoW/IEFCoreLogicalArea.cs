/*******************************************************************************
* 命名空间: SF.Core.EFCore.UoW
*
* 功 能： N/A
* 类 名： IEFCoreLogicalArea
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:57:32 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using SF.Core.Abstraction.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.EFCore.UoW
{
    /// <summary>
    /// Represents an area used to aggregate Unit of Work logic, 
    /// like data transformations or procedures, specialized for the Entity Framework Core.
    /// </summary>
    /// <typeparam name="TDbContext">The database context type</typeparam>
    public interface IEFCoreLogicalArea<out TDbContext> : ILogicalArea
        where TDbContext : DbContext
    {
        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        TDbContext Context { get; }

        /// <summary>
        /// Prepares an <see cref="IQueryable{T}"/> for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <returns>The <see cref="IQueryable{T}"/> for the specified entity type.</returns>
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;
    }

    /// <summary>
    /// Represents an area used to aggregate Unit of Work logic, 
    /// like data transformations or procedures, specialized for the Entity Framework Core.
    /// </summary>
    public interface IEFCoreLogicalArea : IEFCoreLogicalArea<DbContext>
    {

    }
}
