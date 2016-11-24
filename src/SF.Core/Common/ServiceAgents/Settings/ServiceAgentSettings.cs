using System;
using System.Collections.Generic;

namespace SF.Core.ServiceAgents.Settings
{
    public class ServiceAgentSettings
    {
        public ServiceAgentSettings()
        {
            Services = new Dictionary<string, ServiceSettings>();
        }

        public string GlobalApiKey { get; set; }

        public IDictionary<string, ServiceSettings> Services { get; private set; }
    }
}
