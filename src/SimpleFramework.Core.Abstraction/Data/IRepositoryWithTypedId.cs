using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleFramework.Core.Abstraction.Entitys;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SimpleFramework.Core.Abstraction.Data
{
    public interface IRepositoryWithTypedId<TEntity, in TId> where TEntity : IEntityWithTypedId<TId>
    {
        DbContext Context { get; }
        IDbContextTransaction BeginTransaction();
        void SaveChange();

        TEntity Find(params object[] keyValues);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Find<TKey>(Expression<Func<TEntity, TKey>> sortExpression, bool isDesc, Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);

        IQueryable<TEntity> Filter(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null);

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Queryable();

    }
}
