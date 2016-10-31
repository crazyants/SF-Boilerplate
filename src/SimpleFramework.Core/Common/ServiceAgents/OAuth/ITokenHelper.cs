using System.Threading.Tasks;
using  SimpleFramework.Core.ServiceAgents.Models;
using  SimpleFramework.Core.ServiceAgents.Settings;

namespace SimpleFramework.Core.ServiceAgents.OAuth
{
    public interface ITokenHelper
    {
        Task<TokenReply> ReadOrRetrieveToken(ServiceSettings options);
    }
}