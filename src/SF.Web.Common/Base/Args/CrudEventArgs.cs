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
using SF.Core.Entitys.Abstraction;
using SF.Core.Abstraction.UoW;
using SF.Core.EFCore.UoW;
using SF.Web.Common.Base.Business;
using SF.Web.Common.Models;

namespace SF.Web.Common.Base.Args
{
    /// <summary>
    /// 事件参数
    /// </summary>
    /// <typeparam name="TCodeTabelEntity"></typeparam>
    /// <typeparam name="TCodeTabelModel"></typeparam>
    public class CrudEventArgs<TCodeTabelEntity, TCodeTabelModel>
        where TCodeTabelEntity : BaseEntity
        where TCodeTabelModel : EntityModelBase
    {
        public CrudEventArgs( TCodeTabelModel model, TCodeTabelEntity entity=null, long id = 0)
        {
            this.Id = Id;
            this.Entity = entity;
            this.Model = model;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }
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
