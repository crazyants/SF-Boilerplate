/*******************************************************************************
* 命名空间: SF.Module.Backend.ViewModels.Mappings
*
* 功 能： N/A
* 类 名： ViewModelToDomainMappingProfile
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/23 15:06:02 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using AutoMapper;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.ViewModels.Mappings
{
    public class DataItemViewModelMappingProfile : Profile
    {
            public DataItemViewModelMappingProfile()
            {
                CreateMap<DataItemViewModel, DataItemEntity>();
             
            }
        
    }
}
