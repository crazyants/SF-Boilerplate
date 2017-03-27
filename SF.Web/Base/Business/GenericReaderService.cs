using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SF.Core.Abstraction.Data;
using SF.Entitys.Abstraction;
using SF.Core.Abstraction.UoW;
using SF.Core.EFCore.UoW;
using SF.Core.QueryExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Base.Business
{
    /// <summary>
    /// 读取处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericReaderService<T, TKey> : IGenericReaderService<T, TKey> where T : BaseEntity<TKey>
        
    {
        #region Fields

        private readonly IEFCoreQueryableRepository<T, TKey> _repository;
        protected readonly ILogger _logger;

        #endregion

        #region Constructors
        public GenericReaderService(ILogger<Controller> logger, IEFCoreQueryableRepository<T, TKey> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        #endregion

        #region Utilities
        protected OrderBy<T> Ordering => new OrderBy<T>(query => query.OrderBy(a => a.SortIndex));
        #endregion

        #region Method

        /// <summary>
        /// 异步获取所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(_repository.QueryFilter(orderBy: Ordering.Expression));
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return _repository.QueryFilter(orderBy: Ordering.Expression);
        }
        /// <summary>
        /// 根据ID获取实体数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(TKey id)
        {
            return _repository.GetById(id);
        }
        /// <summary>
        /// 异步根据ID获取实体数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(TKey id)
        {
            return await _repository.GetByIdAsync(id);
        }
        #endregion


    }
}