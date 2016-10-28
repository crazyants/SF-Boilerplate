using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Abstraction.Entitys;
using SimpleFramework.Core.Abstraction.Data.UnitOfWork;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;


namespace SimpleFramework.Core.Data
{
    public class RepositoryWithTypedId<TEntity, TId> : IRepositoryWithTypedId<TEntity, TId> where TEntity : class, IEntityWithTypedId<TId>
    {
        #region Private Fields

        private readonly CoreDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWorkAsync _unitOfWork;

        #endregion Private Fields
        public RepositoryWithTypedId(CoreDbContext context, IUnitOfWorkAsync unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            if (_context != null)
            {
                _dbSet = _context.Set<TEntity>();
            }

        }
        public bool? AutoCommitEnabled { get; set; }

        private bool AutoCommitEnabledInternal
        {
            get
            {
                return this.AutoCommitEnabled ?? _unitOfWork.AutoCommitEnabled;
            }
        }
     

        protected DbSet<TEntity> DbSet { get { return _dbSet; } }

        public DbContext Context { get { return _context; } }

        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        public void SaveChange()
        {
            _unitOfWork.SaveChanges();
        }
       
      
        public virtual TEntity Find(params object[] keyValues) => _dbSet.Find(keyValues);

        public TEntity Find(Expression<Func<TEntity, bool>> predicate) => _dbSet.FirstOrDefault(predicate);

        public TEntity Find<TKey>(Expression<Func<TEntity, TKey>> sortExpression, bool isDesc, Expression<Func<TEntity, bool>> predicate) => isDesc ? _dbSet.OrderBy(sortExpression).FirstOrDefault(predicate) : _dbSet.OrderByDescending(sortExpression).FirstOrDefault(predicate);

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters) => _dbSet.FromSql(query, parameters).AsQueryable();

        public virtual void Insert(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _dbSet.Add(entity);
            if (this.AutoCommitEnabledInternal)
                _unitOfWork.SaveChanges();
            else
                _unitOfWork.SyncObjectState(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _dbSet.Attach(entity);
            if (this.AutoCommitEnabledInternal)
                _unitOfWork.SaveChanges();
            else
                _unitOfWork.SyncObjectState(entity);
        }

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _dbSet.Attach(entity);
            // _unitOfWork.SyncObjectState(entity);
            if (this.AutoCommitEnabledInternal)
                _unitOfWork.SaveChanges();
            else
                _unitOfWork.SyncObjectState(entity);
        }

        public virtual IQueryable<TEntity> GetAll() => _dbSet;

        public IQueryable<TEntity> Queryable() => _dbSet;


        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (predicate != null)
            {
                query = query.AsExpandable().Where(predicate);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }

        internal async Task<IEnumerable<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Select(predicate, orderBy, includes, page, pageSize).ToListAsync();
        }

        public virtual IQueryable<TEntity> Filter(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (predicate != null)
            {
                query = query.AsExpandable().Where(predicate);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }

      
    }
}
