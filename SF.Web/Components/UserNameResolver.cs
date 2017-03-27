using SF.Core.Abstraction.Resolvers;
using System;

namespace SF.Web.Components
{
    public class UserNameResolver : IUserNameResolver
    {
        private readonly ICurrentUserResolver _currentUserFactory;

        public UserNameResolver(ICurrentUserResolver currentUserFactory)
        {
            _currentUserFactory = currentUserFactory;
        }

        public string GetCurrentUserName()
        {
            var currentUser = _currentUserFactory != null ? _currentUserFactory : null;
            var userName = currentUser != null ? currentUser.UserName : null;

            if (string.IsNullOrEmpty(userName))
            {
                userName = "unknown";
            }

            return userName;
        }
    }
}
