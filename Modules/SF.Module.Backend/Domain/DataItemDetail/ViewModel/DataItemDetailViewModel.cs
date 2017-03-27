
using FluentValidation;
using SF.Core.Abstraction.Domain;
using SF.Module.Backend.Domain.DataItemDetail.Validation;
using SF.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SF.Module.Backend.Domain.DataItemDetail.ViewModel
{
    public class DataItemDetailViewModel : EntityModelBase, IViewModelValidate<DataItemDetailViewModel>
    {
        /// <summary>
        /// 分类主键
        /// </summary>		
        public long ItemId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>		
        public long? ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>		
        public string ItemCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>		
        public string ItemName { get; set; }
        /// <summary>
        /// 值
        /// </summary>		
        public string ItemValue { get; set; }
        /// <summary>
        /// 快速查询
        /// </summary>		
        public string QuickQuery { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>		
        public string SimpleSpelling { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>		
        public int? IsDefault { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>		
        public int? DeleteMark { get; set; }
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


        [Obsolete("默认使用FluentValidation集成，判断使用ModelState.IsValid")]
        public IEnumerable<ValidationResult> Validate(IValidator<DataItemDetailViewModel> validator)
        {
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
