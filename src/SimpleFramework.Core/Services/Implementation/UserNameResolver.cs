using System;

namespace SimpleFramework.Core
{
    public class UserNameResolver : IUserNameResolver
    {
        private readonly ICurrentUser _currentUserFactory;

        public UserNameResolver(ICurrentUser currentUserFactory)
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
