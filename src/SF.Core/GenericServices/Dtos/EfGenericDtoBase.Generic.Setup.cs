/*******************************************************************************
* 命名空间: SF.Core.GenericServices
*
* 功 能： N/A
* 类 名： EfGenericDtoBase
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/7 17:41:02 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using AutoMapper;
using System.Reflection;
using SF.Core.GenericServices.Internal;

namespace SF.Core.GenericServices.Dtos
{
     /// <summary>
    /// This should not be used. It is used as the base for EfGenericDto and EfGenericDtoAsync
    /// This partial class contains all the code to setup the DTO.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public abstract partial class EfGenericDtoBase<TEntity, TDto> : EfGenericDtoBase
        where TEntity : class
        where TDto : EfGenericDtoBase<TEntity, TDto>
    {


        /// <summary>
        /// Constructor. This ensures that the mappings are set up on creation of the class
        /// and sets the NeedsDecompile property based on checking 
        /// </summary>
        protected EfGenericDtoBase()
        {
            MapperSetup();
        }

        /// <summary>
        /// This is used to set up the mapping of any associated EfGenericDto 
        /// NOTE: It needs to be protected as the reflection .GetMethod doens't find a private method
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="readFromDatabase"></param>
        /// <returns></returns>
        protected void AssociatedMapperSetup(IMapperConfigurationExpression cfg, bool readFromDatabase)
        {
            var needsDecompile = false;
            if (readFromDatabase)
                CreateReadFromDatabaseMapping(cfg, ref needsDecompile);
            else
                CreateWriteToDatabaseMapping(cfg);
        }

        //---------------------------------------------------------------------
        //private methods

        /// <summary>
        /// This sets all the AutoMapper mapping that this dto needs. It is called from the base constructor
        /// It also makes sure that any associated dto mappings are set up as the order of creation is not fixed
        /// </summary>
        private void MapperSetup()
        {
            var needsDecompile = false;
            GenericServicesConfig.AutoMapperConfigs.GetOrAdd(CreateDictionaryKey<TEntity, TDto>(),
                config => new MapperConfiguration(cfg => CreateReadFromDatabaseMapping(cfg, ref needsDecompile)));

            //now set up NeedsDecompile any associated mappings. See comments on AssociatedDtoMapping for why these are needed
            NeedsDecompile = ForceNeedDecompile ;
            NeedsDecompile |= needsDecompile;

            if (SupportedFunctions.HasFlag(CrudFunctions.Update) | SupportedFunctions.HasFlag(CrudFunctions.Create))
                //Only setup TDto->TEntity mapping if needed
                GenericServicesConfig.AutoMapperConfigs.GetOrAdd(CreateDictionaryKey<TDto, TEntity>(), 
                    config => new MapperConfiguration(CreateWriteToDatabaseMapping));
        }

        /// <summary>
        /// This sets up the AutoMapper mapping for a copy from the TEntity to the TDto.
        /// It applies any extra mapping provided by AddedDatabaseToDtoMapping if not null
        /// </summary>
        private void CreateReadFromDatabaseMapping(IMapperConfigurationExpression cfg, ref bool needsDecompile)
        {
            if (AddedDatabaseToDtoMapping == null)
                cfg.CreateMap<TEntity, TDto>();
            else
                AddedDatabaseToDtoMapping(cfg.CreateMap<TEntity, TDto>());

            needsDecompile = needsDecompile | SetupAllAssociatedMappings(cfg, true);
        }

        /// <summary>
        /// This sets up the AutoMapper mapping for a copy from the TDto to the TEntity.
        /// Note that properties which have the [DoNotCopyBackToDatabase] attribute will not be copied
        /// </summary>
        private void CreateWriteToDatabaseMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<TDto, TEntity>().IgnoreMarkedProperties();
            SetupAllAssociatedMappings(cfg, false);
        }

        protected static string CreateDictionaryKey<TFrom, TTo>()
        {
            return typeof (TFrom).FullName + "=" + typeof (TTo).FullName;
        }

        /// <summary>
        /// Set up any requested assocaiated mappings
        /// </summary>
        private bool SetupAllAssociatedMappings(IMapperConfigurationExpression cfg, bool readFromDatabase)
        {
            var shouldDecompile = false;
            if (AssociatedDtoMapping != null)
                shouldDecompile |= CheckAndSetupAssociatedMapping(AssociatedDtoMapping, cfg, readFromDatabase);

            if (AssociatedDtoMappings == null) return shouldDecompile;

            foreach (var associatedDtoMapping in AssociatedDtoMappings)
                shouldDecompile |= CheckAndSetupAssociatedMapping(associatedDtoMapping, cfg, readFromDatabase);

            return shouldDecompile;
        }

        private static bool CheckAndSetupAssociatedMapping(Type associatedDtoMapping, IMapperConfigurationExpression cfg, bool readFromDatabase)
        {
            if (!associatedDtoMapping.GetTypeInfo().IsSubclassOf(typeof(EfGenericDtoBase)))
                throw new InvalidOperationException("You have not supplied a class based on EfGenericDto to set up the mapping.");

            //create the acssociated dto to get the AssociatedMapperSetup method
            var associatedDto = Activator.CreateInstance(associatedDtoMapping, new object[] { });
            var method = associatedDtoMapping.GetMethod(nameof(AssociatedMapperSetup),
                BindingFlags.NonPublic | BindingFlags.Instance);

            method.Invoke(associatedDto, new object[] { cfg, readFromDatabase});
            return ((EfGenericDtoBase)associatedDto).NeedsDecompile;
        }


    }
}
