
using SF.Module.SimpleAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.SimpleAuth.Tenants
{
    public class AppTenantUserLookupProvider : IUserLookupProvider
    {
        public AppTenantUserLookupProvider(AppTenant tenant)
        {
            this.tenant = tenant;
        }

        private AppTenant tenant;

        public SimpleAuthUser GetUser(string userName)
        {
            foreach (SimpleAuthUser u in tenant.Users)
            {
                if (u.UserName == userName) { return u; }
            }

            return null;
        }
    }
}
