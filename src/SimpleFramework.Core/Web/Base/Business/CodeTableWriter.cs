
using Microsoft.Extensions.Logging;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Abstraction.Data.UnitOfWork;
using SimpleFramework.Core.Abstraction.Entitys;
using SimpleFramework.Core.Errors.Exceptions;
using System;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Web.Base.Business
{
    /// <summary>
    /// 写入处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CodeTabelWriter<T> : ICodetableWriter<T> where T : EntityBase
    {
        #region Fields
        protected readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IRepositoryAsync<T> _repository;
        protected readonly ILogger _logger;

        #endregion

        #region Constructors
        public CodeTabelWriter(ILogger<T> logger, IRepositoryAsync<T> repository, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _logger = logger;
            _repository = repository;
            _unitOfWorkAsync = unitOfWorkAsync;
            unitOfWorkAsync.AutoCommitEnabled = false;
        }

        #endregion

        #region Utilities
        #endregion

        #region Method
        /// <summary>
        /// 异步新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null) throw new ArgumentException("No codetable provided", nameof(entity));

            using (var uow = _unitOfWorkAsync)
            {
                IRepository<T> repo = uow.Repository<T>();
                repo.Insert(entity);
                await uow.SaveChangesAsync();
                return entity;
            }

        }
        /// <summary>
        /// 异步更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new EntityNotFoundException(nameof(entity), 0);
            using (var uow = _unitOfWorkAsync)
            {
                IRepository<T> repo = uow.Repository<T>();
                repo.Update(entity);
                await uow.SaveChangesAsync();

            }

        }
        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            using (var uow = _unitOfWorkAsync)
            {
                IRepositoryAsync<T> repo = uow.RepositoryAsync<T>();
                var entity = await repo.FindAsync(id);
                if (entity == null) throw new EntityNotFoundException(typeof(T).Name, id);
                repo.Delete(entity);
                await uow.SaveChangesAsync();
            }
        }
        #endregion
    }
}
