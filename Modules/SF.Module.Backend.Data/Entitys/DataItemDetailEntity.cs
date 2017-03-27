using SF.Entitys.Abstraction;
 

namespace SF.Module.Backend.Data.Entitys
{
    public class DataItemDetailEntity : BaseEntity
    {
        #region 实体成员
     

        /// <summary>
        /// 分类主键
        /// </summary>		
        public long ItemId { get; set; }
        public virtual DataItemEntity DataItem { get; set; }
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

        #endregion

    }
}
