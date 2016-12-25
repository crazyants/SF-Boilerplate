/*******************************************************************************
* 命名空间: SF.Core.Abstraction.UoW
*
* 功 能： N/A
* 类 名： ScopeEnabledUnitOfWork
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:38:41 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.Abstraction.UoW.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Core.Abstraction.UoW
{
    /// <summary>
    /// Represents a scope enabled <see cref="IUnitOfWork"/> that guarantees any given
    /// begin or commit logic is only invoqued once for any given scope. Every scope information is thread safe.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Id = {_privateId}, CurrentScope = {_currentScope}")]
    public abstract class ScopeEnabledUnitOfWork : IUnitOfWork
    {
        private int _currentScope;
        private readonly Guid _privateId = Guid.NewGuid();

        #region Implementation of IUnitOfWork

        /// <summary>
        /// Prepares the <see cref="IUnitOfWork"/> for work.
        /// </summary>
        public void Begin()
        {
            var s = IncrementScope();
            if (s == 1)
                OnBegin();
        }

        /// <summary>
        /// Commit the work made by this <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <exception cref="CommitException">
        /// Thrown when the work failed to commit
        /// </exception>
        /// <exception cref="ConcurrencyException">
        /// Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        public void Commit()
        {
            var s = DecrementScope();
            if (s < 0)
                throw new UndefinedScopeException(s);

            if (s != 0) return;

            try
            {
                OnCommit();
            }
            catch (UnitOfWorkException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CommitException(e);
            }
        }


        /// <summary>
        /// Asynchronously prepares the <see cref="IUnitOfWork"/> for work.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task to be awaited</returns>
        public async Task BeginAsync(CancellationToken ct = default(CancellationToken))
        {
            var s = IncrementScope();
            if (s == 1)
                await OnBeginAsync(ct);
        }


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
        public async Task CommitAsync(CancellationToken ct = default(CancellationToken))
        {
            var s = DecrementScope();
            if (s < 0)
                throw new UndefinedScopeException(s);

            if (s != 0) return;

            try
            {
                await OnCommitAsync(ct);
            }
            catch (UnitOfWorkException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CommitException(e);
            }
        }


        /// <summary>
        /// Prepares a given <see cref="IQueryable{T}"/> for asynchronous work.
        /// </summary>
        /// <typeparam name="T">The query item type</typeparam>
        /// <param name="queryable">The query to wrap</param>
        /// <returns>An <see cref="IAsyncQueryable{T}"/> instance, wrapping the given query</returns>
        public abstract IAsyncQueryable<T> PrepareAsyncQueryable<T>(IQueryable<T> queryable);

        #endregion

        /// <summary>
        /// Invoked once for any given scope, it should prepare the
        /// current instance for any subsequent work
        /// </summary>
        protected abstract void OnBegin();

        /// <summary>
        /// Invoked once for any given scope, it should commit any work
		/// made by this instance
        /// </summary>
        /// <exception cref="CommitException">
        /// Thrown when the work failed to commit
        /// </exception>
        /// <exception cref="ConcurrencyException">
        /// Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        protected abstract void OnCommit();


        /// <summary>
        /// Invoked once for any given scope, it should prepare the
        /// current instance for any subsequent work
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task for this operation</returns>
        protected abstract Task OnBeginAsync(CancellationToken ct);

        /// <summary>
        /// Invoked once for any given scope, it should commit any work
		/// made by this instance
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task for this operation</returns>
        /// <exception cref="CommitException">
        /// Thrown when the work failed to commit
        /// </exception>
        /// <exception cref="ConcurrencyException">
        /// Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        protected abstract Task OnCommitAsync(CancellationToken ct);

        #region Helpers

        private int DecrementScope()
        {
            return Interlocked.Decrement(ref _currentScope);
        }

        private int IncrementScope()
        {
            return Interlocked.Increment(ref _currentScope);
        }

        #endregion
    }
}
