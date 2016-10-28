using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SimpleFramework.Core.Data;

namespace SimpleFramework.WebHost.Migrations
{
    [DbContext(typeof(CoreDbContext))]
    partial class CoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Core_RoleClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Core_UserClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<long>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("Core_UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("Core_UserToken");
                });

            modelBuilder.Entity("SimpleFramework.Core.AutoHistorys.Internal.AutoHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AfterJson")
                        .HasAnnotation("MaxLength", 2048);

                    b.Property<string>("BeforeJson")
                        .HasAnnotation("MaxLength", 2048);

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("Kind");

                    b.Property<string>("SourceId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.HasKey("Id");

                    b.ToTable("Core_AutoHistory");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.AddressEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("ContactName");

                    b.Property<long>("CountryId");

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<long>("DistrictId");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Phone");

                    b.Property<long>("StateOrProvinceId");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("StateOrProvinceId");

                    b.ToTable("Entitys_AddressEntity");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.ApiAccountEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AccountId");

                    b.Property<int>("ApiAccountType");

                    b.Property<string>("AppId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<bool>("IsActive");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("SecretKey");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Core_ApiAccount");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.CountryEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Entitys_CountryEntity");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.DistrictEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("Location");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<long>("StateOrProvinceId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("StateOrProvinceId");

                    b.ToTable("Entitys_DistrictEntity");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.EntityType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<string>("RoutingAction");

                    b.Property<string>("RoutingController");

                    b.HasKey("Id");

                    b.ToTable("Entitys_EntityType");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.MediaEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caption");

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("FileName");

                    b.Property<int>("FileSize");

                    b.Property<int>("MediaType");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.HasKey("Id");

                    b.ToTable("Entitys_MediaEntity");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.NotificationEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AttemptCount");

                    b.Property<string>("Body");

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsSuccessSend");

                    b.Property<string>("Language")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<DateTime?>("LastFailAttemptDate");

                    b.Property<string>("LastFailAttemptMessage");

                    b.Property<int>("MaxAttemptCount");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("ObjectId")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("ObjectTypeId")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("Recipient")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("Sender")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("SendingGateway")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTime?>("SentDate");

                    b.Property<DateTime?>("StartSendingDate");

                    b.Property<string>("Subject")
                        .HasAnnotation("MaxLength", 512);

                    b.Property<string>("Type")
                        .HasAnnotation("MaxLength", 128);

                    b.HasKey("Id");

                    b.ToTable("Entitys_NotificationEntity");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.NotificationTemplateEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Language")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("NotificationTypeId")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("ObjectId")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("ObjectTypeId")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("Recipient");

                    b.Property<string>("Sender");

                    b.Property<string>("Subject");

                    b.Property<string>("TemplateEngine")
                        .HasAnnotation("MaxLength", 64);

                    b.HasKey("Id");

                    b.ToTable("Entitys_NotificationTemplateEntity");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.PermissionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.ToTable("Core_Permission");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.PermissionScopeEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label")
                        .HasAnnotation("MaxLength", 1024);

                    b.Property<long>("RolePermissionId");

                    b.Property<string>("Scope")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 1024);

                    b.Property<string>("Type")
                        .HasAnnotation("MaxLength", 255);

                    b.HasKey("Id");

                    b.HasIndex("RolePermissionId");

                    b.ToTable("Core_PermissionScope");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.RoleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("Core_Role");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.RolePermissionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<long>("PermissionId");

                    b.Property<long>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("Core_RolePermission");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.SettingEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1024);

                    b.Property<bool>("IsEnum");

                    b.Property<bool>("IsLocaleDependant");

                    b.Property<bool>("IsMultiValue");

                    b.Property<bool>("IsSystem");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("ObjectType")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("SettingValueType")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.HasKey("Id");

                    b.ToTable("Core_SettingValue");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.SettingValueEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("BooleanValue");

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<DateTime?>("DateTimeValue");

                    b.Property<decimal>("DecimalValue");

                    b.Property<int>("IntegerValue");

                    b.Property<string>("Locale")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<string>("LongTextValue");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<long>("SettingId");

                    b.Property<string>("ShortTextValue")
                        .HasAnnotation("MaxLength", 512);

                    b.Property<string>("ValueType")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.HasKey("Id");

                    b.HasIndex("SettingId");

                    b.ToTable("Entitys_SettingValueEntity");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.StateOrProvinceEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CountryId");

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Entitys_StateOrProvinceEntity");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.UrlSlugEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<long>("EntityId");

                    b.Property<long>("EntityTypeId");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Slug");

                    b.HasKey("Id");

                    b.HasIndex("EntityTypeId");

                    b.ToTable("Entitys_UrlSlugEntity");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.UserAddressEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AddressId");

                    b.Property<int>("AddressType");

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<DateTimeOffset?>("LastUsedOn");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Entitys_UserAddressEntity");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AccountState")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<long?>("CurrentShippingAddressId");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsAdministrator");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<Guid>("UserGuid");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("UserType")
                        .HasAnnotation("MaxLength", 128);

                    b.HasKey("Id");

                    b.HasIndex("CurrentShippingAddressId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("Core_User");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.UserRoleEntity", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("Core_UserRole");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.WidgetEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("CreateUrl");

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<string>("EditUrl");

                    b.Property<bool>("IsPublished");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<string>("ViewComponentName");

                    b.HasKey("Id");

                    b.ToTable("Entitys_WidgetEntity");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.WidgetInstanceEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("Data");

                    b.Property<int>("DisplayOrder");

                    b.Property<string>("HtmlData");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<DateTimeOffset?>("PublishEnd");

                    b.Property<DateTimeOffset?>("PublishStart");

                    b.Property<long>("WidgetId");

                    b.Property<long>("WidgetZoneId");

                    b.HasKey("Id");

                    b.HasIndex("WidgetId");

                    b.HasIndex("WidgetZoneId");

                    b.ToTable("Entitys_WidgetInstanceEntity");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.WidgetZoneEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("ModifiedBy")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTimeOffset?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Entitys_WidgetZoneEntity");
                });

            modelBuilder.Entity("SimpleFramework.Core.Plugins.Models.InstalledPlugin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateActivated");

                    b.Property<DateTime>("DateDeactivated");

                    b.Property<DateTime>("DateInstalled");

                    b.Property<bool>("Installed");

                    b.Property<string>("PluginAssemblyName");

                    b.Property<string>("PluginName");

                    b.Property<string>("PluginVersion");

                    b.HasKey("Id");

                    b.ToTable("Plugins_InstalledPlugin");
                });

            modelBuilder.Entity("SimpleFramework.Module.ActivityLog.Models.Activity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ActivityTypeId");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<long>("EntityId");

                    b.Property<long>("EntityTypeId");

                    b.HasKey("Id");

                    b.HasIndex("ActivityTypeId");

                    b.ToTable("ActivityLog_Activity");
                });

            modelBuilder.Entity("SimpleFramework.Module.ActivityLog.Models.ActivityType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ActivityLog_ActivityType");
                });

            modelBuilder.Entity("SimpleFramework.Module.EFLogging.Models.LogItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ColumnType", "uniqueidentifier")
                        .HasAnnotation("SqlServer:DefaultValueSql", "newid()");

                    b.Property<string>("Culture")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<int>("EventId");

                    b.Property<string>("IpAddress")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("LogDateUtc")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("LogDate")
                        .HasAnnotation("SqlServer:ColumnType", "datetime")
                        .HasAnnotation("SqlServer:DefaultValueSql", "getutcdate()");

                    b.Property<string>("LogLevel")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Logger")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Message");

                    b.Property<string>("ShortUrl")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("StateJson");

                    b.Property<string>("Thread")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("cs_SystemLog");
                });

            modelBuilder.Entity("SimpleFramework.Module.Localization.Models.Culture", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Localization_Culture");
                });

            modelBuilder.Entity("SimpleFramework.Module.Localization.Models.Resource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CultureId");

                    b.Property<string>("Key");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CultureId");

                    b.ToTable("Localization_Resource");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.RoleEntity")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.UserEntity")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.UserEntity")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.AddressEntity", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.CountryEntity", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("SimpleFramework.Core.Entitys.DistrictEntity", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId");

                    b.HasOne("SimpleFramework.Core.Entitys.StateOrProvinceEntity", "StateOrProvince")
                        .WithMany()
                        .HasForeignKey("StateOrProvinceId");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.ApiAccountEntity", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.UserEntity", "Account")
                        .WithMany("ApiAccounts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.DistrictEntity", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.StateOrProvinceEntity", "StateOrProvince")
                        .WithMany()
                        .HasForeignKey("StateOrProvinceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.PermissionScopeEntity", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.RolePermissionEntity", "RolePermission")
                        .WithMany("Scopes")
                        .HasForeignKey("RolePermissionId");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.RolePermissionEntity", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.PermissionEntity", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId");

                    b.HasOne("SimpleFramework.Core.Entitys.RoleEntity", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.SettingValueEntity", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.SettingEntity", "Setting")
                        .WithMany("SettingValues")
                        .HasForeignKey("SettingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.StateOrProvinceEntity", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.CountryEntity", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.UrlSlugEntity", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.EntityType", "EntityType")
                        .WithMany()
                        .HasForeignKey("EntityTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.UserAddressEntity", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.AddressEntity", "Address")
                        .WithMany("UserAddresses")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleFramework.Core.Entitys.UserEntity", "User")
                        .WithMany("UserAddresses")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.UserEntity", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.UserAddressEntity", "CurrentShippingAddress")
                        .WithMany()
                        .HasForeignKey("CurrentShippingAddressId");
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.UserRoleEntity", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.RoleEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleFramework.Core.Entitys.UserEntity", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleFramework.Core.Entitys.WidgetInstanceEntity", b =>
                {
                    b.HasOne("SimpleFramework.Core.Entitys.WidgetEntity", "Widget")
                        .WithMany()
                        .HasForeignKey("WidgetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleFramework.Core.Entitys.WidgetZoneEntity", "WidgetZone")
                        .WithMany()
                        .HasForeignKey("WidgetZoneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleFramework.Module.ActivityLog.Models.Activity", b =>
                {
                    b.HasOne("SimpleFramework.Module.ActivityLog.Models.ActivityType", "ActivityType")
                        .WithMany()
                        .HasForeignKey("ActivityTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleFramework.Module.Localization.Models.Resource", b =>
                {
                    b.HasOne("SimpleFramework.Module.Localization.Models.Culture", "Culture")
                        .WithMany("Resources")
                        .HasForeignKey("CultureId");
                });
        }
    }
}
