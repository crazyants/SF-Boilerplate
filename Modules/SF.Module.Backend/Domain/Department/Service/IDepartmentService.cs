using SF.Core.Abstraction.GenericServices;
using SF.Entitys;
using SF.Entitys.Abstraction.Pages;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.Department.Service
{
    /// <summary>
    /// 扩展的服务处理类
    /// </summary>
    public interface IDepartmentService : IServiceBase
    {
        Task<List<DepartmentEntity>> GetAlls();
        Task<IList<DepartmentEntity>> QueryFilterByParentId(long parentId);
        Task<IList<long>> QueryFilterByParentIds(long[] parentIds);
        Task<IPagedList<DepartmentEntity>> GetPageListBykeyword(long parentId, string keyword, int pageIndex = 0, int pageSize = 100);
        IPagedList<DepartmentEntity> GetByWhere(string keyWord, string condition, int pageIndex = 0, int recordsCount = 100);
    }
}
