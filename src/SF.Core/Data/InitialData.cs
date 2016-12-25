using SF.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Data
{
    public static class InitialData
    {

        public static SiteSettings BuildInitialSite()
        {
            var newSite = new SiteSettings();
            newSite.SiteName = "Sample Site";
            newSite.AliasId = "s1";
            newSite.IsServerAdminSite = true;
            newSite.Theme = "default";
            newSite.AllowNewRegistration = true;
            newSite.AutoCreateLdapUserOnFirstLogin = true;
            newSite.ReallyDeleteUsers = true;
            newSite.LdapPort = 389;
            newSite.LdapRootDN = string.Empty;
            newSite.LdapServer = string.Empty;
            newSite.UseEmailForLogin = true;
            newSite.UseLdapAuth = false;
            newSite.RequireConfirmedEmail = false;
            newSite.RequiresQuestionAndAnswer = false;
            newSite.MaxInvalidPasswordAttempts = 10;
            newSite.MinRequiredPasswordLength = 7;

            return newSite;
        }


        public static UserEntity BuildInitialAdmin()
        {
            var adminUser = new UserEntity();
            adminUser.Email = "admin@admin.com";
            adminUser.NormalizedEmail = adminUser.Email.ToUpperInvariant();
            adminUser.UserName = "admin";
            adminUser.CreatedBy = "admin";
            adminUser.CreatedOn = DateTimeOffset.Now;
            adminUser.UpdatedBy = "admin";
            adminUser.UpdatedOn = DateTimeOffset.Now;
            adminUser.EmailConfirmed = true;
            adminUser.SecurityStamp = "6fc6624e-a37a-4e33-9fda-857767103ec9";
            //
            adminUser.PasswordHash = "AQAAAAEAACcQAAAAEAVP1ffjq7+KeG7a5FV2fmvXcLKn3yn0wJMYnaCQzK67ipx4MhrGCTbalU2yQTVLmg==";
            // clear text password will be hashed upon login
            // this format allows migrating from mojoportal

            return adminUser;
        }


        public static RoleEntity BuildAdminRole()
        {
            var role = new RoleEntity();
            role.Name = "Administrators";
            role.NormalizedName = "Administrators";
            role.Description = "Administrators";
           
            return role;
        }

        public static RoleEntity BuildAuthenticatedRole()
        {
            var role = new RoleEntity();
            role.Name = "AuthenticatedUsers";
            role.NormalizedName = "Authenticated Users";
            role.Description = role.Name.ToUpperInvariant();

            return role;
        }

        public static RoleEntity BuildContentAdminsRole()
        {
            var role = new RoleEntity();
            role.Name = "ContentAdministrators";
            role.NormalizedName= "Content Administrators";
            role.Description = role.Name.ToUpperInvariant();

            return role;
        }

    }
}
