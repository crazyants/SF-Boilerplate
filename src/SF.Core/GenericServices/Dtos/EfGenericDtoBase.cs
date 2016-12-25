/*******************************************************************************
* 命名空间: SF.Core.GenericServices
*
* 功 能： N/A
* 类 名： EfGenericDtoBase
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/7 17:36:24 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.GenericServices.Dtos
{
    /// <summary>
    /// 增删改查枚举
    /// </summary>
    [Flags]
    public enum CrudFunctions
    {
        None = 0,
        List = 1,
        Detail = 2,
        Create = 4,
        Update = 8,
        //note: no delete as delete does not need a dto

        /// <summary>
        /// By default the Create and Update services will call the method 'SetupSecondaryData' (sync and Async)
        /// which you are supposed to override. As a safety precaution the default SetupSecondaryData method
        /// will throw an exception if not overridden. 
        /// If you don't need SetupSecondaryData then set the flag 'DoesNotNeedSetup' below to stop the call.
        /// </summary>
        DoesNotNeedSetup = 256,
        AllCrudButCreate = List | Detail | Update,
        AllCrudButList = Detail | Create | Update,
        AllCrud = List | Detail | Create | Update
    }


    public abstract class EfGenericDtoBase
    {
        private bool _needsDecompile;

        /// <summary>
        /// If this flag is set then .Decompile is added to any query
        /// The flag is set on creation based on whether config UseDelegateDecompilerWhereNeeded flas is true
        /// and class's TEntity class, or  any of the associatedDTO TEntity classes ,
        /// has properties with the [Computed] attribute on them.
        /// </summary>
        internal bool NeedsDecompile
        {
            get { return _needsDecompile || ForceNeedDecompile; }
            set { _needsDecompile = value; }
        }

        /// <summary>
        /// Override and set to true if you wish to force NeedDecompile as always on in this DTO.
        /// Needed if accessing a calculated field in a related class
        /// </summary>
        protected virtual bool ForceNeedDecompile { get { return false; } }

        /// <summary>
        /// DTO支持什么功能.
        /// 每个方法检查如果不支持该服务,将抛出一个错误。
        /// </summary>
        internal protected abstract CrudFunctions SupportedFunctions { get; }
    }
}
