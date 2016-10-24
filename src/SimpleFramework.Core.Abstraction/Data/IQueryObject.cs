using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Abstraction.Data
{
    public interface IQueryObject<TEntity>
    {
        Expression<Func<TEntity, bool>> Query();
        Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> query);
        Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> query);
        Expression<Func<TEntity, bool>> And(IQueryObject<TEntity> queryObject);
        Expression<Func<TEntity, bool>> Or(IQueryObject<TEntity> queryObject);
    }
}
