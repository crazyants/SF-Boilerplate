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

            modelBuilder.Entity<UserLocation>(entity =>
        {
            entity.ToTable("Core_UserLocation");
            entity.HasKey(p => p.Id);
           ;
            entity.Property(p => p.IpAddress)
                .HasMaxLength(50)
             ;
            entity.HasIndex(p => p.IpAddress);

            entity.Property(p => p.IpAddressLong)
             ;

            entity.Property(p => p.Isp)
                .HasMaxLength(255)
             ;

            entity.Property(p => p.Continent)
                .HasMaxLength(255)
             ;

            entity.Property(p => p.Country)
                .HasMaxLength(255)
             ;

            entity.Property(p => p.Region)
                .HasMaxLength(255)
             ;

            entity.Property(p => p.City)
                .HasMaxLength(255)
             ;

            entity.Property(p => p.TimeZone)
                .HasMaxLength(255)
             ;

            entity.Property(p => p.FirstCaptureUtc)
            ;

            entity.Property(p => p.LastCaptureUtc)
            ;

            entity.Property(p => p.Latitude)
               .ForSqlServerHasColumnType("float")
            ;

            entity.Property(p => p.Longitude)
               .ForSqlServerHasColumnType("float")
            ;

            entity.Property(p => p.HostName)
                .HasMaxLength(255)
             ;

            entity.HasIndex(p => p.UserId);
            entity.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
        });

            modelBuilder.Entity<RoleEntity>(b =>
                {
                    b.HasKey(uc => uc.Id);
                    b.ToTable("Core_Role");
                });

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
                b.Property(u => u.LoginProvider).HasMaxLength(280);
                b.Property(u => u.ProviderKey).HasMaxLength(280);
                b.Property(u => u.ProviderDisplayName).HasMaxLength(500);
            });

            modelBuilder.Entity<IdentityUserToken<long>>(b =>
            {
                b.ToTable("Core_UserToken");
                b.HasKey(p => p.UserId);
                b.Property(u => u.Value).HasMaxLength(280);
                b.Property(u => u.Name).HasMaxLength(280);
                b.Property(u => u.LoginProvider).HasMaxLength(280);

            });

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

                x.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta().MapDeletedMeta();
            });
            #region Security

            modelBuilder.Entity<ApiAccountEntity>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
                b.ToTable("Core_ApiAccount");

            });
            modelBuilder.Entity<PermissionEntity>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
                b.ToTable("Core_Permission");
            });
            modelBuilder.Entity<RolePermissionEntity>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
                b.ToTable("Core_RolePermission");
            });
            modelBuilder.Entity<PermissionScopeEntity>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
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
                b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
                b.ToTable("Core_Setting");
            });
            modelBuilder.Entity<SettingEntity>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
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
                b.Property(u => u.ItemName).HasMaxLength(1000);
                b.Property(u => u.ItemCode).HasMaxLength(1000);
                b.Property(u => u.Description).HasMaxLength(1000);
                b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
                b.ToTable("Core_DataItem");
            });

            modelBuilder.Entity<DataItemDetailEntity>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(u => u.Description).HasMaxLength(1000);
                b.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta();
                b.ToTable("Core_DataItemDetail");
            });

            modelBuilder.Entity<DataItemDetailEntity>()
            .HasOne(x => x.DataItem)
            .WithMany(x => x.DataItemDetailEntitys)
            .HasForeignKey(x => x.ItemId);


            #endregion


            #region Site
            modelBuilder.Entity<SiteSettings>(entity =>
           {
               entity.ToTable("Core_SiteSettings");

               entity.HasKey(p => p.Id);
               ;

               entity.Property(p => p.AliasId)
               .HasMaxLength(36)
               ;

                //entity.Property(u => u.ConcurrencyStamp)
                //    .IsConcurrencyToken()
                //    .HasMaxLength(50)
                //    ;

                entity.HasIndex(p => p.AliasId);

               entity.Property(p => p.SiteName)
               .HasMaxLength(255)
               .IsRequired();

               entity.Property(p => p.Theme)
               .HasMaxLength(100);

               entity.Property(p => p.AllowNewRegistration)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(true)
               ;

               entity.Property(p => p.RequireConfirmedEmail)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.RequireConfirmedPhone)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.IsServerAdminSite)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.UseLdapAuth)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.AutoCreateLdapUserOnFirstLogin)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(true)
               ;

               entity.Property(p => p.LdapServer)
               .HasMaxLength(255);
               ;

               entity.Property(p => p.LdapPort)
               //.IsRequired()
               //.ForSqlServerHasColumnType("int")
               // .HasDefaultValue(389)
               //.ValueGeneratedNever()
               ;

               entity.Property(p => p.LdapDomain)
               .HasMaxLength(255);
               ;

               entity.Property(p => p.LdapRootDN)
               .HasMaxLength(255);
               ;

               entity.Property(p => p.LdapUserDNKey)
               .HasMaxLength(10);
               ;

               entity.Property(p => p.ReallyDeleteUsers)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(true)
               ;

               entity.Property(p => p.UseEmailForLogin)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(true)
               ;

               entity.Property(p => p.RequiresQuestionAndAnswer)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.MaxInvalidPasswordAttempts)
               //.IsRequired()
               //.ForSqlServerHasColumnType("int")
               //.ValueGeneratedNever()
               // .HasDefaultValue(5)
               ;


               entity.Property(p => p.MinRequiredPasswordLength)
               // .IsRequired()
               // .ForSqlServerHasColumnType("int")
               //.HasDefaultValue(5)
               //.ValueGeneratedNever()
               ;

               entity.Property(p => p.DefaultEmailFromAddress)
               .HasMaxLength(100);
               ;

               entity.Property(p => p.DefaultEmailFromAlias)
               .HasMaxLength(100);
               ;

               entity.Property(p => p.RecaptchaPrivateKey)
               .HasMaxLength(255);
               ;

               entity.Property(p => p.RecaptchaPublicKey)
               .HasMaxLength(255);
               ;

               entity.Property(p => p.DisableDbAuth)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.RequireApprovalBeforeLogin)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.AllowDbFallbackWithLdap)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.EmailLdapDbFallback)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.AllowPersistentLogin)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.CaptchaOnLogin)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.CaptchaOnRegistration)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.SiteIsClosed)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

                //not mapped should map to nvarchar(max) by default I think
                // SiteIsClosedMessage 
                // PrivacyPolicy

                entity.Property(p => p.TimeZoneId)
               .HasMaxLength(50);
               ;

               entity.Property(p => p.GoogleAnalyticsProfileId)
               .HasMaxLength(25);
               ;

               entity.Property(p => p.CompanyName)
               .HasMaxLength(250);
               ;

               entity.Property(p => p.CompanyStreetAddress)
               .HasMaxLength(250);
               ;

               entity.Property(p => p.CompanyStreetAddress2)
               .HasMaxLength(250);
               ;

               entity.Property(p => p.CompanyRegion)
               .HasMaxLength(200);
               ;

               entity.Property(p => p.CompanyLocality)
               .HasMaxLength(200);
               ;

               entity.Property(p => p.CompanyCountry)
               .HasMaxLength(10);
               ;

               entity.Property(p => p.CompanyPostalCode)
               .HasMaxLength(20);
               ;

               entity.Property(p => p.CompanyPublicEmail)
               .HasMaxLength(100);
               ;

               entity.Property(p => p.CompanyPhone)
               .HasMaxLength(20);
               ;

               entity.Property(p => p.CompanyFax)
               .HasMaxLength(20);
               ;

               entity.Property(p => p.FacebookAppId)
               .HasMaxLength(100);
               ;

               entity.Property(p => p.FacebookAppSecret)
               ;

               entity.Property(p => p.GoogleClientId)
               .HasMaxLength(100);
               ;

               entity.Property(p => p.GoogleClientSecret)
               ;

               entity.Property(p => p.TwitterConsumerKey)
               .HasMaxLength(100);
               ;

               entity.Property(p => p.TwitterConsumerSecret)
               ;

               entity.Property(p => p.MicrosoftClientId)
               .HasMaxLength(100);
               ;

               entity.Property(p => p.MicrosoftClientSecret)
               ;

               entity.Property(p => p.OidConnectAppId)
              .HasMaxLength(255);
               ;

               entity.Property(p => p.PreferredHostName)
               .HasMaxLength(250);
               ;

               entity.Property(p => p.SiteFolderName)
               .HasMaxLength(50)
               .HasDefaultValue(string.Empty)
               ;

               entity.HasIndex(p => p.SiteFolderName);

               entity.Property(p => p.AddThisDotComUsername)
               .HasMaxLength(50);
               ;

                //not mapped should map to ntext by default I think
                // LoginInfoTop 
                // LoginInfoBottom
                // RegistrationAgreement
                // RegistrationPreamble

                entity.Property(p => p.SmtpServer)
               .HasMaxLength(200);
               ;

               entity.Property(p => p.SmtpPort)
               //.IsRequired()
               //.ForSqlServerHasColumnType("int")
               //.HasDefaultValue(25)
               //.ValueGeneratedNever()
               ;

               entity.Property(p => p.SmtpUser)
               .HasMaxLength(500); // large so it can be encrypted
                ;

               entity.Property(p => p.SmtpPassword)
               ;

               entity.Property(p => p.SmtpPreferredEncoding)
               .HasMaxLength(20);
               ;

               entity.Property(p => p.SmtpRequiresAuth)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.SmtpUseSsl)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.DkimDomain)
               .HasMaxLength(255);
               ;

               entity.Property(p => p.DkimSelector)
               .HasMaxLength(128);
               ;

               entity.Property(p => p.SignEmailWithDkim)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.SmsClientId)
               .HasMaxLength(255);
               ;

               entity.Property(p => p.SmsFrom)
               .HasMaxLength(100);
               ;

               entity.Property(p => p.IsDataProtected)
               .IsRequired()
               .ForSqlServerHasColumnType("bit")
               .ForSqlServerHasDefaultValue(false)
               ;

               entity.Property(p => p.CreatedUtc)
               .ForSqlServerHasColumnType("datetime")
               .ForSqlServerHasDefaultValueSql("getutcdate()")
               ;
           });

            modelBuilder.Entity<SiteHost>(entity =>
            {
                entity.ToTable("Core_SiteHost");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.SiteId)
                .IsRequired()
                ;
                entity.HasIndex(p => p.SiteId);

                entity.Property(p => p.HostName)
                .IsRequired()
                .HasMaxLength(255);
                ;

                entity.HasIndex(p => p.HostName);
            });

            #endregion


            // enable auto history
            modelBuilder.EnableAutoHistory();

        }
    }
}
