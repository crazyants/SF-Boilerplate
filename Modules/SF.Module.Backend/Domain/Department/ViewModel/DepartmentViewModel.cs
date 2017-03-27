
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Abstraction.Domain;
using SF.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SF.Module.Backend.Domain.Department.ViewModel
{
    public class DepartmentViewModel : EntityModelBase 
    {
        /// <summary>
        /// 机构主键
        /// </summary>		
        public long? OrganizeId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>		
        public long? ParentId { get; set; }
        /// <summary>
        /// 部门代码
        /// </summary>		
        public string EnCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>		
        public string FullName { get; set; }
        /// <summary>
        /// 部门简称
        /// </summary>		
        public string ShortName { get; set; }
        /// <summary>
        /// 部门类型
        /// </summary>		
        public string Nature { get; set; }
        /// <summary>
        /// 负责人主键
        /// </summary>		
        public long? ManagerId { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>		
        public string Manager { get; set; }
        /// <summary>
        /// 外线电话
        /// </summary>		
        public string OuterPhone { get; set; }
        /// <summary>
        /// 内线电话
        /// </summary>		
        public string InnerPhone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>		
        public string Email { get; set; }
        /// <summary>
        /// 部门传真
        /// </summary>		
        public string Fax { get; set; }
        /// <summary>
        /// 层
        /// </summary>		
        public int? Layer { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>		
        public int? EnabledMark { get; set; }
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
