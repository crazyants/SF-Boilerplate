
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Abstraction.Domain;
using SF.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SF.Module.Backend.Domain.DMOS.ViewModel
{
    public class DMOSViewModel : EntityModelBase
    {
        /// <summary>
        /// 机构主键
        /// </summary>		
        public int? OrganizeId { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrganizeName { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>		
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        /// <summary>
        /// 分类1-岗位2-职位3-工作组
        /// </summary>		
        public int? Category { get; set; }

        public CategoryType CategoryType
        {
            get { return (CategoryType)Category; }
        }

        /// <summary>
        /// 角色编码
        /// </summary>		
        public string EnCode { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>		
        public string FullName { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>		
        public DateTime? OverdueTime { get; set; }
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
