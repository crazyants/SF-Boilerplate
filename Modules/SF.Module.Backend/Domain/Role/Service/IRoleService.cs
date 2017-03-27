using SF.Core.Abstraction.GenericServices;
using SF.Entitys;
using SF.Entitys.Abstraction.Pages;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.Role.Service
{
    /// <summary>
    /// 扩展的服务处理类
    /// </summary>
    public interface IRoleService : IServiceBase
    {
        void SaveRoleAuthorize(long RoleId, string[] moduleIds, string[] moduleButtonIds);
    }
}
