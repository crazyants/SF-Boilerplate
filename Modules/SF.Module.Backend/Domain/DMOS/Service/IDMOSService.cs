using SF.Core.Abstraction.GenericServices;
using SF.Entitys;
using SF.Entitys.Abstraction.Pages;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DMOS.Service
{
    /// <summary>
    /// 扩展的服务处理类
    /// </summary>
    public interface IDMOSService : IServiceBase
    {
        Task<List<DMOSEntity>> GetAllsAsync();
        Task<IList<DMOSEntity>> QueryFilterByCategoryAsync(long category, string organizeId=null);
        Task<IPagedList<DMOSEntity>> GetPageListBykeywordAsync(long category, string keyword, int pageIndex = 0, int pageSize = 100);
        Task<IPagedList<DMOSEntity>> GetByWhere(long category, string keyWord, string condition, int pageIndex = 0, int recordsCount = 100);
    }
}
