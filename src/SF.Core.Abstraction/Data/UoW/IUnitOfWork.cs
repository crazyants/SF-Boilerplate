/*******************************************************************************
* 命名空间: SF.Core.Abstraction.UoW
*
* 功 能： N/A
* 类 名： IUnitOfWork
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:37:38 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Core.Abstraction.UoW
{

    /// <summary>
    /// Interface representing an unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Prepares the <see cref="IUnitOfWork"/> for work.
        /// </summary>
        void Begin();

        /// <summary>
        /// Commit the work made by this <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <exception cref="CommitException">
        /// Thrown when the work failed to commit
        /// </exception>
        /// <exception cref="ConcurrencyException">
        /// Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        void Commit();

 
        /// <summary>
        /// Asynchronously prepares the <see cref="IUnitOfWork"/> for work.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        Task BeginAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Asynchronously commit the work made by this <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        /// <exception cref="CommitException">
        /// Thrown when the work failed to commit
        /// </exception>
        /// <exception cref="ConcurrencyException">
        /// Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        Task CommitAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Prepares a given <see cref="IQueryable{T}"/> for asynchronous work.
        /// </summary>
        /// <typeparam name="T">The query item type</typeparam>
        /// <param name="queryable">The query to wrap</param>
        /// <returns>An <see cref="IAsyncQueryable{T}"/> instance, wrapping the given query</returns>
        IAsyncQueryable<T> PrepareAsyncQueryable<T>(IQueryable<T> queryable);

 
    }
}
