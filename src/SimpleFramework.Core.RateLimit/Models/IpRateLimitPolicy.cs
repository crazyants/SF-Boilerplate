using System.Collections.Generic;

namespace SimpleFramework.Core.RateLimit
{
    public class IpRateLimitPolicy
    {
        public string Ip { get; set; }
        public List<RateLimitRule> Rules { get; set; }
    }
}
