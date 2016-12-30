/*******************************************************************************
* 命名空间: SF.Core.GenericServices.Services.Implementation
*
* 功 能： N/A
* 类 名： ListService
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/9 17:29:58 疯狂蚂蚁 初版
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

    public class ListService : IListService
    {
        private readonly DbContext _db;

        public ListService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This returns an IQueryable list of all items of the given type
        /// </summary>
        /// <typeparam name="T">The type of the data to output. 
        /// Type must be a type either an EF data class or a class inherited from the EfGenericDto or EfGenericDtoAsync</typeparam>
        /// <returns>note: the list items are not tracked</returns>
        public IQueryable<T> GetAll<T>() where T : class, new()
        {
            var service = DecodeToService<ListService>.CreateCorrectService<T>(WhatItShouldBe.SyncAnything, _db);
            return service.GetAll();
        }
    }


    public class ListService<TEntity> : IListService<TEntity> where TEntity : class
    {
        private readonly DbContext _db;

        public ListService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This returns an IQueryable list of all items of the given type
        /// </summary>
        /// <returns>note: the list items are not tracked</returns>
        public IQueryable<TEntity> GetAll()
        {
            return _db.Set<TEntity>().AsNoTracking();
        }

    }

    //---------------------------------------------------------------------------
    //DTO version

    public class ListService<TEntity, TDto> : IListService<TEntity, TDto>
        where TEntity : class
        where TDto : EfGenericDtoBase<TEntity, TDto>, new()
    {
        private readonly DbContext _db;

        public ListService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This returns an IQueryable list of all items of the given TEntity, but transformed into TDto data type
        /// </summary>
        /// <returns>note: the list items are not tracked</returns>
        public IQueryable<TDto> GetAll()
        {
            var tDto = new TDto();
            if (!tDto.SupportedFunctions.HasFlag(CrudFunctions.List))
                throw new InvalidOperationException("This DTO does not support listings.");

            return tDto.ListQueryUntracked(_db);
        }
    }
}
