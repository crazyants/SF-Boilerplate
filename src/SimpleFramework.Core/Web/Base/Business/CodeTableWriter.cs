
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleFramework.Core.Abstraction.Entitys;
using SimpleFramework.Core.Abstraction.UoW;
using SimpleFramework.Core.Abstraction.UoW.Helper;
using SimpleFramework.Core.EFCore.UoW;
using SimpleFramework.Core.Errors.Exceptions;
using System;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Web.Base.Business
{
    /// <summary>
    /// 写入处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CodeTableWriter<T> : ICodetableWriter<T> where T : BaseEntity
    {
        #region Fields
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IEFCoreQueryableRepository<T> _repository;
        protected readonly ILogger _logger;

        #endregion

        #region Constructors
        public CodeTableWriter(ILogger<Controller> logger, IEFCoreQueryableRepository<T> repository, IUnitOfWork unitOfWork)
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
        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.ExecuteAndCommitAsync(() =>
            {
                var entity = _repository.GetById(id);
                if (entity == null) throw new EntityNotFoundException(typeof(T).Name, id);
                return _repository.DeleteAsync(entity);
            });
        }
        #endregion
    }
}
