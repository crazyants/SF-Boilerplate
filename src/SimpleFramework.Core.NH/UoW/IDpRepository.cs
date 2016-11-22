using SimpleFramework.Core.Abstraction.Entitys;
using SimpleFramework.Core.Abstraction.UoW;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Dapper.UoW
{
     /// <summary>
     /// Specialized interface of an <see cref="IQueryableRepository{TEntity,TId}"/> 
     /// for the NHibernate.
     /// </summary>
     /// <typeparam name="TEntity">The entity type</typeparam>
     /// <typeparam name="TKey">The entity id type</typeparam>
    public interface IDpRepository<TEntity, in TKey> : IQueryableRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        /// <summary>
        /// The database session object
        /// </summary>
        DbConnection Connection { get; }
    }
}
