using SF.Core.Abstraction.GenericServices;
using SF.Entitys;
using SF.Entitys.Abstraction.Pages;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.Area.Service
{
    /// <summary>
    /// 扩展的服务处理类
    /// </summary>
    public interface IAreaService:IServiceBase
    {
        Task<IList<AreaEntity>> QueryFilterByParentId(long parentId);
        Task<IList<long>> QueryFilterByParentIds(long[] parentIds);
        Task<IPagedList<AreaEntity>> GetPageListBykeyword(long parentId,string keyword, int pageIndex = 0, int pageSize = 100);

    }
}
