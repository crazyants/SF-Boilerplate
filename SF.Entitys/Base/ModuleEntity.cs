/*******************************************************************************
* 命名空间: SF.Module.Backend.Data.Entitys
*
* 功 能： N/A
* 类 名： ModuleEntity
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2017/1/10 14:07:51 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Entitys.Abstraction;
using System.Collections.Generic;

namespace SF.Entitys
{
    public class ModuleEntity : BaseEntity
    {
        public ModuleEntity()
        {
            RoleModules = new List<RoleModuleEntity>();
        }
        #region 实体成员
        /// <summary>
        /// 父级主键
        /// </summary>
        public long? ParentId { set; get; }
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
        #endregion

        public virtual IList<RoleModuleEntity> RoleModules { get; set; }
    }
}
