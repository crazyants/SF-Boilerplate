/*******************************************************************************
* 命名空间: SF.Core.EFCore.UoW
*
* 功 能： N/A
* 类 名： IEFCoreQueryableRepository
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:58:23 疯狂蚂蚁 初版
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
    /// Specialized interface of an <see cref="IQueryableRepository{TEntity,TId}"/> 
    /// for the Entity Framework Core.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey">The entity id type</typeparam>
    public interface IEFCoreQueryableRepository<TEntity, in TKey>
        : IQueryableRepository<TEntity, TKey>
        where TEntity : class
    {
        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// The <see cref="DbSet{TEntity}"/> of this repository entity
        /// </summary>
        DbSet<TEntity> Set { get; }
    }

    /// <summary>
    /// Specialized interface of an <see cref="IQueryableRepository{TEntity,TId01,TId02}"/> 
    /// for the Entity Framework Core.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey01">The entity id first type</typeparam>
    /// <typeparam name="TKey02">The entity id second type</typeparam>
    public interface IEFCoreQueryableRepository<TEntity, in TKey01, in TKey02>
        : IQueryableRepository<TEntity, TKey01, TKey02>
        where TEntity : class
    {
        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// The <see cref="DbSet{TEntity}"/> of this repository entity
        /// </summary>
        DbSet<TEntity> Set { get; }
    }

    /// <summary>
    /// Specialized interface of an <see cref="IQueryableRepository{TEntity,TId01,TId02,TId03}"/> 
    /// for the Entity Framework Core.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey01">The entity id first type</typeparam>
    /// <typeparam name="TKey02">The entity id second type</typeparam>
    /// <typeparam name="TKey03">The entity id third type</typeparam>
    public interface IEFCoreQueryableRepository<TEntity, in TKey01, in TKey02, in TKey03>
        : IQueryableRepository<TEntity, TKey01, TKey02, TKey03>
        where TEntity : class
    {
        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// The <see cref="DbSet{TEntity}"/> of this repository entity
        /// </summary>
        DbSet<TEntity> Set { get; }
    }

    /// <summary>
    /// Specialized interface of an <see cref="IQueryableRepository{TEntity,TId01,TId02,TId03,TId04}"/> 
    /// for the Entity Framework Core.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey01">The entity id first type</typeparam>
    /// <typeparam name="TKey02">The entity id second type</typeparam>
    /// <typeparam name="TKey03">The entity id third type</typeparam>
    /// <typeparam name="TKey04">The entity id fourth type</typeparam>
    public interface IEFCoreQueryableRepository<TEntity, in TKey01, in TKey02, in TKey03, in TKey04>
        : IQueryableRepository<TEntity, TKey01, TKey02, TKey03, TKey04>
        where TEntity : class
    {
        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// The <see cref="DbSet{TEntity}"/> of this repository entity
        /// </summary>
        DbSet<TEntity> Set { get; }
    }

    /// <summary>
    /// Specialized interface of an <see cref="IQueryableRepository{TEntity}"/> 
    /// for the Entity Framework Core.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IEFCoreQueryableRepository<TEntity> : IQueryableRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// The <see cref="DbSet{TEntity}"/> of this repository entity
        /// </summary>
        DbSet<TEntity> Set { get; }
    }
}
