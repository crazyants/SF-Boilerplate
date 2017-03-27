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
using SF.Module.Backend.Domain.DataItemDetail.ViewModel;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Data.Uow;
using SF.Core.Abstraction.GenericServices;

namespace SF.Module.Backend.Domain.DataItemDetail.Service
{
    public class DataItemDetailService : ServiceBase, IDataItemDetailService
    {
        #region Fields
        private readonly ICrudDtoMapper<DataItemDetailEntity, DataItemDetailViewModel, long> _curdDtoMapper;
        private readonly ICacheManager<object> _cacheManager;
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        #endregion

        #region Constructors
        public DataItemDetailService(IBackendUnitOfWork backendUnitOfWork,
            ICacheManager<object> cacheManager,
            ICrudDtoMapper<DataItemDetailEntity, DataItemDetailViewModel, long> curdDtoMapper)
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
        public IPagedList<DataItemDetailEntity> GetByWhere(long itemId, string keyWord, string condition, int pageIndex = 0, int recordsCount = 10)
        {

            var predicate = PredicateBuilder.New<DataItemDetailEntity>();
            predicate.And(d => d.ItemId == itemId);

            #region 多条件查询
            if (!keyWord.IsEmpty())
            {
                switch (condition)
                {
                    case "ItemName":        //项目名
                        predicate.And(d => d.ItemName == keyWord);
                        break;
                    case "ItemValue":      //项目值
                        predicate.And(d => d.ItemValue == keyWord);
                        break;
                    case "SimpleSpelling": //拼音
                        predicate.And(d => d.SimpleSpelling == keyWord);
                        break;
                    default:
                        break;

                }
            }
            #endregion

            return _backendUnitOfWork.DataItemDetail.QueryPage(predicate, page: pageIndex, pageSize: recordsCount);

        }

        public IList<DataItemDetailEntity> GetByItemId(long itemId)
        {
            var predicate = PredicateBuilder.New<DataItemDetailEntity>();
            predicate.And(d => d.ItemId == itemId);
            var key = ConstHelper.DATAITEMDETAIL_ITEMID_ALL.FormatCurrent(itemId);
            return _cacheManager.Get(key, ConstHelper.Region, () =>
           {
               return _backendUnitOfWork.DataItemDetail.QueryFilter(predicate).ToList();
           });
        }

        public IList<DataItemDetailEntity> GetByItemCode(string itemCode)
        {
            var predicate = PredicateBuilder.New<DataItemDetailEntity>();
            predicate.And(d => d.ItemCode == itemCode);
            var key = ConstHelper.DATAITEMDETAIL_ITEMCODE_ALL.FormatCurrent(itemCode);
            return _cacheManager.Get(key, ConstHelper.Region, () =>
            {
                return _backendUnitOfWork.DataItemDetail.Query()
                  .Join(_backendUnitOfWork.DataItem.Query(), x => x.ItemId, y => y.Id, (x, y) => new { DataItemDetail = x, DataItem = y })
                  .Where(z => z.DataItem.ItemCode == itemCode).Select(z => z.DataItemDetail).ToList();
            });

        }
        /// <summary>
        /// 获取所有字典分类（缓存）
        /// </summary>
        /// <returns></returns>
        public List<DataItemDetailEntity> GetAlls()
        {
            return _cacheManager.Get(ConstHelper.DATAITEMDETAIL_ALL, ConstHelper.Region, () =>
          {
              return _backendUnitOfWork.DataItemDetail.Query().ToList();
          });

        }

        #endregion
    }
}
