using SF.Entitys.Abstraction;
using System;

namespace SF.Module.Backend.Data.Entitys
{
    /// <summary>
    /// 岗位职位
    /// </summary>
    public class DMOSEntity : BaseEntity
    {
        /// <summary>
        /// 机构主键
        /// </summary>		
        public long? OrganizeId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>		
        public long? DepartmentId { get; set; }
        /// <summary>
        /// 分类1-岗位2-职位3-工作组
        /// </summary>		
        public int? Category { get; set; }
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
    }
}
