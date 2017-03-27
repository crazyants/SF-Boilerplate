using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;
using System.IO;

namespace SF.WebHost.Migrations
{
    public partial class Modify_Multi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Core_UserToken",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_UserToken", x => x.UserId);
                    table.UniqueConstraint("AK_Core_UserToken_UserId_LoginProvider_Name", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Core_AutoHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AfterJson = table.Column<string>(nullable: true),
                    BeforeJson = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Kind = table.Column<int>(nullable: false),
                    SourceId = table.Column<string>(maxLength: 50, nullable: false),
                    TypeName = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_AutoHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Core_InstalledPlugin",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DateActivated = table.Column<DateTime>(nullable: false),
                    DateDeactivated = table.Column<DateTime>(nullable: false),
                    DateInstalled = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Installed = table.Column<bool>(nullable: false),
                    PluginAssemblyName = table.Column<string>(nullable: true),
                    PluginName = table.Column<string>(nullable: true),
                    PluginVersion = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_InstalledPlugin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entitys_CountryEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entitys_CountryEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entitys_EntityType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    RoutingAction = table.Column<string>(nullable: true),
                    RoutingController = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entitys_EntityType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entitys_MediaEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Caption = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FileSize = table.Column<int>(nullable: false),
                    MediaType = table.Column<int>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entitys_MediaEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Core_Module",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowDelete = table.Column<int>(nullable: true),
                    AllowEdit = table.Column<int>(nullable: true),
                    AllowExpand = table.Column<int>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeleteMark = table.Column<int>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Description = table.Column<string>(maxLength: 280, nullable: true),
                    EnCode = table.Column<string>(maxLength: 127, nullable: true),
                    EnabledMark = table.Column<int>(nullable: true),
                    FullName = table.Column<string>(maxLength: 127, nullable: true),
                    Icon = table.Column<string>(maxLength: 127, nullable: true),
                    IsMenu = table.Column<int>(nullable: true),
                    IsPublic = table.Column<int>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    Target = table.Column<string>(maxLength: 127, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UrlAddress = table.Column<string>(maxLength: 127, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entitys_NotificationEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttemptCount = table.Column<int>(nullable: false),
                    Body = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsSuccessSend = table.Column<bool>(nullable: false),
                    Language = table.Column<string>(maxLength: 10, nullable: true),
                    LastFailAttemptDate = table.Column<DateTime>(nullable: true),
                    LastFailAttemptMessage = table.Column<string>(nullable: true),
                    MaxAttemptCount = table.Column<int>(nullable: false),
                    ObjectId = table.Column<string>(maxLength: 128, nullable: true),
                    ObjectTypeId = table.Column<string>(maxLength: 128, nullable: true),
                    Recipient = table.Column<string>(maxLength: 128, nullable: true),
                    Sender = table.Column<string>(maxLength: 128, nullable: true),
                    SendingGateway = table.Column<string>(maxLength: 128, nullable: true),
                    SentDate = table.Column<DateTime>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    StartSendingDate = table.Column<DateTime>(nullable: true),
                    Subject = table.Column<string>(maxLength: 512, nullable: true),
                    Type = table.Column<string>(maxLength: 128, nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entitys_NotificationEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entitys_NotificationTemplateEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    Language = table.Column<string>(maxLength: 10, nullable: true),
                    NotificationTypeId = table.Column<string>(maxLength: 128, nullable: true),
                    ObjectId = table.Column<string>(maxLength: 128, nullable: true),
                    ObjectTypeId = table.Column<string>(maxLength: 128, nullable: true),
                    Recipient = table.Column<string>(nullable: true),
                    Sender = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    TemplateEngine = table.Column<string>(maxLength: 64, nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entitys_NotificationTemplateEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Core_Permission",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActionAddress = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ModuleId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Core_Role",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Enabled = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    SiteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Core_SettingValue",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Description = table.Column<string>(maxLength: 1024, nullable: true),
                    IsEnum = table.Column<bool>(nullable: false),
                    IsLocaleDependant = table.Column<bool>(nullable: false),
                    IsMultiValue = table.Column<bool>(nullable: false),
                    IsSystem = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    ObjectType = table.Column<string>(maxLength: 128, nullable: true),
                    SettingValueType = table.Column<string>(maxLength: 64, nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_SettingValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Core_SiteHost",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    HostName = table.Column<string>(maxLength: 255, nullable: false),
                    SiteId = table.Column<long>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_SiteHost", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Core_SiteSettings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountApprovalEmailCsv = table.Column<string>(nullable: true),
                    AddThisDotComUsername = table.Column<string>(maxLength: 50, nullable: true),
                    AliasId = table.Column<string>(maxLength: 36, nullable: true),
                    AllowDbFallbackWithLdap = table.Column<bool>(nullable: false),
                    AllowNewRegistration = table.Column<bool>(nullable: false),
                    AllowPersistentLogin = table.Column<bool>(nullable: false),
                    AutoCreateLdapUserOnFirstLogin = table.Column<bool>(nullable: false),
                    CaptchaOnLogin = table.Column<bool>(nullable: false),
                    CaptchaOnRegistration = table.Column<bool>(nullable: false),
                    CompanyCountry = table.Column<string>(maxLength: 10, nullable: true),
                    CompanyFax = table.Column<string>(maxLength: 20, nullable: true),
                    CompanyLocality = table.Column<string>(maxLength: 200, nullable: true),
                    CompanyName = table.Column<string>(maxLength: 250, nullable: true),
                    CompanyPhone = table.Column<string>(maxLength: 20, nullable: true),
                    CompanyPostalCode = table.Column<string>(maxLength: 20, nullable: true),
                    CompanyPublicEmail = table.Column<string>(maxLength: 100, nullable: true),
                    CompanyRegion = table.Column<string>(maxLength: 200, nullable: true),
                    CompanyStreetAddress = table.Column<string>(maxLength: 250, nullable: true),
                    CompanyStreetAddress2 = table.Column<string>(maxLength: 250, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DefaultEmailFromAddress = table.Column<string>(maxLength: 100, nullable: true),
                    DefaultEmailFromAlias = table.Column<string>(maxLength: 100, nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    DisableDbAuth = table.Column<bool>(nullable: false),
                    DkimDomain = table.Column<string>(maxLength: 255, nullable: true),
                    DkimPrivateKey = table.Column<string>(nullable: true),
                    DkimPublicKey = table.Column<string>(nullable: true),
                    DkimSelector = table.Column<string>(maxLength: 128, nullable: true),
                    EmailLdapDbFallback = table.Column<bool>(nullable: false),
                    EnabledAuthorization = table.Column<bool>(nullable: false),
                    FacebookAppId = table.Column<string>(maxLength: 100, nullable: true),
                    FacebookAppSecret = table.Column<string>(nullable: true),
                    GoogleAnalyticsProfileId = table.Column<string>(maxLength: 25, nullable: true),
                    GoogleClientId = table.Column<string>(maxLength: 100, nullable: true),
                    GoogleClientSecret = table.Column<string>(nullable: true),
                    IsDataProtected = table.Column<bool>(nullable: false),
                    IsServerAdminSite = table.Column<bool>(nullable: false),
                    LdapDomain = table.Column<string>(maxLength: 255, nullable: true),
                    LdapPort = table.Column<int>(nullable: false),
                    LdapRootDN = table.Column<string>(maxLength: 255, nullable: true),
                    LdapServer = table.Column<string>(maxLength: 255, nullable: true),
                    LdapUserDNKey = table.Column<string>(maxLength: 10, nullable: true),
                    LoginInfoBottom = table.Column<string>(nullable: true),
                    LoginInfoTop = table.Column<string>(nullable: true),
                    MaxInvalidPasswordAttempts = table.Column<int>(nullable: false),
                    MicrosoftClientId = table.Column<string>(maxLength: 100, nullable: true),
                    MicrosoftClientSecret = table.Column<string>(nullable: true),
                    MinRequiredPasswordLength = table.Column<int>(nullable: false),
                    OidConnectAppId = table.Column<string>(maxLength: 255, nullable: true),
                    OidConnectAppSecret = table.Column<string>(nullable: true),
                    PreferredHostName = table.Column<string>(maxLength: 250, nullable: true),
                    PrivacyPolicy = table.Column<string>(nullable: true),
                    ReallyDeleteUsers = table.Column<bool>(nullable: false),
                    RecaptchaPrivateKey = table.Column<string>(maxLength: 255, nullable: true),
                    RecaptchaPublicKey = table.Column<string>(maxLength: 255, nullable: true),
                    RegistrationAgreement = table.Column<string>(nullable: true),
                    RegistrationPreamble = table.Column<string>(nullable: true),
                    RequireApprovalBeforeLogin = table.Column<bool>(nullable: false),
                    RequireConfirmedEmail = table.Column<bool>(nullable: false),
                    RequireConfirmedPhone = table.Column<bool>(nullable: false),
                    RequiresQuestionAndAnswer = table.Column<bool>(nullable: false),
                    SignEmailWithDkim = table.Column<bool>(nullable: false),
                    SiteFolderName = table.Column<string>(maxLength: 50, nullable: true, defaultValue: ""),
                    SiteIsClosed = table.Column<bool>(nullable: false),
                    SiteIsClosedMessage = table.Column<string>(nullable: true),
                    SiteName = table.Column<string>(maxLength: 255, nullable: false),
                    SmsClientId = table.Column<string>(maxLength: 255, nullable: true),
                    SmsFrom = table.Column<string>(maxLength: 100, nullable: true),
                    SmsSecureToken = table.Column<string>(nullable: true),
                    SmtpPassword = table.Column<string>(nullable: true),
                    SmtpPort = table.Column<int>(nullable: false),
                    SmtpPreferredEncoding = table.Column<string>(maxLength: 20, nullable: true),
                    SmtpRequiresAuth = table.Column<bool>(nullable: false),
                    SmtpServer = table.Column<string>(maxLength: 200, nullable: true),
                    SmtpUseSsl = table.Column<bool>(nullable: false),
                    SmtpUser = table.Column<string>(maxLength: 500, nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    Theme = table.Column<string>(maxLength: 100, nullable: true),
                    TimeZoneId = table.Column<string>(maxLength: 50, nullable: true),
                    TwitterConsumerKey = table.Column<string>(maxLength: 100, nullable: true),
                    TwitterConsumerSecret = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UseEmailForLogin = table.Column<bool>(nullable: false),
                    UseLdapAuth = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_SiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Core_UserLocation",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CaptureCount = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 255, nullable: true),
                    Continent = table.Column<string>(maxLength: 255, nullable: true),
                    Country = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    FirstCaptureUtc = table.Column<DateTime>(nullable: false),
                    HostName = table.Column<string>(maxLength: 255, nullable: true),
                    IpAddress = table.Column<string>(maxLength: 50, nullable: true),
                    IpAddressLong = table.Column<long>(nullable: false),
                    Isp = table.Column<string>(maxLength: 255, nullable: true),
                    LastCaptureUtc = table.Column<DateTime>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Region = table.Column<string>(maxLength: 255, nullable: true),
                    SiteId = table.Column<long>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    TimeZone = table.Column<string>(maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_UserLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Core_ActivityType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_ActivityType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Backend_AreaEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AreaCode = table.Column<string>(maxLength: 127, nullable: true),
                    AreaName = table.Column<string>(maxLength: 127, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeleteMark = table.Column<int>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Description = table.Column<string>(maxLength: 280, nullable: true),
                    EnabledMark = table.Column<int>(nullable: true),
                    Layer = table.Column<int>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    QuickQuery = table.Column<string>(maxLength: 127, nullable: true),
                    SimpleSpelling = table.Column<string>(maxLength: 127, nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backend_AreaEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Core_DataItem",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeleteMark = table.Column<int>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    EnabledMark = table.Column<int>(nullable: true),
                    IsNav = table.Column<int>(nullable: true),
                    IsTree = table.Column<int>(nullable: true),
                    ItemCode = table.Column<string>(maxLength: 1000, nullable: true),
                    ItemName = table.Column<string>(maxLength: 1000, nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_DataItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Backend_DepartmentEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Description = table.Column<string>(maxLength: 280, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EnCode = table.Column<string>(maxLength: 127, nullable: true),
                    EnabledMark = table.Column<int>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(maxLength: 127, nullable: true),
                    InnerPhone = table.Column<string>(nullable: true),
                    Layer = table.Column<int>(nullable: true),
                    Manager = table.Column<string>(nullable: true),
                    ManagerId = table.Column<long>(nullable: true),
                    Nature = table.Column<string>(nullable: true),
                    OrganizeId = table.Column<long>(nullable: true),
                    OuterPhone = table.Column<string>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backend_DepartmentEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Backend_DMOSEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<int>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    DepartmentId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(maxLength: 280, nullable: true),
                    EnCode = table.Column<string>(maxLength: 127, nullable: true),
                    EnabledMark = table.Column<int>(nullable: true),
                    FullName = table.Column<string>(maxLength: 127, nullable: true),
                    OrganizeId = table.Column<long>(nullable: true),
                    OverdueTime = table.Column<DateTime>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backend_DMOSEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Backend_OrganizeEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    BusinessScope = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: true),
                    CityId = table.Column<long>(nullable: true),
                    CountyId = table.Column<long>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Description = table.Column<string>(maxLength: 280, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EnCode = table.Column<string>(maxLength: 127, nullable: true),
                    EnabledMark = table.Column<int>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    FoundedTime = table.Column<DateTime>(nullable: true),
                    FullName = table.Column<string>(maxLength: 127, nullable: true),
                    InnerPhone = table.Column<string>(nullable: true),
                    Layer = table.Column<int>(nullable: true),
                    Manager = table.Column<string>(nullable: true),
                    ManagerId = table.Column<string>(nullable: true),
                    Nature = table.Column<string>(nullable: true),
                    OuterPhone = table.Column<string>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    Postalcode = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<long>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    WebAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backend_OrganizeEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localization_CultureEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localization_CultureEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Code_SystemLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Culture = table.Column<string>(maxLength: 10, nullable: true),
                    EventId = table.Column<int>(nullable: false),
                    IpAddress = table.Column<string>(maxLength: 50, nullable: true),
                    LogDate = table.Column<DateTime>(nullable: false),
                    LogLevel = table.Column<string>(maxLength: 20, nullable: true),
                    Logger = table.Column<string>(maxLength: 255, nullable: true),
                    Message = table.Column<string>(nullable: true),
                    ShortUrl = table.Column<string>(maxLength: 255, nullable: true),
                    StateJson = table.Column<string>(nullable: true),
                    Thread = table.Column<string>(maxLength: 255, nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Code_SystemLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entitys_StateOrProvinceEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entitys_StateOrProvinceEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entitys_StateOrProvinceEntity_Entitys_CountryEntity_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Entitys_CountryEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entitys_UrlSlugEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    EntityId = table.Column<long>(nullable: false),
                    EntityTypeId = table.Column<long>(nullable: false),
                    Slug = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entitys_UrlSlugEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entitys_UrlSlugEntity_Entitys_EntityType_EntityTypeId",
                        column: x => x.EntityTypeId,
                        principalTable: "Entitys_EntityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Core_RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Core_RoleClaim_Core_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Core_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Core_RoleModule",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    ModuleId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_RoleModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Core_RoleModule_Core_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Core_Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_RoleModule_Core_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Core_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Core_RolePermission",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    PermissionId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_RolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Core_RolePermission_Core_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Core_Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Core_RolePermission_Core_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Core_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entitys_SettingValueEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BooleanValue = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DateTimeValue = table.Column<DateTime>(nullable: true),
                    DecimalValue = table.Column<decimal>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    IntegerValue = table.Column<int>(nullable: false),
                    Locale = table.Column<string>(maxLength: 64, nullable: true),
                    LongTextValue = table.Column<string>(nullable: true),
                    SettingId = table.Column<long>(nullable: false),
                    ShortTextValue = table.Column<string>(maxLength: 512, nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ValueType = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entitys_SettingValueEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entitys_SettingValueEntity_Core_SettingValue_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Core_SettingValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Core_Activity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivityTypeId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    EntityId = table.Column<long>(nullable: false),
                    EntityTypeId = table.Column<long>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_Activity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Core_Activity_Core_ActivityType_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "Core_ActivityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Core_DataItemDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeleteMark = table.Column<int>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    EnabledMark = table.Column<int>(nullable: true),
                    IsDefault = table.Column<int>(nullable: true),
                    ItemCode = table.Column<string>(nullable: true),
                    ItemId = table.Column<long>(nullable: false),
                    ItemName = table.Column<string>(nullable: true),
                    ItemValue = table.Column<string>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    QuickQuery = table.Column<string>(nullable: true),
                    SimpleSpelling = table.Column<string>(nullable: true),
                    SortCode = table.Column<int>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_DataItemDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Core_DataItemDetail_Core_DataItem_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Core_DataItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localization_ResourceEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CultureId = table.Column<long>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localization_ResourceEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localization_ResourceEntity_Localization_CultureEntity_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Localization_CultureEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entitys_DistrictEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    StateOrProvinceId = table.Column<long>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entitys_DistrictEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entitys_DistrictEntity_Entitys_StateOrProvinceEntity_StateOrProvinceId",
                        column: x => x.StateOrProvinceId,
                        principalTable: "Entitys_StateOrProvinceEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entitys_AddressEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    CountryId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    DistrictId = table.Column<long>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    StateOrProvinceId = table.Column<long>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entitys_AddressEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entitys_AddressEntity_Entitys_CountryEntity_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Entitys_CountryEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entitys_AddressEntity_Entitys_DistrictEntity_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Entitys_DistrictEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entitys_AddressEntity_Entitys_StateOrProvinceEntity_StateOrProvinceId",
                        column: x => x.StateOrProvinceId,
                        principalTable: "Entitys_StateOrProvinceEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entitys_UserAddressEntity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<long>(nullable: false),
                    AddressType = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    LastUsedOn = table.Column<DateTimeOffset>(nullable: true),
                    SortIndex = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entitys_UserAddressEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entitys_UserAddressEntity_Entitys_AddressEntity_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Entitys_AddressEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Core_User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AccountApproved = table.Column<bool>(nullable: false),
                    AccountState = table.Column<string>(maxLength: 128, nullable: true),
                    AllowEndTime = table.Column<DateTime>(nullable: true),
                    AllowStartTime = table.Column<DateTime>(nullable: true),
                    AnswerQuestion = table.Column<string>(nullable: true),
                    AvatarUrl = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    CheckOnLine = table.Column<int>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    CurrentShippingAddressId = table.Column<long>(nullable: true),
                    DeletedBy = table.Column<string>(maxLength: 128, nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    DepartmentId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    DutyId = table.Column<long>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstVisit = table.Column<DateTime>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    IsAdministrator = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsLockedOut = table.Column<bool>(nullable: false),
                    LastVisit = table.Column<DateTime>(nullable: true),
                    LockEndDate = table.Column<DateTime>(nullable: true),
                    LockStartDate = table.Column<DateTime>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LogOnCount = table.Column<int>(nullable: true),
                    MSN = table.Column<string>(nullable: true),
                    ManagerId = table.Column<long>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    OICQ = table.Column<string>(nullable: true),
                    OrganizeId = table.Column<long>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    PostId = table.Column<long>(nullable: true),
                    PreviousVisit = table.Column<DateTime>(nullable: true),
                    Question = table.Column<string>(nullable: true),
                    SecurityLevel = table.Column<int>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    SiteId = table.Column<long>(nullable: false),
                    TimeZoneId = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(nullable: false),
                    UserGuid = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    UserNo = table.Column<string>(nullable: true),
                    UserOnLine = table.Column<int>(nullable: true),
                    UserType = table.Column<string>(maxLength: 128, nullable: true),
                    WeChat = table.Column<string>(nullable: true),
                    WebSiteUrl = table.Column<string>(nullable: true),
                    WorkGroupId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Core_User_Entitys_UserAddressEntity_CurrentShippingAddressId",
                        column: x => x.CurrentShippingAddressId,
                        principalTable: "Entitys_UserAddressEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Core_UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Core_UserClaim_Core_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Core_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Core_UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_Core_UserLogin_Core_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Core_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Core_UserRole",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Core_UserRole_Core_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Core_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Core_UserRole_Core_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Core_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Core_RoleClaim_RoleId",
                table: "Core_RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_UserClaim_UserId",
                table: "Core_UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_UserLogin_UserId",
                table: "Core_UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Entitys_AddressEntity_CountryId",
                table: "Entitys_AddressEntity",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Entitys_AddressEntity_DistrictId",
                table: "Entitys_AddressEntity",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Entitys_AddressEntity_StateOrProvinceId",
                table: "Entitys_AddressEntity",
                column: "StateOrProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Entitys_DistrictEntity_StateOrProvinceId",
                table: "Entitys_DistrictEntity",
                column: "StateOrProvinceId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Core_Role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Core_RoleModule_ModuleId",
                table: "Core_RoleModule",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_RoleModule_RoleId",
                table: "Core_RoleModule",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_RolePermission_PermissionId",
                table: "Core_RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_RolePermission_RoleId",
                table: "Core_RolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Entitys_SettingValueEntity_SettingId",
                table: "Entitys_SettingValueEntity",
                column: "SettingId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_SiteHost_HostName",
                table: "Core_SiteHost",
                column: "HostName");

            migrationBuilder.CreateIndex(
                name: "IX_Core_SiteHost_SiteId",
                table: "Core_SiteHost",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_SiteSettings_AliasId",
                table: "Core_SiteSettings",
                column: "AliasId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_SiteSettings_SiteFolderName",
                table: "Core_SiteSettings",
                column: "SiteFolderName");

            migrationBuilder.CreateIndex(
                name: "IX_Entitys_StateOrProvinceEntity_CountryId",
                table: "Entitys_StateOrProvinceEntity",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Entitys_UrlSlugEntity_EntityTypeId",
                table: "Entitys_UrlSlugEntity",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Entitys_UserAddressEntity_AddressId",
                table: "Entitys_UserAddressEntity",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Entitys_UserAddressEntity_UserId",
                table: "Entitys_UserAddressEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_User_CurrentShippingAddressId",
                table: "Core_User",
                column: "CurrentShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Core_User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Core_User",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Core_UserLocation_IpAddress",
                table: "Core_UserLocation",
                column: "IpAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Core_UserLocation_UserId",
                table: "Core_UserLocation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_UserRole_RoleId",
                table: "Core_UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_Activity_ActivityTypeId",
                table: "Core_Activity",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_DataItemDetail_ItemId",
                table: "Core_DataItemDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Localization_ResourceEntity_CultureId",
                table: "Localization_ResourceEntity",
                column: "CultureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entitys_UserAddressEntity_Core_User_UserId",
                table: "Entitys_UserAddressEntity",
                column: "UserId",
                principalTable: "Core_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(File.ReadAllText("config/sql/area.sql"));
            migrationBuilder.Sql(File.ReadAllText("config/sql/base.sql"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entitys_UserAddressEntity_Core_User_UserId",
                table: "Entitys_UserAddressEntity");

            migrationBuilder.DropTable(
                name: "Core_RoleClaim");

            migrationBuilder.DropTable(
                name: "Core_UserClaim");

            migrationBuilder.DropTable(
                name: "Core_UserLogin");

            migrationBuilder.DropTable(
                name: "Core_UserToken");

            migrationBuilder.DropTable(
                name: "Core_AutoHistory");

            migrationBuilder.DropTable(
                name: "Core_InstalledPlugin");

            migrationBuilder.DropTable(
                name: "Entitys_MediaEntity");

            migrationBuilder.DropTable(
                name: "Entitys_NotificationEntity");

            migrationBuilder.DropTable(
                name: "Entitys_NotificationTemplateEntity");

            migrationBuilder.DropTable(
                name: "Core_RoleModule");

            migrationBuilder.DropTable(
                name: "Core_RolePermission");

            migrationBuilder.DropTable(
                name: "Entitys_SettingValueEntity");

            migrationBuilder.DropTable(
                name: "Core_SiteHost");

            migrationBuilder.DropTable(
                name: "Core_SiteSettings");

            migrationBuilder.DropTable(
                name: "Entitys_UrlSlugEntity");

            migrationBuilder.DropTable(
                name: "Core_UserLocation");

            migrationBuilder.DropTable(
                name: "Core_UserRole");

            migrationBuilder.DropTable(
                name: "Core_Activity");

            migrationBuilder.DropTable(
                name: "Backend_AreaEntity");

            migrationBuilder.DropTable(
                name: "Core_DataItemDetail");

            migrationBuilder.DropTable(
                name: "Backend_DepartmentEntity");

            migrationBuilder.DropTable(
                name: "Backend_DMOSEntity");

            migrationBuilder.DropTable(
                name: "Backend_OrganizeEntity");

            migrationBuilder.DropTable(
                name: "Localization_ResourceEntity");

            migrationBuilder.DropTable(
                name: "Code_SystemLog");

            migrationBuilder.DropTable(
                name: "Core_Module");

            migrationBuilder.DropTable(
                name: "Core_Permission");

            migrationBuilder.DropTable(
                name: "Core_SettingValue");

            migrationBuilder.DropTable(
                name: "Entitys_EntityType");

            migrationBuilder.DropTable(
                name: "Core_Role");

            migrationBuilder.DropTable(
                name: "Core_ActivityType");

            migrationBuilder.DropTable(
                name: "Core_DataItem");

            migrationBuilder.DropTable(
                name: "Localization_CultureEntity");

            migrationBuilder.DropTable(
                name: "Core_User");

            migrationBuilder.DropTable(
                name: "Entitys_UserAddressEntity");

            migrationBuilder.DropTable(
                name: "Entitys_AddressEntity");

            migrationBuilder.DropTable(
                name: "Entitys_DistrictEntity");

            migrationBuilder.DropTable(
                name: "Entitys_StateOrProvinceEntity");

            migrationBuilder.DropTable(
                name: "Entitys_CountryEntity");
        }
    }
}
