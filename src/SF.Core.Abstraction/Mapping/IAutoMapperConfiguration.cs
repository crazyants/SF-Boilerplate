/*******************************************************************************
* 命名空间: SF.Core.Abstraction.Mapping
*
* 功 能： N/A
* 类 名： IMappingRegistration
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/5 11:06:36 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using AutoMapper;

namespace SF.Core.Abstraction.Mapping
{
    /// <summary>
    /// AutoMapper配置注册
    /// </summary>
    public interface IAutoMapperConfiguration
    {
        void MapperConfigurationToExpression(IMapperConfigurationExpression cfg);
    }
}
