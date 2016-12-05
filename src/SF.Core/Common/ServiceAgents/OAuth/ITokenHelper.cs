using System.Threading.Tasks;
using  SF.Core.ServiceAgents.Models;
using  SF.Core.ServiceAgents.Settings;

namespace SF.Core.ServiceAgents.OAuth
{
    public interface ITokenHelper
    {
        Task<TokenReply> ReadOrRetrieveToken(ServiceSettings options);
    }
}