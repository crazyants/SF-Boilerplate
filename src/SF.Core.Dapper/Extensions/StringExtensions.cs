using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Dapper.Extensions
{
    public static class StringExtensions
    {
        public static Type GetEntityType<T>(this string className)
        {
            string name_space = "SimpleFramework.Core.Dapper.";
            var objectType = Type.GetType(name_space + className);
            if (objectType == null)
            {
                objectType = typeof(T);
            }

            return objectType;
        }
    }
}
