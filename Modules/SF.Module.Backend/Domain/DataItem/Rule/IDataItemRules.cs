using SF.Core.Abstraction.Domain;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DataItem.Rule
{
    public interface IDataItemRules : IRules<DataItemEntity>
    {
        bool DoesDataItemExist(long id);
        bool IsDataItemIdUnique(long id);
        bool IsDataItemNameUnique(string name, long dataItemId = 0);
        bool IsDataItemCodeUnique(string code, long dataItemId = 0);
    }
}
