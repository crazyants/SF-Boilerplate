using SF.Core.Abstraction.Resolvers;
using SF.Services;
using System;

namespace SF.Services.Site.Implementation
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
