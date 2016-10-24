using SimpleFramework.Core.Abstraction.Entitys;
using System;
using System.Linq.Expressions;

namespace SimpleFramework.Core.Abstraction.Data
{
    public interface IRepository<TEntity> : IRepositoryWithTypedId<TEntity, long> where TEntity : IEntityWithTypedId<long>
    {
        IRepository<TEntity> GetRepository<T>() where T : IEntityWithTypedId<long>;
        IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject);
        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);
        IQueryFluent<TEntity> Query();
    }
}
