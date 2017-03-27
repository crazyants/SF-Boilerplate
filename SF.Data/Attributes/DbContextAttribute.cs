using Microsoft.EntityFrameworkCore;
using System;

namespace SF.Data
{
    ///// <summary>
    ///// 如果存在多个上下文DbContext情况下，用于区分实体、实体关系映射属于哪个一个上下文
    ///// </summary>
    //[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    //public sealed class DbContextAttribute : Attribute
    //{
    //    public DbContext DbContext { get; set; }


    //    /// <summary>
    //    /// 所属上下文
    //    /// </summary>
    //    /// <param name="dbContext"></param>
    //    public DbContextAttribute(DbContext dbContext)
    //    {
    //        DbContext = dbContext;
    //    }

    //}
}