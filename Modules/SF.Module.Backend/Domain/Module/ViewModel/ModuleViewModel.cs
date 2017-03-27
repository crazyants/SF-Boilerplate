
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Abstraction.Domain;
using SF.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SF.Module.Backend.Domain.Module.ViewModel
{
    public class ModuleViewModel : EntityModelBase 
    {
        /// <summary>
        /// 父级主键
        /// </summary>		
        public long? ParentId { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string EnCode { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { set; get; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { set; get; }
        /// <summary>
        /// 导航地址
        /// </summary>
        public string UrlAddress { set; get; }
        /// <summary>
        /// 导航目标
        /// </summary>
        public string Target { set; get; }
        /// <summary>
        /// 菜单选项
        /// </summary>
        public int? IsMenu { set; get; }
        /// <summary>
        /// 允许展开
        /// </summary>
        public int? AllowExpand { set; get; }
        /// <summary>
        /// 是否公开
        /// </summary>
        public int? IsPublic { set; get; }
        /// <summary>
        /// 允许编辑
        /// </summary>
        public int? AllowEdit { set; get; }
        /// <summary>
        /// 允许删除
        /// </summary>
        public int? AllowDelete { set; get; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public int? DeleteMark { set; get; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public int? EnabledMark { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { set; get; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

       
    }
}
