/*******************************************************************************
* 命名空间: SF.Web.Security.Converters
*
* 功 能： N/A
* 类 名： MappingRegistration
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/5 11:02:21 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using AutoMapper;
using SF.Core.Abstraction.Mapping;
using SF.Entitys;
using SF.Module.Blog.Data.Entitys;
using SF.Module.Blog.Domain.Post.ViewModel;
using SF.Web.Base.Mappers.Extensions;
using System;

namespace SF.Module.Blog.Domain.Post.Mapping
{
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        /// <summary>
        /// AutoMapper的映射配置
        /// </summary>
        /// <param name="cfg"></param>
        public void MapperConfigurationToExpression(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<PostViewModel, PostEntity>()
                .MapperExpressCreatedMeta<PostViewModel, PostEntity, Guid>()
                .MapperExpressUpdatedMeta<PostViewModel, PostEntity, Guid>()
                .MapperExpressDeleteMeta<PostViewModel, PostEntity, Guid>()
                .ReverseMap();


        }
    }


}
