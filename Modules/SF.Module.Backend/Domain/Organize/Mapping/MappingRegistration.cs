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
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.Organize.ViewModel;
using SF.Web.Base.Mappers.Extensions;

namespace SF.Module.Backend.Domain.Organize.Mapping
{
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        public void MapperConfigurationToExpression(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<OrganizeViewModel, OrganizeEntity>()
                .MapperExpressCreatedMeta<OrganizeViewModel, OrganizeEntity, long>()
                .MapperExpressUpdatedMeta<OrganizeViewModel, OrganizeEntity, long>()
                .MapperExpressDeleteMeta<OrganizeViewModel, OrganizeEntity, long>()
                .ReverseMap();

           
        }
    }

   
}
