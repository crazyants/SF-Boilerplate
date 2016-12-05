using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.SimpleAuth.Tenants
{
    public class MultiTenancyOptions
    {
        public List<AppTenant> Tenants { get; set; }
    }
}
