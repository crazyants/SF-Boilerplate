using Microsoft.EntityFrameworkCore;
using SimpleFramework.Infrastructure.Data;
using SimpleFramework.Infrastructure.Data.UnitOfWork;
using SimpleFramework.Infrastructure.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Data
{
    public class Repository<TEntity> : RepositoryWithTypedId<TEntity, long>, IRepository<TEntity>
       where TEntity : Entity
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        public Repository(SimpleDbContext context, IUnitOfWorkAsync unitOfWork) : base(context, unitOfWork)
        {

        }
        public IQueryFluent<TEntity> Query() => new QueryFluent<TEntity>(this);

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject) => new QueryFluent<TEntity>(this, queryObject);

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query) => new QueryFluent<TEntity>(this, query);

        public IRepository<TEntity> GetRepository<T>() where T : IEntityWithTypedId<long> => _unitOfWork.Repository<TEntity>();

     
    }
}
