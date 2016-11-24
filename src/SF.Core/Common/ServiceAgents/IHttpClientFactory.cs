using System.Net.Http;
using  SF.Core.ServiceAgents.Settings;

namespace SF.Core.ServiceAgents
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient(ServiceAgentSettings serviceAgentSettings, ServiceSettings settings);
    }
}