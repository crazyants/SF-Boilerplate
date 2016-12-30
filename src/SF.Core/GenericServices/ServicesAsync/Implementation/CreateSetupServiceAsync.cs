/*******************************************************************************
* 命名空间: SF.Core.GenericServices.ServicesAsync.Implementation
*
* 功 能： N/A
* 类 名： CreateSetupServiceAsync
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/9 17:32:30 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using SF.Core.GenericServices.Dtos;
using SF.Core.GenericServices.Internal;
using System.Threading.Tasks;

namespace SF.Core.GenericServices.ServicesAsync.Implementation
{

    public class CreateSetupServiceAsync : ICreateSetupServiceAsync
    {
        private readonly DbContext _db;

        public CreateSetupServiceAsync(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This returns the dto with any data that is needs for the view setup in it
        /// </summary>
        /// <typeparam name="TDto">The type of the data to output. This must be EfGeneric Dto</typeparam>
        /// <returns>The dto with any secondary data filled in</returns>
        public async Task<TDto> GetDtoAsync<TDto>() where TDto : class
        {
            var service = DecodeToService<CreateSetupServiceAsync>.CreateCorrectService<TDto>(WhatItShouldBe.AsyncSpecificDto, _db);
            return await service.GetDtoAsync().ConfigureAwait(false);
        }
    }


    public class CreateSetupServiceAsync<TEntity, TDto> : ICreateSetupServiceAsync<TEntity, TDto>
        where TEntity : class, new()
        where TDto : EfGenericDtoAsync<TEntity, TDto>, new()
    {
        private readonly DbContext _db;

        public CreateSetupServiceAsync(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This returns the dto with any data that is needs for the view setup in it
        /// </summary>
        /// <returns>A TDto which has had the SetupSecondaryData method called on it</returns>
        public async Task<TDto> GetDtoAsync()
        {
            var dto = new TDto();
            if (!dto.SupportedFunctions.HasFlag(CrudFunctions.DoesNotNeedSetup))
                await dto.SetupSecondaryDataAsync(_db, dto).ConfigureAwait(false);

            return dto;
        }
    }
}
