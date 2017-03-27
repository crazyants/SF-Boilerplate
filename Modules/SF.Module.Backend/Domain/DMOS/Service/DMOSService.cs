using AutoMapper;
using CacheManager.Core;
using SF.Data;
using SF.Entitys;
using SF.Module.Backend.Common;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using SF.Web.Base.DataContractMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SF.Core.Extensions;
using System.Linq.Expressions;
using LinqKit;
using SF.Entitys.Abstraction.Pages;
using Microsoft.EntityFrameworkCore;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Data.Uow;
using SF.Core.Abstraction.GenericServices;

namespace SF.Module.Backend.Domain.DMOS.Service
{
    public class DMOSService : ServiceBase, IDMOSService
    {
        #region Fields
        private readonly ICacheManager<object> _cacheManager;
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        #endregion

        #region Constructors
        public DMOSService(IBackendUnitOfWork backendUnitOfWork,
            ICacheManager<object> cacheManager)
        {
            _backendUnitOfWork = backendUnitOfWork;
            _cacheManager = cacheManager;

        }
        #endregion


        #region Method  
        /// <summary>
        /// 获取所有机构（缓存）
        /// </summary>
        /// <returns></returns>
        public async Task<List<DMOSEntity>> GetAllsAsync()
        {
            return await _cacheManager.GetAsync(ConstHelper.DMOS_CATEGORYTYPE_ALL, ConstHelper.Region, () =>
            {
                return _backendUnitOfWork.DMOS.Query().ToListAsync();
            });

        }

        public async Task<IList<DMOSEntity>> QueryFilterByCategoryAsync(long category,  string organizeId = null)
        {
            var predicate = PredicateBuilder.New<DMOSEntity>();
            predicate.And(d => d.Category == category);
            if (!string.IsNullOrEmpty(organizeId))
            {
                var oid = organizeId.AsLong();
                predicate.And(d => d.OrganizeId == oid);
            }
            return await _backendUnitOfWork.DMOS.QueryFilter(predicate).ToListAsync();
        }

        public async Task<IPagedList<DMOSEntity>> GetPageListBykeywordAsync(long category, string keyword, int pageIndex = 0, int pageSize = 100)
        {
            var predicate = PredicateBuilder.New<DMOSEntity>();
            predicate.And(d => d.Category == category);
            if (!string.IsNullOrEmpty(keyword))
            {
                predicate.And(d => d.FullName.Contains(keyword));
                predicate.And(d => d.EnCode.Contains(keyword));
            }
            return await _backendUnitOfWork.DMOS.QueryPageAsync(predicate, page: pageIndex, pageSize: pageSize);
        }

        /// <summary>
        /// 获取字典分类下级数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rootDataItemId"></param>
        /// <returns></returns>
        public async Task<IPagedList<DMOSEntity>> GetByWhere(long category, string keyWord, string condition, int pageIndex = 0, int recordsCount = 100)
        {
            var predicate = PredicateBuilder.New<DMOSEntity>(true);
            predicate.And(d => d.Category == category);
            #region 多条件查询
            if (!keyWord.IsEmpty())
            {
                switch (condition)
                {
                    case "FullName":        //名称
                        predicate.And(d => d.FullName == keyWord);
                        break;
                    case "EnCode":      //编号
                        predicate.And(d => d.EnCode == keyWord);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            return await _backendUnitOfWork.DMOS.QueryPageAsync(predicate, page: pageIndex, pageSize: recordsCount);

        }
        #endregion
    }
}
