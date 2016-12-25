using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SF.Core.Abstraction;
using SF.Core.Abstraction.Data;
using SF.Core.Entitys.Abstraction;
using SF.Core.Entitys;
using System.Threading.Tasks;
using System.Threading;
using SF.Core.Extensions;

namespace SF.Core.Data
{
    public class CoreDbContext : IdentityDbContext<UserEntity, RoleEntity, long, IdentityUserClaim<long>, UserRoleEntity, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
    {

        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {

          //  Database.EnsureCreated();
            //    Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Type> typeToRegisterCustomModelBuilders = new List<Type>();
            List<Type> typeToRegisterEntitys = new List<Type>();
            foreach (var assemblie in ExtensionManager.Assemblies)
            {
                //获取所有继承BaseEntity的实体
                var entityClassTypes = assemblie.ExportedTypes.Where(x =>
                //( x.GetTypeInfo().IsSubclassOf(typeof(BaseEntity)) && !x.GetTypeInfo().IsAbstract)||
                typeof(IEntityWithTypedId<long>).IsAssignableFrom(x) && !x.GetTypeInfo().IsAbstract &&
               ! x.GetTypeInfo().IsDefined(typeof(MapIgnoreAttribute),false)
                );
                typeToRegisterEntitys.AddRange(entityClassTypes);

                //获取所有继承ICustomModelBuilder的实体映射
                var customModelBuilderClassTypes = assemblie.ExportedTypes.Where(x => typeof(ICustomModelBuilder).IsAssignableFrom(x) && x.GetTypeInfo().IsClass);
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
            //      optionsBuilder.UseSqlServer(tenant.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        private static void RegiserConvention(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.ClrType.Namespace != null)
                {
                    var nameParts = entity.ClrType.Namespace.Split('.');
                    var tableName = string.Concat(nameParts[2], "_", entity.ClrType.Name);
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

        public override int SaveChanges()
        {
            // ensure auto history
            this.EnsureAutoHistory();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            // ensure auto history
            this.EnsureAutoHistory();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            // ensure auto history
            this.EnsureAutoHistory();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // ensure auto history
            this.EnsureAutoHistory();

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
