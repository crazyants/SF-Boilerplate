using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.SimpleAuth.Tenants
{
    public class MultiTenancyOptions
    {
        public List<AppTenant> Tenants { get; set; }
    }
}
