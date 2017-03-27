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

namespace SF.Module.Backend.Domain.Department.Service
{
    public class DepartmentService : ServiceBase, IDepartmentService
    {
        #region Fields
        private readonly ICacheManager<object> _cacheManager;
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        #endregion

        #region Constructors
        public DepartmentService(IBackendUnitOfWork backendUnitOfWork,
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
        public async Task<List<DepartmentEntity>> GetAlls()
        {
            return await _cacheManager.GetAsync(ConstHelper.DEPARTMENT_ALL, ConstHelper.Region, () =>
            {
                return _backendUnitOfWork.Department.Query().ToListAsync();
            });

        }

        public async Task<IList<DepartmentEntity>> QueryFilterByParentId(long parentId)
        {
            return await _backendUnitOfWork.Department.QueryFilter(e => e.ParentId == parentId).ToListAsync();
        }

        public async Task<IList<long>> QueryFilterByParentIds(long[] parentIds)
        {
            return await _backendUnitOfWork.Department.QueryFilter(g =>
                    g.ParentId.HasValue &&
                    parentIds.Contains(g.ParentId.Value)).Select(g => g.ParentId.Value)
                    .Distinct()
                    .ToListAsync();
        }

        public async Task<IPagedList<DepartmentEntity>> GetPageListBykeyword(long parentId, string keyword, int pageIndex = 0, int pageSize = 100)
        {
            var predicate = PredicateBuilder.New<DepartmentEntity>();
            predicate.And(d => d.ParentId == parentId);
            if (!string.IsNullOrEmpty(keyword))
            {
                predicate.And(d => d.FullName.Contains(keyword));
                predicate.And(d => d.ShortName.Contains(keyword));
            }
            return await _backendUnitOfWork.Department.QueryPageAsync(predicate, page: pageIndex, pageSize: pageSize);
        }

        /// <summary>
        /// 获取字典分类下级数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rootDataItemId"></param>
        /// <returns></returns>
        public IPagedList<DepartmentEntity> GetByWhere(string keyWord, string condition, int pageIndex = 0, int recordsCount = 100)
        {
            var predicate = PredicateBuilder.New<DepartmentEntity>(true);

            #region 多条件查询
            if (!keyWord.IsEmpty())
            {
                switch (condition)
                {
                    case "FullName":        //公司名称
                        predicate.And(d => d.FullName == keyWord);
                        break;
                    case "EnCode":      //外文名称
                        predicate.And(d => d.EnCode == keyWord);
                        break;
                    case "ShortName": //中文名称
                        predicate.And(d => d.ShortName == keyWord);
                        break;
                    case "Manager": //负责人
                        predicate.And(d => d.Manager == keyWord);
                        break;
                    default:
                        break;

                }
            }
            #endregion

            return _backendUnitOfWork.Department.QueryPage(predicate, page: pageIndex, pageSize: recordsCount);

        }
        #endregion
    }
}
