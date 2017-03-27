
using CacheManager.Core;
using SF.Entitys;
using SF.Module.Backend.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SF.Core.Extensions;
using System.Linq.Expressions;
using LinqKit;
using SF.Entitys.Abstraction.Pages;
using Microsoft.EntityFrameworkCore;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Data.Uow;
using SF.Core.Abstraction.UoW.Helper;
using Z.EntityFramework.Plus;
using SF.Data;
using SF.Core.Abstraction.GenericServices;

namespace SF.Module.Backend.Domain.Role.Service
{
    public class RoleService : ServiceBase, IRoleService
    {
        #region Fields
        private readonly ICacheManager<object> _cacheManager;
        private readonly IBaseUnitOfWork _baseUnitOfWork;
        #endregion

        #region Constructors
        public RoleService(IBaseUnitOfWork baseUnitOfWork,
            ICacheManager<object> cacheManager)
        {
            _baseUnitOfWork = baseUnitOfWork;
            _cacheManager = cacheManager;
        }
        #endregion


        #region Method  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="moduleIds"></param>
        /// <param name="moduleButtonIds"></param>
        public void SaveRoleAuthorize(long RoleId, string[] moduleIds, string[] moduleButtonIds)
        {
            var rolePermissions = new List<RolePermissionEntity>();
            var roleModules = new List<RoleModuleEntity>();
            foreach (var item in moduleIds)
            {
                roleModules.Add(new RoleModuleEntity() { ModuleId = item.AsInteger(), RoleId = RoleId });
            }
            foreach (var item in moduleButtonIds)
            {
                if (!item.Contains("M"))
                    rolePermissions.Add(new RolePermissionEntity() { PermissionId = item.AsInteger(), RoleId = RoleId });
            }
            if (roleModules.Count > 0)
            {
                _baseUnitOfWork.ExecuteAndCommit((uow) =>
                   {
                       uow.BaseWorkArea.RoleModule.Set.Where(x => x.RoleId == RoleId).Delete();
                       uow.BaseWorkArea.RoleModule.Add(roleModules);

                       uow.BaseWorkArea.RolePermission.Set.Where(x => x.RoleId == RoleId).Delete();
                       uow.BaseWorkArea.RolePermission.Add(rolePermissions);
                   });

            }
        }
        #endregion
    }
}
