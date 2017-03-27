using SF.Core.Abstraction.GenericServices;
using SF.Entitys;
using SF.Entitys.Abstraction.Pages;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.Module.Service
{
    /// <summary>
    /// 扩展的服务处理类
    /// </summary>
    public interface IModuleService : IServiceBase
    {
        Task<List<ModuleEntity>> GetAlls();
        Task<List<ModuleEntity>> QueryFilterByParentId(long parentId);
        Task<List<long>> QueryFilterByParentIds(long[] parentIds);
        Task<List<ModuleEntity>> GetPageListBykeyword(long parentId, string keyword, string condition);
        Task<List<PermissionEntity>> GetButtonByModuleId(long moduleId);
        Task<List<PermissionEntity>> GetButtonByModuleName(string moduleName = null);
        void SaveForm(ModuleEntity moduleEntity, List<PermissionEntity> moduleButtonList);
    }
}
