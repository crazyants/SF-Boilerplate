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
using Microsoft.AspNetCore.Identity;
using SF.Core.Abstraction.Mapping;
using SF.Entitys;

namespace SF.Web.Security.Converters
{
    public class MappingRegistration : IAutoMapperConfiguration
    {
        public void MapperConfigurationToExpression(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<PermissionEntity, Permission>().ReverseMap();
            cfg.CreateMap<RoleEntity, Role>().ReverseMap();
            cfg.CreateMap<UserRoleEntity, UserRoleEntity>().ReverseMap();
            cfg.CreateMap<IdentityResult, SecurityResult>().ReverseMap();
            cfg.CreateMap<UserEntity, ApplicationUserExtended>()
                .ForMember(src => src.Roles, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
