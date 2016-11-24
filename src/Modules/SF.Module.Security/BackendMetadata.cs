
using SimpleFramework.Infrastructure.UI;
using System.Collections.Generic;


namespace SimpleFramework.Module.Security
{
    public class BackendMetadata : BackendMetadataBase
    {
        public override IEnumerable<BackendMenuGroup> BackendMenuGroups
        {
            get
            {
                return new BackendMenuGroup[]
                {
          new BackendMenuGroup(
            "Security",
            1000,
            new BackendMenuItem[]
            {
              new BackendMenuItem("/Security/permissions", "Permissions", 1000),
              new BackendMenuItem("/Security/roles", "Roles", 2000),
              new BackendMenuItem("/Security/users", "Users", 3000),
              new BackendMenuItem("/Security/Account", "Accounts", 3000),
            }
          )
                };
            }
        }
    }
}