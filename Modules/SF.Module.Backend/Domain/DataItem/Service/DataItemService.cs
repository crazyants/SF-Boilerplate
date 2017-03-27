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

namespace SF.Module.Backend.Domain.DataItem.Service
{
    public class DataItemService : ServiceBase, IDataItemService
    {
        #region Fields
        private readonly ICrudDtoMapper<DataItemEntity, DataItemViewModel, long> _curdDtoMapper;
        private readonly ICacheManager<object> _cacheManager;
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        #endregion

        #region Constructors
        public DataItemService(IBackendUnitOfWork backendUnitOfWork,
            ICacheManager<object> cacheManager,
            ICrudDtoMapper<DataItemEntity, DataItemViewModel, long> curdDtoMapper)
        {
            _backendUnitOfWork = backendUnitOfWork;
            _cacheManager = cacheManager;
            _curdDtoMapper = curdDtoMapper;

        }
        #endregion


        #region Method  
        /// <summary>
        /// 获取字典分类下级数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rootDataItemId"></param>
        /// <returns></returns>
        public List<DataItemViewModel> GetChildren(long id, int rootDataItemId)
        {
            var qry = GetAlls().AsQueryable();

            if (id == 0)
            {
                if (rootDataItemId != 0)
                {
                    qry = qry.Where(a => a.ParentId == rootDataItemId);
                }
                else
                {
                    qry = qry.Where(a => a.ParentId == null || a.ParentId == 0);
                }
            }
            else
            {
                qry = qry.Where(a => a.ParentId == id);
            }
            return _curdDtoMapper.MapEntityToDtos(qry.ToList()).ToList();

        }
        /// <summary>
        /// 获取所有字典分类（缓存）
        /// </summary>
        /// <returns></returns>
        public List<DataItemEntity> GetAlls()
        {
            return _cacheManager.Get(ConstHelper.DATAITEM_ALL, ConstHelper.Region, () =>
           {
               return _backendUnitOfWork.DataItem.Query().ToList();
           });

        }

        public IPagedList<DataItemEntity> GetPageListBykeyword(string keyword, int pageIndex, int pageSize)
        {
            
            var predicate = PredicateBuilder.New<DataItemEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                predicate.And(d => d.ItemName.Contains(keyword));
            }
            return _backendUnitOfWork.DataItem.QueryPage(predicate, page: pageIndex, pageSize: pageSize);
        }

        #endregion
    }
}
