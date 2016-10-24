
using SimpleFramework.Core.Abstraction.Entitys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Abstraction.Data.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        /// <summary>
        /// Gets or sets a value indicating whether database write operations
        /// originating from repositories should be committed immediately.
        /// </summary>
        bool AutoCommitEnabled { get; set; }
        int SaveChanges();
        void Dispose(bool disposing);
        IRepository<TEntity> Repository<TEntity>() where TEntity : IEntityWithTypedId<long>;
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        bool Commit();
        void Rollback();
        void SyncObjectsStatePreCommit();
        void SyncObjectsStatePostCommit();
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class;
    }
}
