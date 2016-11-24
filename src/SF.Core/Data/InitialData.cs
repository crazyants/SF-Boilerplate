using SF.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Data
{
    public static class InitialData
    {

        public static UserEntity BuildInitialAdmin()
        {
            var adminUser = new UserEntity();
            adminUser.Email = "admin@admin.com";
            adminUser.NormalizedEmail = adminUser.Email.ToUpperInvariant();
            adminUser.UserName = "admin";

            adminUser.EmailConfirmed = true;

            // clear text password will be hashed upon login
            // this format allows migrating from mojoportal
            adminUser.PasswordHash = "admin||0"; //pwd/salt/format 

            return adminUser;
        }


        public static RoleEntity BuildAdminRole()
        {
            var role = new RoleEntity();
            role.Name = "Recored";
            role.Description = "Administrators";
           
            return role;
        }
    }
}
