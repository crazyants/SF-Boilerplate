using SimpleFramework.Core.Web.Base.Datatypes;
using System;

namespace SimpleFramework.Module.Backend.ViewModels
{
    public class DataItemViewModel: EntityModelBase
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

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
     
        public string CreatedBy { get; set; }
     
        public string ModifiedBy { get; set; }
 
    }
}
