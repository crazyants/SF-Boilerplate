using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SF.Core.Abstraction.UoW.Helper;
using SF.Core.Data;
using SF.Core.Entitys;
using SF.Core.StartupTask;
using SF.Web.Security.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Security
{
    public class RolePermissionStartupTask : IStartupTask
    {
        private readonly IBaseUnitOfWork _baseUnitOfWork;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly IEnumerable<IPermissionProvider> _permissionProviders;
        private readonly IPermissionScopeService _permissionScopeService;

        public RolePermissionStartupTask(
            IBaseUnitOfWork baseUnitOfWork,
            RoleManager<RoleEntity> roleManager,
            IEnumerable<IPermissionProvider> permissionProviders,
            ILogger<RolePermissionStartupTask> logger,
            IPermissionScopeService permissionScopeService)
        {
            _baseUnitOfWork = baseUnitOfWork;
            _roleManager = roleManager;
            _permissionProviders = permissionProviders;
            _permissionScopeService = permissionScopeService;
            Logger = logger;
        }
        public ILogger Logger { get; set; }

        public int Priority { get { return 2; } }

        public async Task Run()
        {

            // when another module is being enabled, locate matching permission providers
            var providersForEnabledModule = _permissionProviders;

            foreach (var permissionProvider in providersForEnabledModule)
            {
                var stereotypes = permissionProvider.GetDefaultStereotypes();
                foreach (var stereotype in stereotypes)
                {

                    var role = await _roleManager.FindByNameAsync(stereotype.Name);
                    if (role == null)
                    {
                        if (Logger.IsEnabled(LogLevel.Information))
                        {
                            Logger.LogInformation($"Defining new role {stereotype.Name} for permission stereotype");
                        }

                        role = new RoleEntity { Name = stereotype.Name, Description = stereotype.Description };
                        await _roleManager.CreateAsync(role);
                    }

                    // and merge the stereotypical permissions into that role
                    var stereotypePermissionNames = (stereotype.Permissions ?? Enumerable.Empty<Permission>()).Select(x => new { Name = x.Name, Description = x.Description });
                    // var currentPermissionNames = role.RolePermissions.Where(x => x.ClaimType == Permission.ClaimType).Select(x => x.ClaimValue);
                    var currentPermissionNames = role.RolePermissions.Select(rp => rp.ToCoreModel(_permissionScopeService)).Select(x => new { Name = x.Name, Description = x.Description }).ToArray();
                    var distinctPermissionNames = currentPermissionNames
                        .Union(stereotypePermissionNames)
                        .Distinct();

                    // update role if set of permissions has increased
                    var additionalPermissionNames = distinctPermissionNames.Except(currentPermissionNames);

                    if (additionalPermissionNames.Any())
                    {
                        foreach (var permission in additionalPermissionNames)
                        {
                            if (Logger.IsEnabled(LogLevel.Debug))
                            {
                                Logger.LogInformation("Default role {0} granted permission {1}", stereotype.Name, permission.Name);
                            }
                            if (!_baseUnitOfWork.BaseWorkArea.Permission.Query().Any(p => p.Name == permission.Name))
                            {
                                //  await _roleManager.AddClaimAsync(role, new Claim(Permission.ClaimType, permissionName));
                                await _baseUnitOfWork.ExecuteAndCommitAsync((uow, c) =>
                               {
                                   return uow.BaseWorkArea.Permission.AddAsync(new PermissionEntity() { Name = permission.Name, Description = permission.Description });
                               });
                            }
                        }
                    }
                }
            }
        }
    }
}
