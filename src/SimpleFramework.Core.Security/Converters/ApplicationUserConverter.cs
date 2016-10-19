using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Omu.ValueInjecter;
using SimpleFramework.Infrastructure.Common;
using SimpleFramework.Infrastructure.Extensions;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Core.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleFramework.Core.Security.Converters
{
    public static class ApplicationUserConverter
    {
        public static ApplicationUserExtended ToCoreModel(this UserEntity applicationUser, UserEntity dbEntity, IPermissionScopeService scopeService)
        {
            var retVal = new ApplicationUserExtended();
            retVal = new ApplicationUserExtended();
            retVal.InjectFrom(applicationUser);
            retVal.InjectFrom(dbEntity);
            retVal.UserState = EnumUtility.SafeParse<AccountState>( dbEntity.AccountState,AccountState.Approved);
 
            retVal.Roles = dbEntity.Roles.Select(x => x.Role.ToCoreModel(scopeService)).ToArray();
            retVal.Permissions = retVal.Roles.SelectMany(x => x.Permissions).SelectMany(x=> x.GetPermissionWithScopeCombinationNames()).Distinct().ToArray();
            retVal.ApiAccounts = dbEntity.ApiAccounts.Select(x => x.ToCoreModel()).ToArray();

            return retVal;
        }

        public static UserEntity ToIdentityModel(this ApplicationUserExtended user)
        {
            var retVal = new UserEntity();
            user.Patch(retVal);
            return retVal;
        }

        public static UserEntity ToDataModel(this ApplicationUserExtended user)
        {
            var retVal = new UserEntity();
            retVal.InjectFrom(user);

            retVal.AccountState = user.UserState.ToString();

            if (user.Roles != null)
            {
                var roles = user.Roles.Select(x => x.ToAssignmentDataModel());
                foreach (var item in roles)
                {
                    retVal.Roles.Add(item);
                }
               
            }
            if (user.ApiAccounts != null)
            {
                retVal.ApiAccounts = new ObservableCollection<ApiAccountEntity>(user.ApiAccounts.Select(x => x.ToDataModel()));
            }
            return retVal;
        }

        public static void Patch(this UserEntity source, UserEntity target)
        {
            var patchInjection = new PatchInjection<UserEntity>(x => x.UserType, x => x.AccountState,x => x.IsAdministrator);
            target.InjectFrom(patchInjection, source);

            if (!source.ApiAccounts.IsNullCollection())
            {
                source.ApiAccounts.Patch(target.ApiAccounts, (sourceItem, targetItem) => sourceItem.Patch(targetItem));
            }
            if (!source.Roles.IsNullCollection())
            {
                source.Roles.Patch(target.Roles, (sourceItem, targetItem) => sourceItem.Patch(targetItem));
            }
        }

        public static void Patch(this ApplicationUserExtended user, UserEntity dbUser)
        {

            var patchInjection = new PatchInjection<UserEntity>(x => x.Id, x => x.PasswordHash, x => x.SecurityStamp,
                                                                     x => x.UserName, x => x.Email, x => x.PhoneNumber);
            dbUser.InjectFrom(patchInjection, user);

            // Copy logins
            if (user.Logins != null)
            {
                var changedLogins = user.Logins.Select(x => new IdentityUserLogin<long>
                {
                    LoginProvider = x.LoginProvider,
                    ProviderKey = x.ProviderKey,
                    UserId = dbUser.Id
                }).ToList();

                var comparer = AnonymousComparer.Create((IdentityUserLogin<long> x) => x.LoginProvider);
                changedLogins.Patch(dbUser.Logins, comparer, (sourceItem, targetItem) => { sourceItem.ProviderKey = targetItem.ProviderKey; });
            }

        }
    }
}
