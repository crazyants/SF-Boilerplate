using AutoMapper;
using SF.Core.Data;
using SF.Core.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using SF.Web.Common.Base.DataContractMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DataItem.Service
{
    public class DataItemService : IDataItemService
    {
        #region Fields
        private readonly ICrudDtoMapper<DataItemEntity, DataItemViewModel> _curdDtoMapper;
        private readonly IBaseUnitOfWork _baseUnitOfWork;
        #endregion

        #region Constructors
        public DataItemService(IBaseUnitOfWork baseUnitOfWork, ICrudDtoMapper<DataItemEntity, DataItemViewModel> curdDtoMapper)
        {
            _baseUnitOfWork = baseUnitOfWork;
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
        public IEnumerable<DataItemViewModel> GetChildren(int id, int rootDataItemId)
        {
            var qry = _baseUnitOfWork.BaseWorkArea.DataItem.Query();

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
            return _curdDtoMapper.MapEntityToDtos(qry.ToList());

        }
        #endregion
    }
}
