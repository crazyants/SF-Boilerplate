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

namespace SF.Module.Backend.Domain.Area.Service
{
    public class AreaService : ServiceBase, IAreaService
    {
        #region Fields
        private readonly ICacheManager<object> _cacheManager;
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        #endregion

        #region Constructors
        public AreaService(IBackendUnitOfWork backendUnitOfWork,
            ICacheManager<object> cacheManager)
        {
            _backendUnitOfWork = backendUnitOfWork;
            _cacheManager = cacheManager;

        }
        #endregion


        #region Method  


        public async Task<IList<AreaEntity>> QueryFilterByParentId(long parentId)
        {
            return await _cacheManager.GetAsync(ConstHelper.AREA_PATTERN_KEY.FormatCurrent(parentId), ConstHelper.Region, () =>
            {
                return _backendUnitOfWork.Area.QueryFilter(e => e.ParentId == parentId).ToListAsync();
            });
        }

        public async Task<IList<long>> QueryFilterByParentIds(long[] parentIds)
        {
            return await _backendUnitOfWork.Area.QueryFilter(g =>
                    g.ParentId.HasValue &&
                    parentIds.Contains(g.ParentId.Value)).Select(g => g.ParentId.Value)
                    .Distinct()
                    .ToListAsync();
        }

        public async Task<IPagedList<AreaEntity>> GetPageListBykeyword(long parentId, string keyword, int pageIndex = 0, int pageSize = 100)
        {
            var predicate = PredicateBuilder.New<AreaEntity>();
            predicate.And(d => d.ParentId == parentId);
            if (!string.IsNullOrEmpty(keyword))
            {
                predicate.And(d => d.AreaCode.Contains(keyword));
                predicate.And(d => d.AreaName.Contains(keyword));
            }
            return await _backendUnitOfWork.Area.QueryPageAsync(predicate, page: pageIndex, pageSize: pageSize);
        }

        #endregion
    }
}
