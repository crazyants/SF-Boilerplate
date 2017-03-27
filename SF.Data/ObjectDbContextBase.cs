using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SF.Core.Abstraction;
using SF.Core.Abstraction.Data;
using SF.Entitys.Abstraction;
using SF.Entitys;
using System.Threading.Tasks;
using System.Threading;
using SF.Core.Extensions;
using SF.Core;
using SF.Core.Common;
using SF.Data.Extensions;

namespace SF.Data
{
    /// <summary>
    /// 上下文抽象基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class ObjectDbContextBase<TKey, TDbContext> : DbContext where TDbContext : DbContext
    {
        public ObjectDbContextBase(DbContextOptions<TDbContext> options) : base(options)
        {

        }
        /// <summary>
        /// 自定实体创建
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Type> typeToRegisterCustomModelBuilders = new List<Type>();
            List<Type> typeToRegisterEntitys = new List<Type>();
            foreach (var assemblie in ExtensionManager.Assemblies)
            {
                //获取所有继承BaseEntity的实体
                var entityClassTypes = assemblie.ExportedTypes.Where(x =>
                typeof(IEntityWithTypedId<TKey>).IsAssignableFrom(x) && !x.GetTypeInfo().IsAbstract &&
                !x.GetTypeInfo().IsDefined(typeof(MapIgnoreAttribute), false) &&
                x.EnsureDbContextAndEntitiesIncludeDbContextAttrCache<TDbContext>(false));
                typeToRegisterEntitys.AddRange(entityClassTypes);

                //获取所有继承ICustomModelBuilder的实体映射
                var customModelBuilderClassTypes = assemblie.ExportedTypes.Where(x => typeof(ICustomModelBuilder).IsAssignableFrom(x)
                && x.GetTypeInfo().IsClass
                && x.EnsureDbContextAndEntitiesIncludeDbContextAttrCache<TDbContext>(false));
                typeToRegisterCustomModelBuilders.AddRange(customModelBuilderClassTypes);
            }

            //把实体注册到模型构建中
            RegisterEntities(modelBuilder, typeToRegisterEntitys);
            //构建所有继承ICustomModelBuilder的实体映射
            RegiserConvention(modelBuilder);

            base.OnModelCreating(modelBuilder);

            RegisterCustomMappings(modelBuilder, typeToRegisterCustomModelBuilders);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected static void RegiserConvention(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.ClrType.Namespace != null)
                {
                    var nameParts = entity.ClrType.Namespace.Split('.');
                    var tableName = string.Concat(nameParts[1], "_", entity.ClrType.Name);
                    if (nameParts.Contains("Module"))
                        tableName = string.Concat(nameParts[2], "_", entity.ClrType.Name);
                    modelBuilder.Entity(entity.Name).ToTable(tableName);
                }
            }
        }

        protected static void RegisterEntities(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {

            //   var entityTypes = typeToRegisters.Where(x => (x.GetTypeInfo().IsSubclassOf(typeof(Entity))|| x.GetTypeInfo().IsSubclassOf(typeof(EntityWithTypedId<>))) && !x.GetTypeInfo().IsAbstract);
            foreach (var type in typeToRegisters)
            {
                modelBuilder.Entity(type);
            }
        }

        protected static void RegisterCustomMappings(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {
            //  var customModelBuilderTypes = typeToRegisters.Where(x => typeof(ICustomModelBuilder).IsAssignableFrom(x));
            foreach (var builderType in typeToRegisters)
            {
                if (builderType != null && builderType != typeof(ICustomModelBuilder))
                {
                    var builder = (ICustomModelBuilder)Activator.CreateInstance(builderType);
                    builder.Build(modelBuilder);
                }
            }
        }

        public override int SaveChanges()
        {
            // ensure auto history
            this.EnsureAutoHistory();
            //var changedEntityNames = this.GetChangedEntityNames();

            var result = base.SaveChanges();
            //  _cacheServiceProvider.InvalidateCacheDependencies(changedEntityNames);

            return result;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {

            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            return result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            // ensure auto history
            this.EnsureAutoHistory();
            //  var changedEntityNames = this.GetChangedEntityNames();

            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            //  _cacheServiceProvider.InvalidateCacheDependencies(changedEntityNames);
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            var result = base.SaveChangesAsync(cancellationToken);
            return result;

        }


    }
}
