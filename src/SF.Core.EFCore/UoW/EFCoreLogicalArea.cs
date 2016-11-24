/*******************************************************************************
* 命名空间: SF.Core.EFCore.UoW
*
* 功 能： N/A
* 类 名： EFCoreLogicalArea
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:56:52 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace SF.Core.EFCore.UoW
{
    /// <summary>
    /// Represents an area used to aggregate Unit of Work logic, 
    /// like data transformations or procedures, specialized for the Entity Framework Core.
    /// </summary>
    /// <typeparam name="TDbContext">The database context type</typeparam>
    public abstract class EFCoreLogicalArea<TDbContext> : IEFCoreLogicalArea<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context">The database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected EFCoreLogicalArea(TDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            Context = context;
        }

        #region Implementation of IEFCoreLogicalArea<out TDbContext>

        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        public TDbContext Context { get; }

        /// <summary>
        /// Prepares an <see cref="IQueryable{T}"/> for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <returns>The <see cref="IQueryable{T}"/> for the specified entity type.</returns>
        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }

        #endregion
    }

    /// <summary>
    /// Represents an area used to aggregate Unit of Work logic, 
    /// like data transformations or procedures, specialized for the Entity Framework Core.
    /// </summary>
    public abstract class EFCoreLogicalArea : EFCoreLogicalArea<DbContext>, IEFCoreLogicalArea
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context">The database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected EFCoreLogicalArea(DbContext context) : base(context)
        {

        }
    }
}
