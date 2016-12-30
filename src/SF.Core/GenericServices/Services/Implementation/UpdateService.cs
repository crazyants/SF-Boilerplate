/*******************************************************************************
* 命名空间: SF.Core.GenericServices.Services.Implementation
*
* 功 能： N/A
* 类 名： UpdateService
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/9 17:30:29 疯狂蚂蚁 初版
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
    public class UpdateService : IUpdateService
    {
        private readonly DbContext _db;

        public UpdateService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This updates the data in the database using the input data
        /// </summary>
        /// <typeparam name="T">The type of input data. 
        /// Type must be a type either an EF data class or one of the EfGenericDto's</typeparam>
        /// <param name="data">data to update the class. If Dto then copied over to data class</param>
        /// <returns></returns>
        public ISuccessOrErrors Update<T>(T data) where T : class
        {
            var service = DecodeToService<UpdateService>.CreateCorrectService<T>(WhatItShouldBe.SyncAnything, _db);
            return service.Update(data);
        }

        /// <summary>
        /// This is available to reset any secondary data in the dto. Call this if the ModelState was invalid and
        /// you need to display the view again with errors
        /// </summary>
        /// <param name="dto">Must be a dto inherited from EfGenericDtoAsync</param>
        /// <returns></returns>
        public T ResetDto<T>(T dto) where T : class
        {
            var service = DecodeToService<UpdateService>.CreateCorrectService<T>(WhatItShouldBe.SyncSpecificDto, _db);
            return service.ResetDto(dto);
        }
    }

    //--------------------------------
    //direct

    public class UpdateService<TEntity> : IUpdateService<TEntity> where TEntity : class
    {
        private readonly DbContext _db;

        public UpdateService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This updates the entity data class directly
        /// </summary>
        /// <param name="itemToUpdate"></param>
        /// <returns>status</returns>
        public ISuccessOrErrors Update(TEntity itemToUpdate)
        {
            if (itemToUpdate == null)
                throw new ArgumentNullException("itemToUpdate", "The item provided was null.");

            //Set the entry as modified
            _db.Entry(itemToUpdate).State = EntityState.Modified;

            var result = _db.SaveChangesWithChecking();
            if (result.IsValid)
                result.SetSuccessMessage("Successfully updated {0}.", typeof(TEntity).Name);

            return result;
        }
    }

    //------------------------------------------------------------------------
    //DTO version

    public class UpdateService<TEntity, TDto> : IUpdateService<TEntity, TDto>
        where TEntity : class, new()
        where TDto : EfGenericDto<TEntity, TDto>, new()
    {
        private readonly DbContext _db;

        public UpdateService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This updates the entity data by copying over the relevant dto data.
        /// </summary>
        /// <param name="dto">If an error then its resets any secondary data so that you can reshow the dto</param>
        /// <returns>status</returns>
        public ISuccessOrErrors Update(TDto dto)
        {
            ISuccessOrErrors result = new SuccessOrErrors();
            if (!dto.SupportedFunctions.HasFlag(CrudFunctions.Update))
                return result.AddSingleError("Delete of a {0} is not supported in this mode.", dto.DataItemName);

            var itemToUpdate = dto.FindItemTrackedForUpdate(_db);
            if (itemToUpdate == null)
                return result.AddSingleError("Could not find the {0} you requested.", dto.DataItemName);

            result = dto.UpdateDataFromDto(_db, dto, itemToUpdate); //update those properties we want to change
            if (result.IsValid)
            {
                result = _db.SaveChangesWithChecking();
                if (result.IsValid)
                    return result.SetSuccessMessage("Successfully updated {0}.", dto.DataItemName);
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
