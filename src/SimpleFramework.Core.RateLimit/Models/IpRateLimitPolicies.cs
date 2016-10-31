using System.Collections.Generic;

namespace SimpleFramework.Core.RateLimit
{
    public class IpRateLimitPolicies
    {
        public List<IpRateLimitPolicy> IpRules { get; set; }
    }
}
