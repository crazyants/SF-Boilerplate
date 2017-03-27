/*******************************************************************************
* 命名空间: SF.Web.Base.Args
*
* 功 能： N/A
* 类 名： CrudEventArgs
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/25 10:20:41 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Entitys.Abstraction;
using SF.Core.Abstraction.UoW;
using SF.Core.EFCore.UoW;
using SF.Web.Base.Business;
using SF.Web.Models;

namespace SF.Web.Base.Args
{
    /// <summary>
    /// 事件参数
    /// </summary>
    /// <typeparam name="TCodeTabelEntity"></typeparam>
    /// <typeparam name="TCodeTabelModel"></typeparam>
    public class CrudEventArgs<TCodeTabelEntity, TCodeTabelModel, Tkey>
        where TCodeTabelEntity : BaseEntity<Tkey>
        where TCodeTabelModel : EntityModelBase<Tkey>
    {
        public CrudEventArgs(Tkey id, TCodeTabelModel model, TCodeTabelEntity entity = null)
        {
            this.Id = Id;
            this.Entity = entity;
            this.Model = model;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        public Tkey Id { get; set; }
        /// <summary>
        /// 实体数据
        /// </summary>
        public TCodeTabelEntity Entity { get; set; }
        /// <summary>
        /// 模型数据
        /// </summary>
        public TCodeTabelModel Model { get; set; }

    }
}
