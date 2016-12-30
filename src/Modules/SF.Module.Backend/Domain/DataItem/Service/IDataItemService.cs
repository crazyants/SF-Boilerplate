using SF.Core.Entitys;
using SF.Core.Entitys.Abstraction.Pages;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DataItem.Service
{
    /// <summary>
    /// 扩展的服务处理类
    /// </summary>
    public interface IDataItemService
    {
        List<DataItemViewModel> GetChildren(int id, int rootDataItemId);

        List<DataItemEntity> GetAlls();

        IPagedList<DataItemEntity> GetPageListBykeyword(string keyword, int pageIndex, int pageSize);

    }
}
