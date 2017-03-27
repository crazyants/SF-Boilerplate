/*******************************************************************************
* 命名空间: SF.Web.Base.Mapper.Extensions
*
* 功 能： N/A
* 类 名： MappingExpressionExtensions
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/5 17:35:19 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using AutoMapper;
using SF.Entitys.Abstraction;
using SF.Web.Models;

namespace SF.Web.Base.Mappers.Extensions
{
    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TModel, TEntity> MapperExpressCreatedMeta<TModel, TEntity, TKey>(this IMappingExpression<TModel, TEntity> cfg)
            where TEntity : BaseEntity<TKey>
            where TModel : EntityModelBase<TKey>
        {
            return cfg.ForMember(src => src.CreatedBy, opt => opt.Ignore())
               .ForMember(src => src.CreatedOn, opt => opt.Ignore());
        }

        public static IMappingExpression<TModel, TEntity> MapperExpressUpdatedMeta<TModel, TEntity, TKey>(this IMappingExpression<TModel, TEntity> cfg)
           where TEntity : BaseEntity<TKey>
           where TModel : EntityModelBase<TKey>
        {
            return cfg.ForMember(src => src.UpdatedBy, opt => opt.Ignore())
             .ForMember(src => src.UpdatedOn, opt => opt.Ignore());
        }

        public static IMappingExpression<TModel, TEntity> MapperExpressDeleteMeta<TModel, TEntity, TKey>(this IMappingExpression<TModel, TEntity> cfg)
       where TEntity : BaseEntity<TKey>
       where TModel : EntityModelBase<TKey>
        {
            return cfg.ForMember(src => src.DeletedBy, opt => opt.Ignore())
             .ForMember(src => src.DeletedOn, opt => opt.Ignore());
        }
    }
}
