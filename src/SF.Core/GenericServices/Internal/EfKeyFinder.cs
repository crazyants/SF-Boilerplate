
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace SF.Core.GenericServices.Internal
{

    internal static class EfKeyFinder
    {

        private static readonly ConcurrentDictionary<Type, IReadOnlyCollection<PropertyInfo>> KeyCache = new ConcurrentDictionary<Type, IReadOnlyCollection<PropertyInfo>>();

        /// <summary>
        /// This returns PropertyInfos for all the properties in the class that are found in the entity framework metadata 
        /// </summary>
        /// <typeparam name="TClass">The class must belong to a class that entity framework has in its metadata</typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IReadOnlyCollection<PropertyInfo> GetKeyProperties<TClass>(this DbContext context) where TClass : class
        {
            return KeyCache.GetOrAdd(typeof(TClass), type => FindKeys(type, context));
        }

        private static List<PropertyInfo> FindKeys(Type type, DbContext context)
        {
            // Get the entity type from the model that maps to the CLR type
            var entityType = context.Model.GetEntityTypes()
                    .SingleOrDefault(e => e.ClrType == type);

            if (entityType == null)
                throw new InvalidOperationException("This method expects a entity class. Did you provide a DTO by mistake?");

            var keyProperties = entityType.FindPrimaryKey().Properties.Select(x=>x.PropertyInfo);
            if (!keyProperties.Any() || keyProperties.Any(x => x == null))
                throw new NullReferenceException(string.Format("Failed to find key property by name in type {0}", type.Name));

            return keyProperties.ToList();
        }


    }
}
