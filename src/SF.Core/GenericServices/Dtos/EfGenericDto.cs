/*******************************************************************************
* 命名空间: SF.Core.GenericServices
*
* 功 能： N/A
* 类 名： EfGenericDto
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/8 10:43:27 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SF.Core.GenericServices.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SF.Core.GenericServices.Dtos
{
    public abstract class EfGenericDto<TEntity, TDto> : EfGenericDtoBase<TEntity, TDto>
     where TEntity : class, new()
     where TDto : EfGenericDto<TEntity, TDto>, new()
    {

        protected EfGenericDto()
        {
        }

        /// <summary>
        /// This function will be called at the end of CreateSetupService and UpdateSetupService to setup any
        /// additional data in the dto used to display dropdownlists etc. 
        /// It is also called at the end of the CreateService or UpdateService if there are errors, so that
        /// the data is available if the form needs to be reshown.
        /// This function should be overridden if the dto needs additional data setup 
        /// </summary>
        /// <returns></returns>
        protected internal virtual void SetupSecondaryData(DbContext db, TDto dto)
        {
            if (!SupportedFunctions.HasFlag(CrudFunctions.DoesNotNeedSetup))
                throw new InvalidOperationException("SupportedFunctions flags say that setup of secondary data is needed, but did not override the SetupSecondaryData method.");
        }

        /// <summary>
        /// Used only by Update. This returns the TEntity item that fits the key(s) in the DTO.
        /// Override this if you need to include any related entries when doing a complex update.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected internal virtual TEntity FindItemTrackedForUpdate(DbContext context)
        {
            return context.Set<TEntity>().Find(GetKeyValues(context));
        }

        /// <summary>
        /// This is used in a create. It copies only the properties in TDto that have public setter into the TEntity.
        /// You can override this if you need a more complex copy
        /// </summary>
        /// <param name="context"></param>
        /// <param name="source"></param>
        /// <returns>status which, if Valid, has new TEntity with data from DTO copied in</returns>
        protected internal virtual ISuccessOrErrors<TEntity> CreateDataFromDto(DbContext context, TDto source)
        {
            var result = new TEntity();
            var mapper = GenericServicesConfig.AutoMapperConfigs[CreateDictionaryKey<TDto, TEntity>()].CreateMapper();
            mapper.Map(source, result);
            return new SuccessOrErrors<TEntity>(result, "Successful copy of data");
        }

        /// <summary>
        /// This is used in an update. It copies only the properties in TDto that do not have the [DoNotCopyBackToDatabase] on them.
        /// You can override this if you need a more complex copy
        /// </summary>
        /// <param name="context"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <return>status. destination is only valid if status.IsValid</return>
        protected internal virtual ISuccessOrErrors UpdateDataFromDto(DbContext context, TDto source, TEntity destination)
        {
            var mapper = GenericServicesConfig.AutoMapperConfigs[CreateDictionaryKey<TDto, TEntity>()].CreateMapper();
            mapper.Map(source, destination);
            return SuccessOrErrors.Success("Successful copy of data");
        }

        /// <summary>
        /// This copies an existing TEntity into a new dto using a Lambda expression to define the where clause
        /// It copies TEntity properties into all TDto properties that have accessable setters, i.e. not private
        /// </summary>
        /// <returns>status. If valid result is dto. Otherwise null</returns>
        protected internal virtual ISuccessOrErrors<TDto> DetailDtoFromDataIn(DbContext context,
            Expression<Func<TEntity, bool>> predicate)
        {
            var query = GetDataUntracked(context).Where(predicate).ProjectTo<TDto>(
                GenericServicesConfig.AutoMapperConfigs[CreateDictionaryKey<TEntity, TDto>()]);

            //We check if we need to decompile the LINQ expression so that any computed properties in the class are filled in properly
            return query.RealiseSingleWithErrorChecking();
        }

    }
}
