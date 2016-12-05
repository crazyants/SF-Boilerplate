using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;


namespace SF.Core.ServiceAgents.Settings
{
    internal class ServiceSettingsConfigReader
    {
        public ServiceAgentSettings ReadConfig(IConfigurationRoot config)
        {
            var serviceAgentSettings = new ServiceAgentSettings();

            var sections = config.GetChildren().ToDictionary(s => s.Key);

            if (sections.ContainsKey("Global"))
            {
                var globalSection = sections["Global"];

                globalSection.Bind(serviceAgentSettings);
                sections.Remove("Global");
            }

            foreach (var item in sections)
            {
                try
                {
                    var settings = new ServiceSettings();

                    item.Value.Bind(settings);

                    serviceAgentSettings.Services.Add(item.Key, settings);
                }
                catch (Exception)
                {
                }
            }

            return serviceAgentSettings;
        }

        private PropertyInfo[] GetWritableProperties()
        {
            var type = typeof(ServiceSettings);

            var properties = type.GetProperties().Where(p => p.CanWrite == true).ToArray();
            return properties;
        }
    }
}
