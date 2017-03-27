
using SF.Core.Abstraction.Resolvers;

namespace SF.Web.Components
{
    public class CurrentUser : ICurrentUserResolver
    {
        public CurrentUser()
        {

        }

        public string UserName { get; set; }
    }
}
