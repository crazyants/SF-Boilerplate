using AutoMapper;
using CacheManager.Core;
using SF.Core.Data;
using SF.Core.Entitys;
using SF.Module.Backend.Common;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using SF.Web.Common.Base.DataContractMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SF.Core.Extensions;
using System.Linq.Expressions;
using LinqKit;
using SF.Core.Entitys.Abstraction.Pages;
using Microsoft.EntityFrameworkCore;

namespace SF.Module.Backend.Domain.DataItem.Service
{
    public class DataItemService : IDataItemService
    {
        #region Fields
        private readonly ICrudDtoMapper<DataItemEntity, DataItemViewModel> _curdDtoMapper;
        private readonly ICacheManager<object> _cacheManager;
        private readonly IBaseUnitOfWork _baseUnitOfWork;
        #endregion

        #region Constructors
        public DataItemService(IBaseUnitOfWork baseUnitOfWork,
            ICacheManager<object> cacheManager,
            ICrudDtoMapper<DataItemEntity, DataItemViewModel> curdDtoMapper)
        {
            _baseUnitOfWork = baseUnitOfWork;
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
        public List<DataItemViewModel> GetChildren(int id, int rootDataItemId)
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
            return _cacheManager.Get(ConstHelper.DATAITEM_ALL, ConstHelper.DATAITEM_ALL, () =>
           {
               return _baseUnitOfWork.BaseWorkArea.DataItem.Query().ToList();
           });

        }

        public IPagedList<DataItemEntity> GetPageListBykeyword(string keyword, int pageIndex, int pageSize)
        {
            Expression<Func<DataItemEntity, bool>> pc = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                pc = d => d.ItemName.Contains(keyword);
            }
            return _baseUnitOfWork.BaseWorkArea.DataItem.QueryPage(pc, page: pageIndex, pageSize: pageSize);
        }

        #endregion
    }
}
