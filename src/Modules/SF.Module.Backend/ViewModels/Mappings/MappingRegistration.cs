/*******************************************************************************
* 命名空间: SF.Core.Security.Converters
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
using SF.Core.Entitys;
using SF.Core.Web.Base.Mapper.Extensions;

namespace SF.Module.Backend.ViewModels.Mappings
{
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        public void MapperConfigurationToExpression(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DataItemViewModel, DataItemEntity>()
                .MapperExpressCreatedMeta()
                .MapperExpressUpdatedMeta()
                .MapperExpressDeleteMeta()
                .ReverseMap();

            cfg.CreateMap<DataItemDetailViewModel, DataItemDetailEntity>()
              .MapperExpressCreatedMeta()
              .MapperExpressUpdatedMeta()
              .MapperExpressDeleteMeta()
              .ReverseMap();

            cfg.CreateMap<SF.Module.Backend.ViewModels.Setting.SettingViewModel, SF.Core.Settings.SettingEntry>()
            .ReverseMap();
        }
    }

   
}
