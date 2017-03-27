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

namespace SF.Module.Backend.Domain.Organize.Service
{
    public class OrganizeService : ServiceBase, IOrganizeService
    {
        #region Fields
        private readonly ICacheManager<object> _cacheManager;
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        #endregion

        #region Constructors
        public OrganizeService(IBackendUnitOfWork backendUnitOfWork,
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
        public async Task<List<OrganizeEntity>> GetAlls()
        {
            return await _cacheManager.GetAsync(ConstHelper.ORGANIZE_ALL, ConstHelper.Region, () =>
            {
                return _backendUnitOfWork.Organize.Query().ToListAsync();
            });

        }

        public async Task<IList<OrganizeEntity>> QueryFilterByParentId(long parentId)
        {
            return await _backendUnitOfWork.Organize.QueryFilter(e => e.ParentId == parentId).ToListAsync();
        }

        public async Task<IList<long>> QueryFilterByParentIds(long[] parentIds)
        {
            return await _backendUnitOfWork.Organize.QueryFilter(g =>
                    g.ParentId.HasValue &&
                    parentIds.Contains(g.ParentId.Value)).Select(g => g.ParentId.Value)
                    .Distinct()
                    .ToListAsync();
        }

        public async Task<IPagedList<OrganizeEntity>> GetPageListBykeyword(long parentId, string keyword, int pageIndex = 0, int pageSize = 100)
        {
            var predicate = PredicateBuilder.New<OrganizeEntity>();
            predicate.And(d => d.ParentId == parentId);
            if (!string.IsNullOrEmpty(keyword))
            {
                predicate.And(d => d.FullName.Contains(keyword));
                predicate.And(d => d.ShortName.Contains(keyword));
            }
            return await _backendUnitOfWork.Organize.QueryPageAsync(predicate, page: pageIndex, pageSize: pageSize);
        }

        /// <summary>
        /// 获取字典分类下级数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rootDataItemId"></param>
        /// <returns></returns>
        public IPagedList<OrganizeEntity> GetByWhere(string keyWord, string condition, int pageIndex = 0, int recordsCount = 100)
        {
            var predicate = PredicateBuilder.New<OrganizeEntity>(true);

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

            return _backendUnitOfWork.Organize.QueryPage(predicate, page: pageIndex, pageSize: recordsCount);

        }
        #endregion
    }
}
