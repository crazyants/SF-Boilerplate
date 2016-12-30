/*******************************************************************************
* 命名空间: SF.Core.GenericServices.Services.Implementation
*
* 功 能： N/A
* 类 名： CreateSetupService
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/9 17:25:09 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using SF.Core.GenericServices.Dtos;
using SF.Core.GenericServices.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.GenericServices.Services.Implementation
{

    public class CreateSetupService : ICreateSetupService
    {

        private readonly DbContext _db;

        public CreateSetupService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This returns the dto with any data that is needs for the view setup in it
        /// </summary>
        /// <typeparam name="TDto">The type of the data to output. This must be EfGeneric Dto</typeparam>
        /// <returns>The dto with any secondary data filled in</returns>
        public TDto GetDto<TDto>() where TDto : class
        {
            var service = DecodeToService<CreateSetupService>.CreateCorrectService<TDto>(WhatItShouldBe.SyncSpecificDto, _db);
            return service.GetDto();
        }
    }

    public class CreateSetupService<TEntity, TDto> : ICreateSetupService<TEntity, TDto>
        where TEntity : class, new()
        where TDto : EfGenericDto<TEntity, TDto>, new()
    {
        private readonly DbContext _db;

        public CreateSetupService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This returns the dto with any data that is needs for the view setup in it
        /// </summary>
        /// <returns>A TDto which has had the SetupSecondaryData method called on it</returns>
        public TDto GetDto()
        {
            var dto = new TDto();
            if (!dto.SupportedFunctions.HasFlag(CrudFunctions.DoesNotNeedSetup))
                dto.SetupSecondaryData(_db, dto);

            return dto;
        }
    }
}
