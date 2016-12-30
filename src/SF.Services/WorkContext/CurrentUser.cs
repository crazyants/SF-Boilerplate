
using SF.Core.Abstraction.Resolvers;

namespace SF.Services
{
    public class CurrentUser : ICurrentUserResolver
    {
        public CurrentUser()
        {

        }

        public string UserName { get; set; }
    }
}
