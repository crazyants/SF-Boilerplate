using LinqKit;
using Microsoft.EntityFrameworkCore;
using SF.Core.Abstraction.Domain;
using SF.Data;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Data.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DataItemDetail.Rule
{
    /// <summary>
    /// 字典业务规则处理
    /// </summary>
    public class DataItemDetailRules : BaseRules<DataItemDetailEntity>, IDataItemDetailRules
    {
        private readonly IBackendUnitOfWork _backendUnitOfWork;

        public DataItemDetailRules(IBackendUnitOfWork backendUnitOfWork)
        {
            _backendUnitOfWork = backendUnitOfWork;
        }

        public bool DoesDataItemDetailExist(long id)
        {
            var app = _backendUnitOfWork.DataItem.GetById(id);
            return app != null && app.DeleteMark != (int)DeleteStatus.Deleted;
        }

        public bool IsDataItemDetailIdUnique(long id)
        {
            return _backendUnitOfWork.DataItemDetail.Exists(id);

        }

        public bool IsDataItemDetailValueUnique(string itemValue, long itemId, long dataItemId = 0)
        {
            var predicate = PredicateBuilder.New<DataItemDetailEntity>();
            predicate.And(d => d.ItemValue == itemValue);
            predicate.And(d => d.ItemId != itemId);
            if (dataItemId != 0)
            {
                predicate.And(d => d.Id != dataItemId);
            }
            return _backendUnitOfWork.DataItemDetail.Query().AsNoTracking().Any(predicate);

        }

        public bool IsDataItemDetailNameUnique(string name, long itemId, long dataItemId = 0)
        {
            var predicate = PredicateBuilder.New<DataItemDetailEntity>();
            predicate.And(d => d.ItemName == name);
            predicate.And(d => d.ItemId != itemId);
            if (dataItemId != 0)
            {
                predicate.And(d => d.Id != dataItemId);
            }
            return _backendUnitOfWork.DataItemDetail.Query().AsNoTracking().Any(predicate);

        }

        private bool IsDataItemDetailUnique(DataItemDetailEntity dataItem, long dataItemId)
        {
            return dataItem == null
                || dataItem.DeleteMark == (int)DeleteStatus.Deleted
                || (dataItemId != 0 && dataItem.Id == dataItemId);
        }
    }
}
