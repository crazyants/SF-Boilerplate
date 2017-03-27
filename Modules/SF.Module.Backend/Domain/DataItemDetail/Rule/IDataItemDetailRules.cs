using SF.Core.Abstraction.Domain;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;

namespace SF.Module.Backend.Domain.DataItemDetail.Rule
{
    public interface IDataItemDetailRules : IRules<DataItemDetailEntity>
    {
        bool DoesDataItemDetailExist(long id);
        bool IsDataItemDetailIdUnique(long id);
        bool IsDataItemDetailNameUnique(string name, long itemId, long dataItemDetailId = 0);
        bool IsDataItemDetailValueUnique(string value, long itemId, long dataItemDetailId = 0);
    }
}
