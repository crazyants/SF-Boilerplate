using System.Net.Http;
using  SimpleFramework.Core.ServiceAgents.Settings;

namespace SimpleFramework.Core.ServiceAgents
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient(ServiceAgentSettings serviceAgentSettings, ServiceSettings settings);
    }
}