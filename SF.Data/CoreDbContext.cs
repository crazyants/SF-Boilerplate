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
    public class CoreDbContext : IdentityDbContext<UserEntity, RoleEntity, long, IdentityUserClaim<long>, UserRoleEntity, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>, IStorageContext
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Type> typeToRegisterCustomModelBuilders = new List<Type>();
            List<Type> typeToRegisterEntitys = new List<Type>();
            foreach (var assemblie in ExtensionManager.Assemblies)
            {
                //获取所有继承BaseEntity的实体
                var entityClassTypes = assemblie.ExportedTypes.Where(x =>
                typeof(IEntityWithTypedId<long>).IsAssignableFrom(x) && !x.GetTypeInfo().IsAbstract
                && !x.GetTypeInfo().IsDefined(typeof(MapIgnoreAttribute), false)
                && x.EnsureDbContextAndEntitiesIncludeDbContextAttrCache<CoreDbContext>(true)
                );
                typeToRegisterEntitys.AddRange(entityClassTypes);

                //获取所有继承ICustomModelBuilder的实体映射
                var customModelBuilderClassTypes = assemblie.ExportedTypes.Where(x => typeof(ICustomModelBuilder).IsAssignableFrom(x) 
                && x.GetTypeInfo().IsClass
                && x.EnsureDbContextAndEntitiesIncludeDbContextAttrCache<CoreDbContext>(true));
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
            //optionsBuilder.UseSqlServer("Server=192.168.1.101;Database=SF_Team_2017;uid=sa;pwd=123.com.cn;Pooling=True;Min Pool Size=1;Max Pool Size=100;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=false;",
            //   b => b.MigrationsAssembly("SF.WebHost"));
            base.OnConfiguring(optionsBuilder);
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

        private static void RegiserConvention(ModelBuilder modelBuilder)
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

        private static void RegisterEntities(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
        {

            //   var entityTypes = typeToRegisters.Where(x => (x.GetTypeInfo().IsSubclassOf(typeof(Entity))|| x.GetTypeInfo().IsSubclassOf(typeof(EntityWithTypedId<>))) && !x.GetTypeInfo().IsAbstract);
            foreach (var type in typeToRegisters)
            {
                modelBuilder.Entity(type);
            }
        }

        private static void RegisterCustomMappings(ModelBuilder modelBuilder, IEnumerable<Type> typeToRegisters)
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

        //private static bool? EnsureDbContextAndEntitiesIncludeDbContextAttrCache(Type type)
        //{
        //    var dbContextAttributes = type.GetAttributesOfDeclaringType()
        //      .OfType<DbContextAttribute>().ToArray();

        //    var includeAttr = type.GetTypeInfo().GetCustomAttribute(typeof(DbContextAttribute));
        //    if (!dbContextAttributes.Any())
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        foreach (var dbContextAttribute in dbContextAttributes)
        //        {

        //            if (dbContextAttribute.DbContext is CoreDbContext)
        //                return true;
        //        }
        //    }
        //    return false;

        //}
    }
}
