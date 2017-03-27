/*******************************************************************************
* 命名空间: SF.Core.Abstraction.UoW
*
* 功 能： N/A
* 类 名： IExposeQueryable
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:32:11 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Entitys.Abstraction.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SF.Core.Abstraction.UoW
{
    /// <summary>
    /// Offers the possibility of exposing an <see cref="IQueryable{T}"/>
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The entity id type</typeparam>
    public interface IExposeQueryable<TEntity, in TId>
        where TEntity : class
    {
        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <param name="query">The Sql String</param>
        /// <param name="parameters">The Sql Parameters</param>
        /// <returns></returns>
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> filtered by
        /// the entity id
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(TId id);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> 
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);

        /// <summary>
        ///  Gets an <see cref="IQueryable{TEntity}"/> 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<TEntity> QueryPage(
           Expression<Func<TEntity, bool>> predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           List<Expression<Func<TEntity, object>>> includes = null,
           int page = 0,
           int pageSize = 15);
        /// <summary>
        ///  Gets an <see cref="IQueryable{TEntity}"/> 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IPagedList<TEntity>> QueryPageAsync(
         Expression<Func<TEntity, bool>> predicate = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          List<Expression<Func<TEntity, object>>> includes = null,
          int page = 0,
          int pageSize = 15);

        /// <summary>
        ///  Gets an <see cref="IQueryable{TEntity}"/> 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IQueryable<TEntity> QueryFilter(
             Expression<Func<TEntity, bool>> predicate = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             List<Expression<Func<TEntity, object>>> includes = null,
             int? page = null,
             int? pageSize = null);
    }

    /// <summary>
    /// Offers the possibility of exposing an <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    public interface IExposeQueryable<TEntity, in TId01, in TId02>
        where TEntity : class
    {
        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> filtered by
        /// the entity id
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(TId01 id01, TId02 id02);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> 
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);
    }

    /// <summary>
    /// Offers the possibility of exposing an <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    public interface IExposeQueryable<TEntity, in TId01, in TId02, in TId03>
        where TEntity : class
    {
        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> filtered by
        /// the entity id
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third unique identifier value</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(TId01 id01, TId02 id02, TId03 id03);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> 
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);
    }

    /// <summary>
    /// Offers the possibility of exposing an <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    /// <typeparam name="TId04">The entity id fourth type</typeparam>
    public interface IExposeQueryable<TEntity, in TId01, in TId02, in TId03, in TId04>
        where TEntity : class
    {
        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> filtered by
        /// the entity id
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third unique identifier value</param>
        /// <param name="id04">The entity fourth unique identifier value</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(TId01 id01, TId02 id02, TId03 id03, TId04 id04);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> 
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);
    }

    /// <summary>
    /// Offers the possibility of exposing an <see cref="IQueryable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IExposeQueryable<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <param name="query">The Sql String</param>
        /// <param name="parameters">The Sql Parameters</param>
        /// <returns></returns>
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> filtered by
        /// the entity id
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryById(params object[] ids);

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> 
        /// that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>The <see cref="IQueryable{TEntity}"/> object</returns>
        IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch);


        IPagedList<TEntity> QueryPage(
           Expression<Func<TEntity, bool>> predicate = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           List<Expression<Func<TEntity, object>>> includes = null,
           int page = 0,
           int pageSize = 15);

        /// <summary>
        ///  Gets an <see cref="IQueryable{TEntity}"/> 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IQueryable<TEntity> QueryFilter(
             Expression<Func<TEntity, bool>> predicate = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             List<Expression<Func<TEntity, object>>> includes = null,
             int? page = null,
             int? pageSize = null);
    }

}
