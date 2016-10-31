using System.Collections.Generic;

namespace SimpleFramework.Core.RateLimit
{
    public class ClientRateLimitPolicies
    {
        public List<ClientRateLimitPolicy> ClientRules { get; set; }
    }
}
