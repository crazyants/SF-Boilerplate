using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SF.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SF.Data.Extensions
{
    public static class DbContextExtension
    {
        /// <summary>
        ///  判断实体、上下文是否存在DbContext特性
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="type"></param>
        /// <param name="defaultVaule">如果不存在特性的默认值</param>
        /// <returns></returns>
        public static bool EnsureDbContextAndEntitiesIncludeDbContextAttrCache<TDbContext>(this Type type, bool defaultVaule) where TDbContext : DbContext
        {
            var dbContextAttributes = type.GetAttributesOfDeclaringType()
              .OfType<DbContextAttribute>().ToArray();

            if (!dbContextAttributes.Any())
            {
                return defaultVaule;
            }
            else
            {
                //判断特性是否所属的上下文
                foreach (var dbContextAttribute in dbContextAttributes)
                {
                        if (typeof(TDbContext).Name.Equals(dbContextAttribute.ContextType.Name))
                            return true;
                }
                return false;
            }


        }
    }
}
