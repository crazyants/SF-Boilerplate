/*******************************************************************************
* 命名空间: SF.Core.Entitys.Base
*
* 功 能： N/A
* 类 名： IUserInfo
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/13 13:25:36 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Entitys
{
    public interface IUserInfo
    {
        long Id { get; }
        long SiteId { get; set; }
        string UserName { get; set; }
        string DisplayName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        DateTime? DateOfBirth { get; set; }
        bool DisplayInMemberList { get; set; }
        bool Trusted { get; set; }
        string WebSiteUrl { get; set; }
        bool IsDeleted { get; set; }

        /// <summary>
        /// this property indicates if an account has been locked by an administrator
        /// </summary>
        bool IsLockedOut { get; set; }

        DateTime? LastLoginUtc { get; set; }
        string TimeZoneId { get; set; }

        string PhoneNumber { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        bool AccountApproved { get; set; }
        string AvatarUrl { get; set; }
        string Gender { get; set; }
    }


    public interface ISiteUser 
    {

        int AccessFailedCount { get; set; } // maps to FailedPasswordAttemptCount in ado data layers
        string PasswordHash { get; set; }
        bool MustChangePwd { get; set; }
        DateTime? LastPasswordChangeUtc { get; set; }

        /// <summary>
        /// This property is independendent of IsLockedOut, if the property is populated with a future datetime then
        /// the user is locked out until that datetime. This property is used for lockouts related to failed authentication attempts,
        /// as opposed to IsLockedOut which is a property the admin can use to permanently lock out an account.
        /// </summary>
        DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// This property determines whether a user account can be locked out using LockouEndDateUtc,
        /// ie whether failed login attempts can cause the account to be locked by setting the LockoutEndDate.
        /// It should be true for most accounts but perhaps for admin accounts you may not want it to be possible 
        /// for an admin user to be locked out
        /// </summary>
        //bool CanBeLockedOut { get; set; } // TODO: add this property

        bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// indicates if this account can be automatically locked out (temporarily) due to AccessFailedCount
        /// >= site.MaxInvalidPasswordAttempts
        /// If false then this account will not be locked out by failed access attempts.
        /// </summary>
        bool CanAutoLockout { get; set; }

        //TODO: implement a middleware to detect this and reset the the auth/role cookie
        // set this true whenever a users roles have been changed and set false after cookie reset
        bool RolesChanged { get; set; }

        /// <summary>
        /// A random value that must change whenever a users credentials change (password changed, login removed)
        /// </summary>
        string SecurityStamp { get; set; }

        bool EmailConfirmed { get; set; }
        string NormalizedEmail { get; set; } // maps to LoweredEmail in ado data layers
        string NormalizedUserName { get; set; }

        string NewEmail { get; set; }


        /// <summary>
        /// when a user requests a change to their currently confirmed account email
        /// we should send them an approval link to their current email
        /// if the user clicks the link we should set this to true, then send an email with 
        /// a link to confirm the new email. Once they click that link we should move the new email to the
        /// Email and NormalizedEmail fields, mark it as confirmed and clear the value from NewEmail,
        /// and set NewEmailApproved to false
        /// This strategy should make it difficult to take over an account even if a session hijack
        /// somehow had been achieved.
        /// </summary>
        bool NewEmailApproved { get; set; }

        string Signature { get; set; }
        string AuthorBio { get; set; }
        string Comment { get; set; }
        // string ConcurrencyStamp { get; set; }



    }
}
