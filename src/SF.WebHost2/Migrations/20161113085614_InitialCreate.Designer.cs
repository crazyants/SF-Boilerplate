using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SF.Core.Data;

namespace SF.WebHost.Migrations
{
    [DbContext(typeof(CoreDbContext))]
    [Migration("20161113085614_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("SF.Core.AutoHistorys.Internal.AutoHistory", b =>
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

            modelBuilder.Entity("SF.Core.Entitys.AddressEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("ContactName");

                    b.Property<long>("CountryId");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<long>("DistrictId");

                    b.Property<string>("Phone");

                    b.Property<int>("Sortindex");

                    b.Property<long>("StateOrProvinceId");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("StateOrProvinceId");

                    b.ToTable("Entitys_AddressEntity");
                });

            modelBuilder.Entity("SF.Core.Entitys.ApiAccountEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AccountId");

                    b.Property<int>("ApiAccountType");

                    b.Property<string>("AppId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("SecretKey");

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Core_ApiAccount");
                });

            modelBuilder.Entity("SF.Core.Entitys.CountryEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<string>("Name");

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Entitys_CountryEntity");
                });

            modelBuilder.Entity("SF.Core.Entitys.DataItemDetailEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<int?>("DeleteMark");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<int?>("EnabledMark");

                    b.Property<int?>("IsDefault");

                    b.Property<string>("ItemCode");

                    b.Property<long>("ItemId");

                    b.Property<string>("ItemName");

                    b.Property<string>("ItemValue");

                    b.Property<long?>("ParentId");

                    b.Property<string>("QuickQuery");

                    b.Property<string>("SimpleSpelling");

                    b.Property<int?>("SortCode");

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("Core_DataItemDetail");
                });

            modelBuilder.Entity("SF.Core.Entitys.DataItemEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<int?>("DeleteMark");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<int?>("EnabledMark");

                    b.Property<int?>("IsNav");

                    b.Property<int?>("IsTree");

                    b.Property<string>("ItemCode");

                    b.Property<string>("ItemName");

                    b.Property<long?>("ParentId");

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Core_DataItem");
                });

            modelBuilder.Entity("SF.Core.Entitys.DistrictEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<int>("Sortindex");

                    b.Property<long>("StateOrProvinceId");

                    b.Property<string>("Type");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("StateOrProvinceId");

                    b.ToTable("Entitys_DistrictEntity");
                });

            modelBuilder.Entity("SF.Core.Entitys.EntityType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<string>("Name");

                    b.Property<string>("RoutingAction");

                    b.Property<string>("RoutingController");

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Entitys_EntityType");
                });

            modelBuilder.Entity("SF.Core.Entitys.MediaEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caption");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<string>("FileName");

                    b.Property<int>("FileSize");

                    b.Property<int>("MediaType");

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Entitys_MediaEntity");
                });

            modelBuilder.Entity("SF.Core.Entitys.NotificationEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AttemptCount");

                    b.Property<string>("Body");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsSuccessSend");

                    b.Property<string>("Language")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<DateTime?>("LastFailAttemptDate");

                    b.Property<string>("LastFailAttemptMessage");

                    b.Property<int>("MaxAttemptCount");

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

                    b.Property<int>("Sortindex");

                    b.Property<DateTime?>("StartSendingDate");

                    b.Property<string>("Subject")
                        .HasAnnotation("MaxLength", 512);

                    b.Property<string>("Type")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Entitys_NotificationEntity");
                });

            modelBuilder.Entity("SF.Core.Entitys.NotificationTemplateEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Language")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("NotificationTypeId")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("ObjectId")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("ObjectTypeId")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("Recipient");

                    b.Property<string>("Sender");

                    b.Property<int>("Sortindex");

                    b.Property<string>("Subject");

                    b.Property<string>("TemplateEngine")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Entitys_NotificationTemplateEntity");
                });

            modelBuilder.Entity("SF.Core.Entitys.PermissionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Core_Permission");
                });

            modelBuilder.Entity("SF.Core.Entitys.PermissionScopeEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<string>("Label")
                        .HasAnnotation("MaxLength", 1024);

                    b.Property<long>("RolePermissionId");

                    b.Property<string>("Scope")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 1024);

                    b.Property<int>("Sortindex");

                    b.Property<string>("Type")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("RolePermissionId");

                    b.ToTable("Core_PermissionScope");
                });

            modelBuilder.Entity("SF.Core.Entitys.RoleEntity", b =>
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

            modelBuilder.Entity("SF.Core.Entitys.RolePermissionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<long>("PermissionId");

                    b.Property<long>("RoleId");

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("Core_RolePermission");
                });

            modelBuilder.Entity("SF.Core.Entitys.SettingEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1024);

                    b.Property<bool>("IsEnum");

                    b.Property<bool>("IsLocaleDependant");

                    b.Property<bool>("IsMultiValue");

                    b.Property<bool>("IsSystem");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("ObjectType")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("SettingValueType")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Core_SettingValue");
                });

            modelBuilder.Entity("SF.Core.Entitys.SettingValueEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("BooleanValue");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<DateTime?>("DateTimeValue");

                    b.Property<decimal>("DecimalValue");

                    b.Property<int>("IntegerValue");

                    b.Property<string>("Locale")
                        .HasAnnotation("MaxLength", 64);

                    b.Property<string>("LongTextValue");

                    b.Property<long>("SettingId");

                    b.Property<string>("ShortTextValue")
                        .HasAnnotation("MaxLength", 512);

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.Property<string>("ValueType")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.HasKey("Id");

                    b.HasIndex("SettingId");

                    b.ToTable("Entitys_SettingValueEntity");
                });

            modelBuilder.Entity("SF.Core.Entitys.StateOrProvinceEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CountryId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<string>("Name");

                    b.Property<int>("Sortindex");

                    b.Property<string>("Type");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Entitys_StateOrProvinceEntity");
                });

            modelBuilder.Entity("SF.Core.Entitys.UrlSlugEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<long>("EntityId");

                    b.Property<long>("EntityTypeId");

                    b.Property<string>("Slug");

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("EntityTypeId");

                    b.ToTable("Entitys_UrlSlugEntity");
                });

            modelBuilder.Entity("SF.Core.Entitys.UserAddressEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AddressId");

                    b.Property<int>("AddressType");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<DateTimeOffset?>("LastUsedOn");

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Entitys_UserAddressEntity");
                });

            modelBuilder.Entity("SF.Core.Entitys.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AccountState")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<long?>("CurrentShippingAddressId");

                    b.Property<string>("DeletedBy")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsAdministrator");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTimeOffset>("UpdatedOn");

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

            modelBuilder.Entity("SF.Core.Entitys.UserRoleEntity", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("Core_UserRole");
                });

            modelBuilder.Entity("SF.Module.ActivityLog.Models.ActivityEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ActivityTypeId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<long>("EntityId");

                    b.Property<long>("EntityTypeId");

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("ActivityTypeId");

                    b.ToTable("ActivityLog_ActivityEntity");
                });

            modelBuilder.Entity("SF.Module.ActivityLog.Models.ActivityType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<string>("Name");

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("ActivityLog_ActivityType");
                });

            modelBuilder.Entity("SF.Module.Localization.Models.CultureEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<string>("Name");

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Localization_CultureEntity");
                });

            modelBuilder.Entity("SF.Module.Localization.Models.ResourceEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<long?>("CultureId");

                    b.Property<string>("Key");

                    b.Property<int>("Sortindex");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CultureId");

                    b.ToTable("Localization_ResourceEntity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("SF.Core.Entitys.RoleEntity")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("SF.Core.Entitys.UserEntity")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("SF.Core.Entitys.UserEntity")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SF.Core.Entitys.AddressEntity", b =>
                {
                    b.HasOne("SF.Core.Entitys.CountryEntity", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("SF.Core.Entitys.DistrictEntity", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId");

                    b.HasOne("SF.Core.Entitys.StateOrProvinceEntity", "StateOrProvince")
                        .WithMany()
                        .HasForeignKey("StateOrProvinceId");
                });

            modelBuilder.Entity("SF.Core.Entitys.ApiAccountEntity", b =>
                {
                    b.HasOne("SF.Core.Entitys.UserEntity", "Account")
                        .WithMany("ApiAccounts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SF.Core.Entitys.DataItemDetailEntity", b =>
                {
                    b.HasOne("SF.Core.Entitys.DataItemEntity", "DataItem")
                        .WithMany("DataItemDetailEntitys")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SF.Core.Entitys.DistrictEntity", b =>
                {
                    b.HasOne("SF.Core.Entitys.StateOrProvinceEntity", "StateOrProvince")
                        .WithMany()
                        .HasForeignKey("StateOrProvinceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SF.Core.Entitys.PermissionScopeEntity", b =>
                {
                    b.HasOne("SF.Core.Entitys.RolePermissionEntity", "RolePermission")
                        .WithMany("Scopes")
                        .HasForeignKey("RolePermissionId");
                });

            modelBuilder.Entity("SF.Core.Entitys.RolePermissionEntity", b =>
                {
                    b.HasOne("SF.Core.Entitys.PermissionEntity", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId");

                    b.HasOne("SF.Core.Entitys.RoleEntity", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("SF.Core.Entitys.SettingValueEntity", b =>
                {
                    b.HasOne("SF.Core.Entitys.SettingEntity", "Setting")
                        .WithMany("SettingValues")
                        .HasForeignKey("SettingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SF.Core.Entitys.StateOrProvinceEntity", b =>
                {
                    b.HasOne("SF.Core.Entitys.CountryEntity", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SF.Core.Entitys.UrlSlugEntity", b =>
                {
                    b.HasOne("SF.Core.Entitys.EntityType", "EntityType")
                        .WithMany()
                        .HasForeignKey("EntityTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SF.Core.Entitys.UserAddressEntity", b =>
                {
                    b.HasOne("SF.Core.Entitys.AddressEntity", "Address")
                        .WithMany("UserAddresses")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SF.Core.Entitys.UserEntity", "User")
                        .WithMany("UserAddresses")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SF.Core.Entitys.UserEntity", b =>
                {
                    b.HasOne("SF.Core.Entitys.UserAddressEntity", "CurrentShippingAddress")
                        .WithMany()
                        .HasForeignKey("CurrentShippingAddressId");
                });

            modelBuilder.Entity("SF.Core.Entitys.UserRoleEntity", b =>
                {
                    b.HasOne("SF.Core.Entitys.RoleEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SF.Core.Entitys.UserEntity", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SF.Module.ActivityLog.Models.ActivityEntity", b =>
                {
                    b.HasOne("SF.Module.ActivityLog.Models.ActivityType", "ActivityType")
                        .WithMany()
                        .HasForeignKey("ActivityTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SF.Module.Localization.Models.ResourceEntity", b =>
                {
                    b.HasOne("SF.Module.Localization.Models.CultureEntity", "Culture")
                        .WithMany("Resources")
                        .HasForeignKey("CultureId");
                });
        }
    }
}
