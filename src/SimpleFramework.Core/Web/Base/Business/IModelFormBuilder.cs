
using SimpleFramework.Core.Abstraction.Entitys;
using SimpleFramework.Core.Web.Base.Datatypes;

namespace Digipolis.Codetable.Interfaces
{
    /// <summary>
    /// 从模型构建表单的构建器的接口
    /// </summary>
    public interface IModelFormBuilder<TCodeTabelEntity, TCodeTabelModel>
        where TCodeTabelEntity : EntityBase
        where TCodeTabelModel : EntityModelBase
    {
        /// <summary>
        /// 绑定表单
        /// </summary>
        void Bind();
        /// <summary>
        /// 提交表单，返回处理结果
        /// </summary>
        object Submit();

        /// <summary>
        /// 获取添加数据使用的表单
        /// </summary>
        /// <returns></returns>
        TCodeTabelEntity ToCoreModel(TCodeTabelModel model);
        /// <summary>
        /// 获取编辑数据使用的表单
        /// </summary>
        /// <returns></returns>
        TCodeTabelModel ToDataModel(TCodeTabelEntity entity);

    }
}
