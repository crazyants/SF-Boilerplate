
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Abstraction.Domain;
using SF.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SF.Module.Backend.Domain.User.ViewModel
{
    public class UserViewModel : EntityModelBase
    {
        public Guid UserGuid { get; set; }

        public bool IsAdministrator { get; set; }
        public string AccountState { get; set; }

        public string UserType { get; set; }
        public string Password { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 所属网站
        /// </summary>
        public long SiteId { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 用户工号
        /// </summary>
        public string UserNo { get; set; }      
        /// <summary>
        /// 网址
        /// </summary>
        public string WebSiteUrl { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 账户已被管理员锁定
        /// </summary>
        public bool IsLockedOut { get; set; }
        /// <summary>
        /// 时区ID
        /// </summary>
        public string TimeZoneId { get; set; }

        /// <summary>
        /// 账户审核
        /// </summary>
        public bool AccountApproved { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string AvatarUrl { get; set; }
        /// <summary>
        /// 性别
        /// </summary>		
        public string Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>		
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 手机
        /// </summary>		
        public string Mobile { get; set; }

        /// <summary>
        /// QQ号
        /// </summary>		
        public string OICQ { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>		
        public string WeChat { get; set; }
        /// <summary>
        /// MSN
        /// </summary>		
        public string MSN { get; set; }
        /// <summary>
        /// 主管主键
        /// </summary>		
        public long? ManagerId { get; set; }
        /// <summary>
        /// 机构主键
        /// </summary>		
        public long? OrganizeId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>		
        public long? DepartmentId { get; set; }
        /// <summary>
        /// 岗位主键
        /// </summary>		
        public long? DutyId { get; set; }
        /// <summary>
        /// 职位主键
        /// </summary>		
        public long? PostId { get; set; }
        /// <summary>
        /// 工作组主键
        /// </summary>		
        public long? WorkGroupId { get; set; }
        /// <summary>
        /// 安全级别
        /// </summary>		
        public int? SecurityLevel { get; set; }
        /// <summary>
        /// 在线状态
        /// </summary>		
        public int? UserOnLine { get; set; }
        /// <summary>
        /// 密码提示问题
        /// </summary>		
        public string Question { get; set; }
        /// <summary>
        /// 密码提示答案
        /// </summary>		
        public string AnswerQuestion { get; set; }
        /// <summary>
        /// 允许多用户同时登录
        /// </summary>		
        public int? CheckOnLine { get; set; }
        /// <summary>
        /// 允许登录时间开始
        /// </summary>		
        public DateTime? AllowStartTime { get; set; }
        /// <summary>
        /// 允许登录时间结束
        /// </summary>		
        public DateTime? AllowEndTime { get; set; }
        /// <summary>
        /// 暂停用户开始日期
        /// </summary>		
        public DateTime? LockStartDate { get; set; }
        /// <summary>
        /// 暂停用户结束日期
        /// </summary>		
        public DateTime? LockEndDate { get; set; }
        /// <summary>
        /// 第一次访问时间
        /// </summary>		
        public DateTime? FirstVisit { get; set; }
        /// <summary>
        /// 上一次访问时间
        /// </summary>		
        public DateTime? PreviousVisit { get; set; }
        /// <summary>
        /// 最后访问时间
        /// </summary>		
        public DateTime? LastVisit { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>		
        public int? LogOnCount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        public string Description { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

    }
}
