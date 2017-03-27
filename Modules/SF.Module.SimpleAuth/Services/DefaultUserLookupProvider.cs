
using Microsoft.Extensions.Options;
using SF.Module.SimpleAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.SimpleAuth.Services
{
    public class DefaultUserLookupProvider : IUserLookupProvider
    {
        public DefaultUserLookupProvider(IOptions<List<SimpleAuthUser>> usersAccessor)
        {
            allUsers = usersAccessor.Value;
        }

        private List<SimpleAuthUser> allUsers;

        public SimpleAuthUser GetUser(string userName)
        {
            foreach (SimpleAuthUser u in allUsers)
            {
                if (u.UserName == userName) { return u; }
            }

            return null;
        }
    }
}
