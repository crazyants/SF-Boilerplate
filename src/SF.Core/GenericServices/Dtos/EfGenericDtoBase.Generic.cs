/*******************************************************************************
* 命名空间: SF.Core.GenericServices
*
* 功 能： N/A
* 类 名： EfGenericDtoBase
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/7 19:54:06 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SF.Core.GenericServices.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SF.Core.GenericServices.Dtos
{
  /// <summary>
    /// This should not be used. It is used as the base for EfGenericDto and EfGenericDtoAsync
    /// and contained all the common methods/properties
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public abstract partial class EfGenericDtoBase<TEntity, TDto>
        where TEntity : class
        where TDto : EfGenericDtoBase<TEntity, TDto>
    {
        
        /// <summary>
        /// This provides the name of the name of the data item to display in success or error messages.
        /// Override if you want a more user friendly name
        /// </summary>
        internal protected virtual string DataItemName { get { return typeof (TEntity).Name; }}

        /// <summary>
        /// Override this to add .ForEach mappings that will be applied to the TEntity to TDto conversion
        /// See 'DTO data copying, Using AutoMapper for calculated properties' in the documentation 
        /// </summary>
        protected virtual Action<IMappingExpression<TEntity, TDto>> AddedDatabaseToDtoMapping { get { return null; } }

        /// <summary>
        /// Override this if your dto relies on another dto in its mapping
        /// For instance if you are mapping a property that is a type and you want that to map to a dto then call this
        /// </summary>
        protected virtual Type AssociatedDtoMapping { get { return null; } }

        /// <summary>
        /// Override this if your dto relies on multiple other dtos in its mapping
        /// For instance if you are mapping a property that is a type and you want that to map to a dto then call this
        /// </summary>
        protected virtual Type[] AssociatedDtoMappings { get { return null; } }
        
        /// <summary>
        /// This method is called to get the data table. Can be overridden if include statements are needed.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>returns an IQueryable of the table TEntity as Untracked</returns>
        protected virtual IQueryable<TEntity> GetDataUntracked(DbContext context)
        {
            return context.Set<TEntity>().AsNoTracking();
        }

        /// <summary>
        /// This provides the IQueryable command to get a list of TEntity, but projected to TDto.
        /// Can be overridden if standard AutoMapping isn't good enough, or return null if not supported
        /// </summary>
        /// <returns></returns>
        protected internal virtual IQueryable<TDto> ListQueryUntracked(DbContext context)
        {
            var query = GetDataUntracked(context).ProjectTo<TDto>(GenericServicesConfig.AutoMapperConfigs[CreateDictionaryKey<TEntity, TDto>()]);

            //We check if we need to decompile the LINQ expression so that any computed properties in the class are filled in properly
            return query;
        }

        //----------------------------------------------------------------------
        //non-overridable internal methods

        /// <summary>
        /// This copies back the keys from a newly created entity into the dto as long as there are matching properties in the Dto
        /// </summary>
        /// <param name="context"></param>
        /// <param name="newEntity"></param>
        internal protected void AfterCreateCopyBackKeysToDtoIfPresent(DbContext context, TEntity newEntity)
        {
            var dtoKeyProperies = typeof (TDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var entityKeys in context.GetKeyProperties<TEntity>())
            {
                var dtoMatchingProperty =
                    dtoKeyProperies.SingleOrDefault(
                        x => x.Name == entityKeys.Name && x.PropertyType == entityKeys.PropertyType);
                if (dtoMatchingProperty == null) continue;

                dtoMatchingProperty.SetValue(this, entityKeys.GetValue(newEntity));
            }
        }

        //---------------------------------------------------------------
        //protected methods

        /// <summary>
        /// This gets the key values from this DTO in the correct order. Used in FindItemTrackedForUpdate sync/async
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected object[] GetKeyValues(DbContext context)
        {
            var efkeyProperties = context.GetKeyProperties<TEntity>().ToArray();
            var dtoProperties = typeof(TDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var keysInOrder = efkeyProperties.Select(x => dtoProperties.SingleOrDefault(y => y.Name == x.Name && y.PropertyType == x.PropertyType)).ToArray();

            if (keysInOrder.Any(x => x == null))
                throw new KeyNotFoundException("The dto must contain all the key(s) properties from the data class.");
            
            return keysInOrder.Select(x => x.GetValue(this)).ToArray();
        }


    }
}
