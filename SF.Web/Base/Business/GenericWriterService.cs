
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SF.Entitys.Abstraction;
using SF.Core.Abstraction.UoW;
using SF.Core.Abstraction.UoW.Helper;
using SF.Core.EFCore.UoW;
using SF.Core.Errors.Exceptions;
using System;
using System.Threading.Tasks;

namespace SF.Web.Base.Business
{
    /// <summary>
    /// 写入处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericWriterService<T,TKey> : IGenericWriterService<T, TKey> where T : BaseEntity<TKey>
    {
        #region Fields
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IEFCoreQueryableRepository<T, TKey> _repository;
        protected readonly ILogger _logger;

        #endregion

        #region Constructors
        public GenericWriterService(ILogger<Controller> logger, IEFCoreQueryableRepository<T, TKey> repository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;

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
          
            await _unitOfWork.ExecuteAndCommitAsync(() =>
            {
                return _repository.AddAsync(entity);
            });
            return entity;
        }
        /// <summary>
        /// 异步更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new EntityNotFoundException(nameof(entity), 0);

            await _unitOfWork.ExecuteAndCommitAsync(() =>
            {
                return _repository.UpdateAsync(entity);
            });

        }
        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TKey id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new EntityNotFoundException(typeof(T).Name, id);
            await _unitOfWork.ExecuteAndCommitAsync(() =>
            {
                return _repository.DeleteAsync(entity);
            });
        }
        #endregion
    }
}
