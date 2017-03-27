
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using SF.Core.AutoHistorys.Internal;
using SF.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// Represents a plugin for Microsoft.EntityFrameworkCore to support automatically recording data changes history.
    /// </summary>
    public static class DbContextAutoHistoryExtensions
    {
        #region Private fields
        // Entities Include/Ignore attributes cache
        private static readonly Dictionary<Type, bool?> EntitiesIncludeIgnoreAttrCache = new Dictionary<Type, bool?>();

        #endregion
        /// <summary>
        /// Ensures the automatic history.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void EnsureAutoHistory(this DbContext context)
        {
            // TODO: only record the changed properties.
            var jsonSetting = new JsonSerializerSettings
            {
                ContractResolver = new EntityContractResolver(context),
            };

            var entries = context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged
                         && e.State != EntityState.Detached
                         && IncludeEntity(e)).ToList();
            if (entries.Count == 0)
            {
                return;
            }
            foreach (var entry in entries)
            {
                var history = new AutoHistory
                {
                    TypeName = entry.Entity.GetType().FullName,
                };

                switch (entry.State)
                {
                    case EntityState.Added:
                        // REVIEW: what's the best way to do this?
                        history.SourceId = "0";
                        history.Kind = EntityState.Added;
                        history.AfterJson = JsonConvert.SerializeObject(entry.Entity, Formatting.Indented, jsonSetting);
                        break;
                    case EntityState.Deleted:
                        history.SourceId = entry.PrimaryKey();
                        history.Kind = EntityState.Deleted;
                        history.BeforeJson = JsonConvert.SerializeObject(entry.Entity, Formatting.Indented, jsonSetting);
                        break;
                    case EntityState.Modified:
                        history.SourceId = entry.PrimaryKey();
                        history.Kind = EntityState.Modified;
                        history.BeforeJson = JsonConvert.SerializeObject(entry.Original(), Formatting.Indented, jsonSetting);
                        history.AfterJson = JsonConvert.SerializeObject(entry.Entity, Formatting.Indented, jsonSetting);
                        break;
                    default:
                        continue;
                }

                context.Add(history);
            }
        }

        private static object Original(this EntityEntry entry)
        {
            var type = entry.Entity.GetType();
            var typeInfo = type.GetTypeInfo();

            // Create a entity instance.
            var entity = Activator.CreateInstance(type, true);

            // Get the mapped properties for the entity type from EF metadata.
            // (include shadow properties, not include navigations)
            var properties = entry.Metadata.GetProperties();
            foreach (var property in properties)
            {
                // TODO: Supports the shadow properties
                if (property.IsShadowProperty)
                {
                    continue;
                }

                var entityProperty = typeInfo.GetProperty(property.Name);

                // Get the original value
                var original = entry.Property(property.Name).OriginalValue;

                // Set the original value to entity property.
                entityProperty.SetValue(entity, original);
            }

            return entity;
        }

        private static string PrimaryKey(this EntityEntry entry)
        {
            var key = entry.Metadata.FindPrimaryKey();

            var values = new List<object>();
            foreach (var property in key.Properties)
            {
                var value = entry.Property(property.Name).CurrentValue;
                if (value != null)
                {
                    values.Add(value);
                }
            }

            return string.Join(",", values);
        }

        private static bool IncludeEntity(EntityEntry entry)
        {
            var type = entry.Entity.GetType();
            bool? result = EnsureEntitiesIncludeIgnoreAttrCache(type); //true:excluded false=ignored null=unknown
            // Include all, except the explicitly ignored entities
            return result == null || result.Value;
        }

        private static bool? EnsureEntitiesIncludeIgnoreAttrCache(Type type)
        {
            if (!EntitiesIncludeIgnoreAttrCache.ContainsKey(type))
            {
                var includeAttr = type.GetTypeInfo().GetCustomAttribute(typeof(AuditIncludeAttribute));
                if (includeAttr != null)
                {
                    EntitiesIncludeIgnoreAttrCache[type] = true; // Type Included by IncludeAttribute
                }
                else if (type.GetTypeInfo().GetCustomAttribute(typeof(AuditIgnoreAttribute)) != null)
                {
                    EntitiesIncludeIgnoreAttrCache[type] = false; // Type Ignored by IgnoreAttribute
                }
                else
                {
                    EntitiesIncludeIgnoreAttrCache[type] = null; // No attribute
                }
            }
            return EntitiesIncludeIgnoreAttrCache[type];
        }


    }
}
