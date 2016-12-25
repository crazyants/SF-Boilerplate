/*******************************************************************************
* 命名空间: SF.Core.GenericServices.Services.Implementation
*
* 功 能： N/A
* 类 名： CreateService
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/9 17:05:16 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using SF.Core.GenericServices.Dtos;
using SF.Core.GenericServices.Helper;
using SF.Core.GenericServices.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.GenericServices.Services.Implementation
{

    public class CreateService : ICreateService
    {
        private readonly DbContext _db;

        public CreateService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This adds a new entity class to the database with error checking
        /// </summary>
        /// <typeparam name="T">The type of the data to output. 
        /// Type must be a type either an EF data class or one of the EfGenericDto's</typeparam>
        /// <param name="newItem">either entity class or dto to create the data item with</param>
        /// <returns>status</returns>
        public ISuccessOrErrors Create<T>(T newItem) where T : class
        {
            var service = DecodeToService<CreateService>.CreateCorrectService<T>(WhatItShouldBe.SyncAnything, _db);
            return service.Create(newItem);
        }

        /// <summary>
        /// This is available to reset any secondary data in the dto. Call this if the ModelState was invalid and
        /// you need to display the view again with errors
        /// </summary>
        /// <param name="dto">Must be a dto inherited from EfGenericDto</param>
        /// <returns></returns>
        public T ResetDto<T>(T dto) where T : class
        {
            var service = DecodeToService<UpdateService>.CreateCorrectService<T>(WhatItShouldBe.SyncSpecificDto, _db);
            return service.ResetDto(dto);
        }
    }

    //-----------------------------------------------
    //direct service

    public class CreateService<TEntity> : ICreateService<TEntity> where TEntity : class
    {
        private readonly DbContext _db;

        public CreateService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This adds a new entity class to the database with error checking
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns>status</returns>
        public ISuccessOrErrors Create(TEntity newItem)
        {
            _db.Set<TEntity>().Add(newItem);
            var result = _db.SaveChangesWithChecking();
            if (result.IsValid)
                result.SetSuccessMessage("Successfully created {0}.", typeof(TEntity).Name);

            return result;
        }
    }

    //---------------------------------------------------------------------------
    //DTO version

    public class CreateService<TEntity, TDto> : ICreateService<TEntity, TDto>
        where TEntity : class, new()
        where TDto : EfGenericDto<TEntity, TDto>, new()
    {
        private readonly DbContext _db;


        public CreateService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This uses a dto to create a data class which it writes to the database with error checking
        /// </summary>
        /// <param name="dto">If an error then its resets any secondary data so that you can reshow the dto</param>
        /// <returns>status</returns>
        public ISuccessOrErrors Create(TDto dto)
        {
            ISuccessOrErrors result = new SuccessOrErrors();
            if (!dto.SupportedFunctions.HasFlag(CrudFunctions.Create))
                return result.AddSingleError("Create of a new {0} is not supported in this mode.", dto.DataItemName);

            var statusWithData = dto.CreateDataFromDto(_db, dto);    //creates the new data and fills in the properties
            result = statusWithData as ISuccessOrErrors;             //convert to normal status as need errors to fall through propertly
            if (result.IsValid)
            {
                _db.Set<TEntity>().Add(statusWithData.Result);
                result = _db.SaveChangesWithChecking();
                dto.AfterCreateCopyBackKeysToDtoIfPresent(_db, statusWithData.Result);
                if (result.IsValid)
                    return result.SetSuccessMessage("Successfully created {0}.", dto.DataItemName);
            }

            //otherwise there are errors
            if (!dto.SupportedFunctions.HasFlag(CrudFunctions.DoesNotNeedSetup))
                //we reset any secondary data as we expect the view to be reshown with the errors
                dto.SetupSecondaryData(_db, dto);
            return result;

        }

        /// <summary>
        /// This is available to reset any secondary data in the dto. Call this if the ModelState was invalid and
        /// you need to display the view again with errors
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public TDto ResetDto(TDto dto)
        {
            if (!dto.SupportedFunctions.HasFlag(CrudFunctions.DoesNotNeedSetup))
                //we reset any secondary data as we expect the view to be reshown with the errors
                dto.SetupSecondaryData(_db, dto);

            return dto;
        }

    }
}
