

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
 
namespace SimpleFramework.Module.EFLogging.Data
{
    public static class Extensions
    {

        public static void RemoveAll<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }

    }
}
