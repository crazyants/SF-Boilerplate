using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SF.Core.Abstraction.Data;
using SF.Core.EFCore.Maps.Fluent;
using SF.Core.Entitys;

namespace SF.Core.Data
{
    public class CoreCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(cfg =>
            {
                cfg.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
                cfg.ToTable("Core_User");
            });

            modelBuilder.Entity<RoleEntity>()
                .ToTable("Core_Role");

            modelBuilder.Entity<IdentityUserClaim<long>>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.ToTable("Core_UserClaim");
            });

            modelBuilder.Entity<IdentityRoleClaim<long>>(b =>
            {
                b.HasKey(rc => rc.Id);
                b.ToTable("Core_RoleClaim");
            });

            modelBuilder.Entity<UserRoleEntity>(b =>
            {
                b.HasKey(ur => new { ur.UserId, ur.RoleId });
                b.HasOne(ur => ur.Role).WithMany(r => r.Users).HasForeignKey(r => r.RoleId);
                b.HasOne(ur => ur.User).WithMany(u => u.Roles).HasForeignKey(u => u.UserId);
                b.ToTable("Core_UserRole");
            });

            modelBuilder.Entity<IdentityUserLogin<long>>(b =>
            {
                b.ToTable("Core_UserLogin");
            });

            //modelBuilder.Entity<IdentityUserToken<long>>(b =>
            //{
            //    b.ToTable("Core_UserToken");
            //});

            modelBuilder.Entity<UserEntity>(u =>
            {
                u.HasOne(x => x.CurrentShippingAddress)
               .WithMany()
               .HasForeignKey(x => x.CurrentShippingAddressId)
               .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserAddressEntity>()
                .HasOne(x => x.User)
                .WithMany(a => a.UserAddresses)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AddressEntity>(x =>
            {
                x.HasOne(d => d.District)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(d => d.StateOrProvince)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                x.HasOne(d => d.Country)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                x.MapCreatedMeta().MapUpdatedMeta();
            });
            #region Security

            modelBuilder.Entity<ApiAccountEntity>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.MapCreatedMeta().MapUpdatedMeta();
                b.ToTable("Core_ApiAccount");

            });
            modelBuilder.Entity<PermissionEntity>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.MapCreatedMeta().MapUpdatedMeta();
                b.ToTable("Core_Permission");
            });
            modelBuilder.Entity<RolePermissionEntity>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.MapCreatedMeta().MapUpdatedMeta();
                b.ToTable("Core_RolePermission");
            });
            modelBuilder.Entity<PermissionScopeEntity>(b =>
            {
                b.HasKey(uc => uc.Id);

                b.ToTable("Core_PermissionScope");
            });


            // Relations
            modelBuilder.Entity<ApiAccountEntity>()
                .HasOne(x => x.Account)
                .WithMany(x => x.ApiAccounts)
                .HasForeignKey(x => x.AccountId);


            modelBuilder.Entity<RolePermissionEntity>()
                .HasOne(x => x.Permission)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.PermissionId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RolePermissionEntity>()
                .HasOne(x => x.Role)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PermissionScopeEntity>()
                .HasOne(x => x.RolePermission)
                .WithMany(x => x.Scopes)
                .HasForeignKey(x => x.RolePermissionId).OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Settings

            modelBuilder.Entity<SettingEntity>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.MapCreatedMeta().MapUpdatedMeta();
                b.ToTable("Core_Setting");
            });
            modelBuilder.Entity<SettingEntity>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.MapCreatedMeta().MapUpdatedMeta();
                b.ToTable("Core_SettingValue");
            });

            modelBuilder.Entity<SettingValueEntity>()
                .HasOne(x => x.Setting)
                .WithMany(x => x.SettingValues)
                .HasForeignKey(x => x.SettingId);

            #endregion

            #region System

            modelBuilder.Entity<DataItemEntity>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(u => u.Description).HasMaxLength(1000);
                b.MapCreatedMeta().MapUpdatedMeta();
                b.ToTable("Core_DataItem");
            });

            modelBuilder.Entity<DataItemDetailEntity>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(u => u.Description).HasMaxLength(1000);
                b.MapCreatedMeta().MapUpdatedMeta();
                b.ToTable("Core_DataItemDetail");
            });

            modelBuilder.Entity<DataItemDetailEntity>()
            .HasOne(x => x.DataItem)
            .WithMany(x => x.DataItemDetailEntitys)
            .HasForeignKey(x => x.ItemId);


            #endregion


            // enable auto history
            modelBuilder.EnableAutoHistory();

        }
    }
}
