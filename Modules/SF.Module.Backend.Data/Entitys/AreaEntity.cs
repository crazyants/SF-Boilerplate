using SF.Entitys.Abstraction;
 

namespace SF.Module.Backend.Data.Entitys
{
    public class AreaEntity : BaseEntity
    {
        /// <summary>
        /// 父级主键
        /// </summary>		
        public long? ParentId { get; set; }
        /// <summary>
        /// 区域编码
        /// </summary>		
        public string AreaCode { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>		
        public string AreaName { get; set; }
        /// <summary>
        /// 快速查询
        /// </summary>		
        public string QuickQuery { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>		
        public string SimpleSpelling { get; set; }
        /// <summary>
        /// 层次
        /// </summary>		
        public int? Layer { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>		
        public int? DeleteMark { get; set; } = 0;
        /// <summary>
        /// 有效标志
        /// </summary>		
        public int? EnabledMark { get; set; } = 0;
        /// <summary>
        /// 备注
        /// </summary>		
        public string Description { get; set; }
    }
}
