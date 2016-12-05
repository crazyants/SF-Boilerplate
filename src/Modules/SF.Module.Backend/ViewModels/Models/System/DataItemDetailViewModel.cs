using SF.Core.Web.Base.Datatypes;
using SF.Module.Backend.ViewModels.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SF.Module.Backend.ViewModels
{
    public class DataItemDetailViewModel : EntityModelBase, IValidatableObject
    {
        /// <summary>
        /// 父级主键
        /// </summary>		
        public long? ParentId { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary>		
        public string ItemCode { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>		
        public string ItemName { get; set; }
        /// <summary>
        /// 树型结构
        /// </summary>		
        public int? IsTree { get; set; }
        /// <summary>
        /// 导航标记
        /// </summary>		
        public int? IsNav { get; set; }
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new DataItemDetailViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }

    }
}
