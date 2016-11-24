using System.Collections.Generic;

namespace SF.Core.RateLimit
{
    public class ClientRateLimitPolicy
    {
        public string ClientId { get; set; }
        public List<RateLimitRule> Rules { get; set; }
    }
}
