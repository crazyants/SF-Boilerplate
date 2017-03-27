using SF.Core.Abstraction.GenericServices;
using SF.Entitys;
using SF.Entitys.Abstraction.Pages;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using SF.Module.Backend.Domain.DataItemDetail.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DataItemDetail.Service
{
    /// <summary>
    /// 扩展的服务处理类
    /// </summary>
    public interface IDataItemDetailService : IServiceBase
    {
        IPagedList<DataItemDetailEntity> GetByWhere(long itemId, string keyWord, string condition, int pageIndex = 0, int recordsCount = 10);
        IList<DataItemDetailEntity> GetByItemId(long itemId);
        IList<DataItemDetailEntity> GetByItemCode(string itemCode);
        List<DataItemDetailEntity> GetAlls();


    }
}
