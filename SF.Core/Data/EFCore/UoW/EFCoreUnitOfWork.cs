/*******************************************************************************
* 命名空间: SF.Core.EFCore.UoW
*
* 功 能： N/A
* 类 名： EFCoreUnitOfWork
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:59:21 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using SF.Core.Abstraction;
using SF.Core.Abstraction.Interceptors;
using SF.Core.Abstraction.UoW;
using SF.Core.Abstraction.UoW.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Core.EFCore.UoW
{
    /// <summary>
    /// An implementation compatible with Entity Framework Core for the Unit of Work pattern.
    /// Underline, it also uses work scopes (see: <see cref="ScopeEnabledUnitOfWork"/>).
    /// </summary>
    /// <typeparam name="TDbContext">The database context type</typeparam>
    public abstract class EFCoreUnitOfWork<TDbContext> : ScopeEnabledUnitOfWork, IEFCoreUnitOfWork<TDbContext>, IDisposable
        where TDbContext : DbContext
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context">The database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected EFCoreUnitOfWork(TDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            Context = context;
            this.AutoCommitEnabled = true;
        }
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context">The database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected EFCoreUnitOfWork(TDbContext context, IEnumerable<IInterceptor> interceptors)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            Context = context;
            _interceptors = interceptors;
            this.AutoCommitEnabled = true;
        }
        /// <summary>
        /// Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~EFCoreUnitOfWork()
        {
            Dispose(false);
        }

        #region Implementation of IEFCoreUnitOfWork<out TDbContext>

        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        public TDbContext Context { get; set; }

        private readonly IEnumerable<IInterceptor> _interceptors;

        private bool _disposed;

        public bool AutoCommitEnabled { get; set; }
        InterceptionContext intercept;

        #endregion

        #region Overrides of ScopeEnabledUnitOfWork

        /// <summary>
        /// Prepares a given <see cref="T:System.Linq.IQueryable`1" /> for asynchronous work.
        /// </summary>
        /// <typeparam name="T">The query item type</typeparam>
        /// <param name="queryable">The query to wrap</param>
        /// <returns>An <see cref="T:SimplePersistence.UoW.IAsyncQueryable`1" /> instance, wrapping the given query</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public override IAsyncQueryable<T> PrepareAsyncQueryable<T>(IQueryable<T> queryable)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            return new EFCoreAsyncQueryable<T>(queryable);
        }

        #endregion

        #region Overrides of ScopeEnabledUnitOfWork

        /// <summary>
        /// Invoked once for any given scope, it should prepare the
        /// current instance for any subsequent work
        /// </summary>
        protected override void OnBegin()
        {
          
        }

        /// <summary>
        /// Invoked once for any given scope, it should commit any work
        /// made by this instance
        /// </summary>
        /// <exception cref="T:SimplePersistence.UoW.Exceptions.CommitException">
        /// Thrown when the work failed to commit
        /// </exception>
        /// <exception cref="T:SimplePersistence.UoW.Exceptions.ConcurrencyException">
        /// Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        protected override void OnCommit()
        {
            try
            {
                var entries = Context.ChangeTracker.Entries().ToList();
                var entriesByState = entries.ToLookup(row => row.State);
                var processInterceptors = _interceptors != null;

                intercept = null;

                if (_interceptors != null)
                {
                    intercept = new InterceptionContext(_interceptors.ToArray())
                    {
                        DatabaseContext = Context,
                        ChangeTracker = Context.ChangeTracker,
                        Entries = entries,
                        EntriesByState = entriesByState,
                    };
                }

                if (intercept != null)
                {
                    intercept.Before();
                }
                SyncObjectsStatePreCommit();
                var changes = Context.SaveChanges();
                SyncObjectsStatePostCommit();
                if (intercept != null)
                {
                    intercept.After();
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyException(e);
            }
        }

        /// <summary>
        /// Invoked once for any given scope, it should prepare the
        /// current instance for any subsequent work
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task for this operation</returns>
        protected override Task OnBeginAsync(CancellationToken ct)
        {
           
            return Task.FromResult(true);
        }

        /// <summary>
        /// Invoked once for any given scope, it should commit any work
        /// made by this instance
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The task for this operation</returns>
        /// <exception cref="T:SimplePersistence.UoW.Exceptions.CommitException">
        /// Thrown when the work failed to commit
        /// </exception>
        /// <exception cref="T:SimplePersistence.UoW.Exceptions.ConcurrencyException">
        /// Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        protected override async Task OnCommitAsync(CancellationToken ct)
        {
            try
            {
                var entries = Context.ChangeTracker.Entries().ToList();
                var entriesByState = entries.ToLookup(row => row.State);
                var processInterceptors = _interceptors != null;

                intercept = null;

                if (_interceptors != null)
                {
                    intercept = new InterceptionContext(_interceptors.ToArray())
                    {
                        DatabaseContext = Context,
                        ChangeTracker = Context.ChangeTracker,
                        Entries = entries,
                        EntriesByState = entriesByState,
                    };
                }

                if (intercept != null)
                {
                    intercept.Before();
                }
                SyncObjectsStatePreCommit();
                await Context.SaveChangesAsync(ct);
                SyncObjectsStatePostCommit();
                if (intercept != null)
                {
                    intercept.After();
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyException(e);
            }
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the EF database context
        /// </summary>
        /// <param name="disposing">Disposes if true, else does nothing</param>
        protected virtual void Dispose(bool disposing)
        {
           // Context.Dispose();

            if (_disposed)
                return;

            if (disposing)
            {
                try
                {
                    var connection = Context.Database.GetDbConnection();
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                catch (ObjectDisposedException)
                {
                    // 
                }

                if (Context != null)
                {
                    //Context.Dispose();
                    Context = null;
                }
            }
            _disposed = true;
        }

        #endregion

        #region State
        public void SyncObjectsStatePreCommit()
        {
            foreach (var dbEntityEntry in Context.ChangeTracker.Entries())
            {
                dbEntityEntry.State = StateHelper.ConvertState(dbEntityEntry.State);
            }
        }

        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in Context.ChangeTracker.Entries())
            {
                dbEntityEntry.State = StateHelper.ConvertState(dbEntityEntry.State);
            }
        }
        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class
        {
            Context.Entry(entity).State = StateHelper.ConvertState(Context.Entry(entity).State);
        }
        #endregion
    }

    /// <summary>
    /// An implementation compatible with Entity Framework Core for the Unit of Work pattern.
    /// Underline, it also uses work scopes (see: <see cref="ScopeEnabledUnitOfWork"/>).
    /// </summary>
    public abstract class EFCoreUnitOfWork : EFCoreUnitOfWork<DbContext>, IEFCoreUnitOfWork
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context">The database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected EFCoreUnitOfWork(DbContext context) : base(context)
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context">The database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected EFCoreUnitOfWork(DbContext context, IEnumerable<IInterceptor> interceptors) : base(context, interceptors)
        {

        }
    }
}
