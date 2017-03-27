using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SF.Core.Common;
using SF.Core.Extensions;
using SF.Entitys;
using SF.Web.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SF.Core;

namespace SF.Web.Security.Converters
{
    /// <summary>
    /// 应用用户转换处理
    /// </summary>
    public static class ApplicationUserConverter
    {
        public static ApplicationUserExtended ToCoreModel(this UserEntity dbEntity)
        {
            var retVal = new ApplicationUserExtended();
            retVal = new ApplicationUserExtended();
            retVal = Mapper.Map<UserEntity, ApplicationUserExtended>(dbEntity);

            retVal.UserState = EnumHelper.SafeParse<AccountState>(dbEntity.AccountState, AccountState.Approved);

          //  retVal.Roles = dbEntity.Roles.Select(x => x.Role.ToCoreModel()).ToArray();
           
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
            retVal = Mapper.Map<ApplicationUserExtended, UserEntity>(user);
            retVal.AccountState = user.UserState.ToString();

            if (user.Roles != null)
            {
                var roles = user.Roles.Select(x => x.ToAssignmentDataModel());
                foreach (var item in roles)
                {
                    retVal.Roles.Add(item);
                }

            }
            return retVal;
        }


        public static void Patch(this ApplicationUserExtended user, UserEntity dbUser)
        {

            dbUser = Mapper.Map<ApplicationUserExtended, UserEntity>(user);
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
