using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SF.Core.Data;
using SF.Core.Entitys;
using SF.Web.Security.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Security
{
    public class RolePermissionStartup
    {
        private readonly IBaseUnitOfWork _baseUnitOfWork;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly IEnumerable<IPermissionProvider> _permissionProviders;
        private readonly IPermissionScopeService _permissionScopeService;

        public RolePermissionStartup(
            IBaseUnitOfWork baseUnitOfWork,
            RoleManager<RoleEntity> roleManager,
            IEnumerable<IPermissionProvider> permissionProviders,
            ILogger<RolePermissionStartup> logger,
            IPermissionScopeService permissionScopeService)
        {
            _baseUnitOfWork = baseUnitOfWork;
            _roleManager = roleManager;
            _permissionProviders = permissionProviders;
            _permissionScopeService = permissionScopeService;
            Logger = logger;
        }
        public ILogger Logger { get; set; }

        public async Task AddDefaultRolesPermissionAsync()
        {

            // when another module is being enabled, locate matching permission providers
            var providersForEnabledModule = _permissionProviders;


            foreach (var permissionProvider in providersForEnabledModule)
            {
                // get and iterate stereotypical groups of permissions
                var stereotypes = permissionProvider.GetDefaultStereotypes();
                foreach (var stereotype in stereotypes)
                {

                    // turn those stereotypes into roles
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
                            //  await _roleManager.AddClaimAsync(role, new Claim(Permission.ClaimType, permissionName));
                            // _permissionRepository.Insert(new PermissionEntity() { Name = permission.Name, Description = permission.Description });

                        }
                    }
                }
            }
        }
    }
}
