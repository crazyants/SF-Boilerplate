
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleFramework.Core.Abstraction.Data.UnitOfWork;
using SimpleFramework.Core.Abstraction.Entitys;
using SimpleFramework.Core.Abstraction.Data;
using Microsoft.Practices.ServiceLocation;
using SimpleFramework.Core.Interceptors;

namespace SimpleFramework.Core.Data
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        #region Private Fields
        private bool _disposed;
        private DbContext _context;
        private IDbContextTransaction _transaction;
        private Dictionary<string, dynamic> _repositories;
        private readonly IInterceptor[] _interceptors;
        #endregion Private Fields

        #region Constuctor/Dispose

        public UnitOfWork(DbContext context,params IInterceptor[] interceptors)
        {
            _context = context;
            _interceptors = interceptors;
            _repositories = new Dictionary<string, dynamic>();
            this.AutoCommitEnabled = true;

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                try
                {
                    var connection = _context.Database.GetDbConnection();
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                catch (ObjectDisposedException)
                {
                    // 
                }

                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
            _disposed = true;
        }

        public bool AutoCommitEnabled { get; set; }

        #endregion Constuctor/Dispose

        public int SaveChanges()
        {

            var entries = _context.ChangeTracker.Entries().ToList();
            var entriesByState = entries.ToLookup(row => row.State);
            var processInterceptors = _interceptors != null;

            InterceptionContext intercept = null;

            if (_interceptors != null)
            {
                intercept = new InterceptionContext(_interceptors)
                {
                    DatabaseContext = _context,
                    ChangeTracker = _context.ChangeTracker,
                    Entries = entries,
                    EntriesByState= entriesByState,
                };
            }

            if (intercept != null)
            {
                intercept.Before();
            }
            SyncObjectsStatePreCommit();
            var changes = _context.SaveChanges();
            SyncObjectsStatePostCommit();

            if (intercept != null)
            {
                intercept.After();
            }

            return changes;

        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : IEntityWithTypedId<long>
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepository<TEntity>>();
            }

            return RepositoryAsync<TEntity>();
        }

        public async Task<int> SaveChangesAsync() => await this.SaveChangesAsync(CancellationToken.None);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var entries = _context.ChangeTracker.Entries().ToList();
            var entriesByState = entries.ToLookup(row => row.State);
            var processInterceptors = _interceptors != null;

            InterceptionContext intercept = null;

            if (_interceptors != null)
            {
                intercept = new InterceptionContext(_interceptors)
                {
                    DatabaseContext = _context,
                    ChangeTracker = _context.ChangeTracker,
                    Entries = entries,
                    EntriesByState = entriesByState,
                };
            }

            if (intercept != null)
            {
                intercept.Before();
            }
            SyncObjectsStatePreCommit();
            var changesAsync = await _context.SaveChangesAsync(cancellationToken);
            SyncObjectsStatePostCommit();

            if (intercept != null)
            {
                intercept.After();
            }

            return changesAsync;
        }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : IEntityWithTypedId<long>
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepositoryAsync<TEntity>>();
            }

            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepositoryAsync<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);

            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context, this));

            return _repositories[type];
        }

        #region Unit of Work Transactions

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            _transaction = _context.Database.BeginTransaction(isolationLevel);
        }

        public bool Commit()
        {
            _transaction.Commit();
            return true;
        }

        public void Rollback()
        {
            _transaction.Rollback();
            SyncObjectsStatePostCommit();
        }
        #endregion

        #region State
        public void SyncObjectsStatePreCommit()
        {
            foreach (var dbEntityEntry in _context.ChangeTracker.Entries())
            {
                dbEntityEntry.State = StateHelper.ConvertState(dbEntityEntry.State);
            }
        }

        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in _context.ChangeTracker.Entries())
            {
                dbEntityEntry.State = StateHelper.ConvertState(dbEntityEntry.State);
            }
        }
        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Entry(entity).State = StateHelper.ConvertState(_context.Entry(entity).State);
        }
        #endregion
    }
}
