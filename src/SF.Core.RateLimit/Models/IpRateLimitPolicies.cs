using System.Collections.Generic;

namespace SF.Core.RateLimit
{
    public class IpRateLimitPolicies
    {
        public List<IpRateLimitPolicy> IpRules { get; set; }
    }
}
