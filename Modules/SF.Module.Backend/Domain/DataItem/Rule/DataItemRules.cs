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

namespace SF.Module.Backend.Domain.DataItem.Rule
{
    /// <summary>
    /// 字典业务规则处理
    /// </summary>
    public class DataItemRules : BaseRules<DataItemEntity>, IDataItemRules
    {
        private readonly IBackendUnitOfWork _backendUnitOfWork;

        public DataItemRules(IBackendUnitOfWork backendUnitOfWork)
        {
            _backendUnitOfWork = backendUnitOfWork;
        }

        public bool DoesDataItemExist(long id)
        {
            var app = _backendUnitOfWork.DataItem.GetById(id);
            return app != null && app.DeleteMark != (int)DeleteStatus.Deleted;
        }

        public bool IsDataItemIdUnique(long id)
        {
            return _backendUnitOfWork.DataItem.Exists(id);

        }

        public bool IsDataItemCodeUnique(string code, long dataItemId = 0)
        {
            var predicate = PredicateBuilder.New<DataItemEntity>();
            predicate.And(d => d.ItemCode == code);
            if (dataItemId != 0)
            {
                predicate.And(d => d.Id != dataItemId);
            }
            return _backendUnitOfWork.DataItem.Query().AsNoTracking().Any(predicate);

        }

        public bool IsDataItemNameUnique(string name, long dataItemId = 0)
        {
            var predicate = PredicateBuilder.New<DataItemEntity>();
            predicate.And(d => d.ItemName == name);
            if (dataItemId != 0)
            {
                predicate.And(d => d.Id != dataItemId);
            }
            return _backendUnitOfWork.DataItem.Query().AsNoTracking().Any(predicate);

        }

        private bool IsDataItemUnique(DataItemEntity dataItem, long dataItemId)
        {
            return dataItem == null
                || dataItem.DeleteMark == (int)DeleteStatus.Deleted
                || (dataItemId != 0 && dataItem.Id == dataItemId);
        }
    }
}
