/*******************************************************************************
* 命名空间: SF.Entitys.Base
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

namespace SF.Entitys
{
   public interface IUserInfo
    {
        long Id { get; }
        /// <summary>
        /// 所属网站
        /// </summary>
        long SiteId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        string DisplayName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        string Email { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        string WebSiteUrl { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// 账户已被管理员锁定
        /// </summary>
        bool IsLockedOut { get; set; }
        /// <summary>
        /// 时区ID
        /// </summary>
        string TimeZoneId { get; set; }

        /// <summary>
        /// 电话
        /// </summary>		
        string PhoneNumber { get; set; }
        /// <summary>
        /// 电话是否确认
        /// </summary>
        bool PhoneNumberConfirmed { get; set; }
        /// <summary>
        /// 账户审核
        /// </summary>
        bool AccountApproved { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        string AvatarUrl { get; set; }
        /// <summary>
        /// 性别
        /// </summary>		
        string Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>		
        DateTime? Birthday { get; set; }
        /// <summary>
        /// 手机
        /// </summary>		
        string Mobile { get; set; }

        /// <summary>
        /// QQ号
        /// </summary>		
        string OICQ { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>		
        string WeChat { get; set; }
        /// <summary>
        /// MSN
        /// </summary>		
        string MSN { get; set; }
        /// <summary>
        /// 主管主键
        /// </summary>		
        long? ManagerId { get; set; }
        /// <summary>
        /// 机构主键
        /// </summary>		
        long? OrganizeId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>		
        long? DepartmentId { get; set; }
        /// <summary>
        /// 岗位主键
        /// </summary>		
        long? DutyId { get; set; }
        /// <summary>
        /// 职位主键
        /// </summary>		
        long? PostId { get; set; }
        /// <summary>
        /// 工作组主键
        /// </summary>		
        long? WorkGroupId { get; set; }
        /// <summary>
        /// 安全级别
        /// </summary>		
        int? SecurityLevel { get; set; }
        /// <summary>
        /// 在线状态
        /// </summary>		
        int? UserOnLine { get; set; }
        /// <summary>
        /// 密码提示问题
        /// </summary>		
        string Question { get; set; }
        /// <summary>
        /// 密码提示答案
        /// </summary>		
        string AnswerQuestion { get; set; }
        /// <summary>
        /// 允许多用户同时登录
        /// </summary>		
        int? CheckOnLine { get; set; }
        /// <summary>
        /// 允许登录时间开始
        /// </summary>		
        DateTime? AllowStartTime { get; set; }
        /// <summary>
        /// 允许登录时间结束
        /// </summary>		
        DateTime? AllowEndTime { get; set; }
        /// <summary>
        /// 暂停用户开始日期
        /// </summary>		
        DateTime? LockStartDate { get; set; }
        /// <summary>
        /// 暂停用户结束日期
        /// </summary>		
        DateTime? LockEndDate { get; set; }
        /// <summary>
        /// 第一次访问时间
        /// </summary>		
        DateTime? FirstVisit { get; set; }
        /// <summary>
        /// 上一次访问时间
        /// </summary>		
        DateTime? PreviousVisit { get; set; }
        /// <summary>
        /// 最后访问时间
        /// </summary>		
        DateTime? LastVisit { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>		
        int? LogOnCount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        string Description { get; set; }
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
