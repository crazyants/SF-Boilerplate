USE [SF2]
GO
/****** Object:  Schema [HangFire]    Script Date: 2016/10/19 15:47:03 ******/
CREATE SCHEMA [HangFire]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ActivityLog_Activity]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityLog_Activity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ActivityTypeId] [bigint] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[EntityTypeId] [bigint] NOT NULL,
 CONSTRAINT [PK_ActivityLog_Activity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ActivityLog_ActivityType]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityLog_ActivityType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_ActivityLog_ActivityType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Core_ApiAccount]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Core_ApiAccount](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountId] [bigint] NOT NULL,
	[ApiAccountType] [int] NOT NULL,
	[AppId] [nvarchar](128) NOT NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[Name] [nvarchar](128) NULL,
	[SecretKey] [nvarchar](max) NULL,
 CONSTRAINT [PK_Core_ApiAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Core_AutoHistory]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Core_AutoHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AfterJson] [nvarchar](2048) NULL,
	[BeforeJson] [nvarchar](2048) NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[Kind] [int] NOT NULL,
	[SourceId] [nvarchar](50) NOT NULL,
	[TypeName] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_AutoHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Core_Permission]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Core_Permission](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Core_Permission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Core_PermissionScope]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Core_PermissionScope](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](1024) NULL,
	[RolePermissionId] [bigint] NOT NULL,
	[Scope] [nvarchar](1024) NOT NULL,
	[Type] [nvarchar](255) NULL,
 CONSTRAINT [PK_Core_PermissionScope] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Core_Role]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Core_Role](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_Core_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Core_RoleClaim]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Core_RoleClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK_Core_RoleClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Core_RolePermission]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Core_RolePermission](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[PermissionId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK_Core_RolePermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Core_SettingValue]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Core_SettingValue](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[IsEnum] [bit] NOT NULL,
	[IsLocaleDependant] [bit] NOT NULL,
	[IsMultiValue] [bit] NOT NULL,
	[IsSystem] [bit] NOT NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[Name] [nvarchar](128) NULL,
	[ObjectType] [nvarchar](128) NULL,
	[SettingValueType] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_Core_SettingValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Core_TagDistributedCache]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Core_TagDistributedCache](
	[Id] [nvarchar](449) NOT NULL,
	[Value] [varbinary](max) NOT NULL,
	[ExpiresAtTime] [datetimeoffset](7) NOT NULL,
	[SlidingExpirationInSeconds] [bigint] NULL,
	[AbsoluteExpiration] [datetimeoffset](7) NULL,
 CONSTRAINT [pk_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Core_User]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Core_User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[AccountState] [nvarchar](128) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CurrentShippingAddressId] [bigint] NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[IsAdministrator] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserGuid] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[UserType] [nvarchar](128) NULL,
 CONSTRAINT [PK_Core_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Core_UserClaim]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Core_UserClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_Core_UserClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Core_UserLogin]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Core_UserLogin](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_Core_UserLogin] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Core_UserRole]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Core_UserRole](
	[UserId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK_Core_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Core_UserToken]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Core_UserToken](
	[UserId] [bigint] NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_Core_UserToken] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cs_SystemLog]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cs_SystemLog](
	[Id] [uniqueidentifier] NOT NULL,
	[Culture] [nvarchar](10) NULL,
	[EventId] [int] NOT NULL,
	[IpAddress] [nvarchar](50) NULL,
	[LogDate] [datetime] NOT NULL,
	[LogLevel] [nvarchar](20) NULL,
	[Logger] [nvarchar](255) NULL,
	[Message] [nvarchar](max) NULL,
	[ShortUrl] [nvarchar](255) NULL,
	[StateJson] [nvarchar](max) NULL,
	[Thread] [nvarchar](255) NULL,
	[Url] [nvarchar](max) NULL,
 CONSTRAINT [PK_cs_SystemLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entitys_AddressEntity]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entitys_AddressEntity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressLine1] [nvarchar](max) NULL,
	[AddressLine2] [nvarchar](max) NULL,
	[ContactName] [nvarchar](max) NULL,
	[CountryId] [bigint] NOT NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[DistrictId] [bigint] NOT NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[Phone] [nvarchar](max) NULL,
	[StateOrProvinceId] [bigint] NOT NULL,
 CONSTRAINT [PK_Entitys_AddressEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entitys_CountryEntity]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entitys_CountryEntity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Entitys_CountryEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entitys_DistrictEntity]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entitys_DistrictEntity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[Location] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[Name] [nvarchar](max) NULL,
	[StateOrProvinceId] [bigint] NOT NULL,
	[Type] [nvarchar](max) NULL,
 CONSTRAINT [PK_Entitys_DistrictEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entitys_EntityType]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entitys_EntityType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[Name] [nvarchar](max) NULL,
	[RoutingAction] [nvarchar](max) NULL,
	[RoutingController] [nvarchar](max) NULL,
 CONSTRAINT [PK_Entitys_EntityType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entitys_MediaEntity]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entitys_MediaEntity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Caption] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[FileName] [nvarchar](max) NULL,
	[FileSize] [int] NOT NULL,
	[MediaType] [int] NOT NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Entitys_MediaEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entitys_NotificationEntity]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entitys_NotificationEntity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AttemptCount] [int] NOT NULL,
	[Body] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsSuccessSend] [bit] NOT NULL,
	[Language] [nvarchar](10) NULL,
	[LastFailAttemptDate] [datetime2](7) NULL,
	[LastFailAttemptMessage] [nvarchar](max) NULL,
	[MaxAttemptCount] [int] NOT NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[ObjectId] [nvarchar](128) NULL,
	[ObjectTypeId] [nvarchar](128) NULL,
	[Recipient] [nvarchar](128) NULL,
	[Sender] [nvarchar](128) NULL,
	[SendingGateway] [nvarchar](128) NULL,
	[SentDate] [datetime2](7) NULL,
	[StartSendingDate] [datetime2](7) NULL,
	[Subject] [nvarchar](512) NULL,
	[Type] [nvarchar](128) NULL,
 CONSTRAINT [PK_Entitys_NotificationEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entitys_NotificationTemplateEntity]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entitys_NotificationTemplateEntity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Body] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[Language] [nvarchar](10) NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[NotificationTypeId] [nvarchar](128) NULL,
	[ObjectId] [nvarchar](128) NULL,
	[ObjectTypeId] [nvarchar](128) NULL,
	[Recipient] [nvarchar](max) NULL,
	[Sender] [nvarchar](max) NULL,
	[Subject] [nvarchar](max) NULL,
	[TemplateEngine] [nvarchar](64) NULL,
 CONSTRAINT [PK_Entitys_NotificationTemplateEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entitys_SettingValueEntity]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entitys_SettingValueEntity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BooleanValue] [bit] NOT NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[DateTimeValue] [datetime2](7) NULL,
	[DecimalValue] [decimal](18, 2) NOT NULL,
	[IntegerValue] [int] NOT NULL,
	[Locale] [nvarchar](64) NULL,
	[LongTextValue] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[SettingId] [bigint] NOT NULL,
	[ShortTextValue] [nvarchar](512) NULL,
	[ValueType] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_Entitys_SettingValueEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entitys_StateOrProvinceEntity]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entitys_StateOrProvinceEntity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CountryId] [bigint] NOT NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[Name] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
 CONSTRAINT [PK_Entitys_StateOrProvinceEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entitys_UrlSlugEntity]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entitys_UrlSlugEntity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[EntityTypeId] [bigint] NOT NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[Slug] [nvarchar](max) NULL,
 CONSTRAINT [PK_Entitys_UrlSlugEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entitys_UserAddressEntity]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entitys_UserAddressEntity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[AddressType] [int] NOT NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[LastUsedOn] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_Entitys_UserAddressEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entitys_WidgetEntity]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entitys_WidgetEntity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[CreateUrl] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[EditUrl] [nvarchar](max) NULL,
	[IsPublished] [bit] NOT NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[Name] [nvarchar](max) NULL,
	[ViewComponentName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Entitys_WidgetEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entitys_WidgetInstanceEntity]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entitys_WidgetInstanceEntity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[Data] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
	[HtmlData] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[Name] [nvarchar](max) NULL,
	[PublishEnd] [datetimeoffset](7) NULL,
	[PublishStart] [datetimeoffset](7) NULL,
	[WidgetId] [bigint] NOT NULL,
	[WidgetZoneId] [bigint] NOT NULL,
 CONSTRAINT [PK_Entitys_WidgetInstanceEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Entitys_WidgetZoneEntity]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entitys_WidgetZoneEntity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [nvarchar](64) NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ModifiedBy] [nvarchar](64) NULL,
	[ModifiedDate] [datetimeoffset](7) NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Entitys_WidgetZoneEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Event]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[EventId] [bigint] IDENTITY(1,1) NOT NULL,
	[InsertedDate] [datetime] NOT NULL,
	[LastUpdatedDate] [datetime] NULL,
	[Data] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Localization_Culture]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Localization_Culture](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Localization_Culture] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Localization_Resource]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Localization_Resource](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CultureId] [bigint] NULL,
	[Key] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_Localization_Resource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Plugins_InstalledPlugin]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plugins_InstalledPlugin](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[DateActivated] [datetime2](7) NOT NULL,
	[DateDeactivated] [datetime2](7) NOT NULL,
	[DateInstalled] [datetime2](7) NOT NULL,
	[Installed] [bit] NOT NULL,
	[PluginAssemblyName] [nvarchar](max) NULL,
	[PluginName] [nvarchar](max) NULL,
	[PluginVersion] [nvarchar](max) NULL,
 CONSTRAINT [PK_Plugins_InstalledPlugin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [HangFire].[AggregatedCounter]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[AggregatedCounter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [bigint] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_CounterAggregated] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [HangFire].[Counter]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Counter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [smallint] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Counter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [HangFire].[Hash]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Hash](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Field] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime2](7) NULL,
 CONSTRAINT [PK_HangFire_Hash] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [HangFire].[Job]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Job](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateId] [int] NULL,
	[StateName] [nvarchar](20) NULL,
	[InvocationData] [nvarchar](max) NOT NULL,
	[Arguments] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [HangFire].[JobParameter]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobParameter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_JobParameter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [HangFire].[JobQueue]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobQueue](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NOT NULL,
	[Queue] [nvarchar](50) NOT NULL,
	[FetchedAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_JobQueue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [HangFire].[List]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[List](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_List] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [HangFire].[Schema]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Schema](
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_HangFire_Schema] PRIMARY KEY CLUSTERED 
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [HangFire].[Server]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Server](
	[Id] [nvarchar](100) NOT NULL,
	[Data] [nvarchar](max) NULL,
	[LastHeartbeat] [datetime] NOT NULL,
 CONSTRAINT [PK_HangFire_Server] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [HangFire].[Set]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Set](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Score] [float] NOT NULL,
	[Value] [nvarchar](256) NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Set] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [HangFire].[State]    Script Date: 2016/10/19 15:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[State](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Reason] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Data] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[cs_SystemLog] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[cs_SystemLog] ADD  DEFAULT (getutcdate()) FOR [LogDate]
GO
ALTER TABLE [dbo].[Event] ADD  DEFAULT (getutcdate()) FOR [InsertedDate]
GO
ALTER TABLE [dbo].[ActivityLog_Activity]  WITH CHECK ADD  CONSTRAINT [FK_ActivityLog_Activity_ActivityLog_ActivityType_ActivityTypeId] FOREIGN KEY([ActivityTypeId])
REFERENCES [dbo].[ActivityLog_ActivityType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ActivityLog_Activity] CHECK CONSTRAINT [FK_ActivityLog_Activity_ActivityLog_ActivityType_ActivityTypeId]
GO
ALTER TABLE [dbo].[Core_ApiAccount]  WITH CHECK ADD  CONSTRAINT [FK_Core_ApiAccount_Core_User_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Core_User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Core_ApiAccount] CHECK CONSTRAINT [FK_Core_ApiAccount_Core_User_AccountId]
GO
ALTER TABLE [dbo].[Core_PermissionScope]  WITH CHECK ADD  CONSTRAINT [FK_Core_PermissionScope_Core_RolePermission_RolePermissionId] FOREIGN KEY([RolePermissionId])
REFERENCES [dbo].[Core_RolePermission] ([Id])
GO
ALTER TABLE [dbo].[Core_PermissionScope] CHECK CONSTRAINT [FK_Core_PermissionScope_Core_RolePermission_RolePermissionId]
GO
ALTER TABLE [dbo].[Core_RoleClaim]  WITH CHECK ADD  CONSTRAINT [FK_Core_RoleClaim_Core_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Core_Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Core_RoleClaim] CHECK CONSTRAINT [FK_Core_RoleClaim_Core_Role_RoleId]
GO
ALTER TABLE [dbo].[Core_RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_Core_RolePermission_Core_Permission_PermissionId] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Core_Permission] ([Id])
GO
ALTER TABLE [dbo].[Core_RolePermission] CHECK CONSTRAINT [FK_Core_RolePermission_Core_Permission_PermissionId]
GO
ALTER TABLE [dbo].[Core_RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_Core_RolePermission_Core_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Core_Role] ([Id])
GO
ALTER TABLE [dbo].[Core_RolePermission] CHECK CONSTRAINT [FK_Core_RolePermission_Core_Role_RoleId]
GO
ALTER TABLE [dbo].[Core_User]  WITH CHECK ADD  CONSTRAINT [FK_Core_User_Entitys_UserAddressEntity_CurrentShippingAddressId] FOREIGN KEY([CurrentShippingAddressId])
REFERENCES [dbo].[Entitys_UserAddressEntity] ([Id])
GO
ALTER TABLE [dbo].[Core_User] CHECK CONSTRAINT [FK_Core_User_Entitys_UserAddressEntity_CurrentShippingAddressId]
GO
ALTER TABLE [dbo].[Core_UserClaim]  WITH CHECK ADD  CONSTRAINT [FK_Core_UserClaim_Core_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Core_User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Core_UserClaim] CHECK CONSTRAINT [FK_Core_UserClaim_Core_User_UserId]
GO
ALTER TABLE [dbo].[Core_UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_Core_UserLogin_Core_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Core_User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Core_UserLogin] CHECK CONSTRAINT [FK_Core_UserLogin_Core_User_UserId]
GO
ALTER TABLE [dbo].[Core_UserRole]  WITH CHECK ADD  CONSTRAINT [FK_Core_UserRole_Core_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Core_Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Core_UserRole] CHECK CONSTRAINT [FK_Core_UserRole_Core_Role_RoleId]
GO
ALTER TABLE [dbo].[Core_UserRole]  WITH CHECK ADD  CONSTRAINT [FK_Core_UserRole_Core_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Core_User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Core_UserRole] CHECK CONSTRAINT [FK_Core_UserRole_Core_User_UserId]
GO
ALTER TABLE [dbo].[Entitys_AddressEntity]  WITH CHECK ADD  CONSTRAINT [FK_Entitys_AddressEntity_Entitys_CountryEntity_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Entitys_CountryEntity] ([Id])
GO
ALTER TABLE [dbo].[Entitys_AddressEntity] CHECK CONSTRAINT [FK_Entitys_AddressEntity_Entitys_CountryEntity_CountryId]
GO
ALTER TABLE [dbo].[Entitys_AddressEntity]  WITH CHECK ADD  CONSTRAINT [FK_Entitys_AddressEntity_Entitys_DistrictEntity_DistrictId] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[Entitys_DistrictEntity] ([Id])
GO
ALTER TABLE [dbo].[Entitys_AddressEntity] CHECK CONSTRAINT [FK_Entitys_AddressEntity_Entitys_DistrictEntity_DistrictId]
GO
ALTER TABLE [dbo].[Entitys_AddressEntity]  WITH CHECK ADD  CONSTRAINT [FK_Entitys_AddressEntity_Entitys_StateOrProvinceEntity_StateOrProvinceId] FOREIGN KEY([StateOrProvinceId])
REFERENCES [dbo].[Entitys_StateOrProvinceEntity] ([Id])
GO
ALTER TABLE [dbo].[Entitys_AddressEntity] CHECK CONSTRAINT [FK_Entitys_AddressEntity_Entitys_StateOrProvinceEntity_StateOrProvinceId]
GO
ALTER TABLE [dbo].[Entitys_DistrictEntity]  WITH CHECK ADD  CONSTRAINT [FK_Entitys_DistrictEntity_Entitys_StateOrProvinceEntity_StateOrProvinceId] FOREIGN KEY([StateOrProvinceId])
REFERENCES [dbo].[Entitys_StateOrProvinceEntity] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Entitys_DistrictEntity] CHECK CONSTRAINT [FK_Entitys_DistrictEntity_Entitys_StateOrProvinceEntity_StateOrProvinceId]
GO
ALTER TABLE [dbo].[Entitys_SettingValueEntity]  WITH CHECK ADD  CONSTRAINT [FK_Entitys_SettingValueEntity_Core_SettingValue_SettingId] FOREIGN KEY([SettingId])
REFERENCES [dbo].[Core_SettingValue] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Entitys_SettingValueEntity] CHECK CONSTRAINT [FK_Entitys_SettingValueEntity_Core_SettingValue_SettingId]
GO
ALTER TABLE [dbo].[Entitys_StateOrProvinceEntity]  WITH CHECK ADD  CONSTRAINT [FK_Entitys_StateOrProvinceEntity_Entitys_CountryEntity_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Entitys_CountryEntity] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Entitys_StateOrProvinceEntity] CHECK CONSTRAINT [FK_Entitys_StateOrProvinceEntity_Entitys_CountryEntity_CountryId]
GO
ALTER TABLE [dbo].[Entitys_UrlSlugEntity]  WITH CHECK ADD  CONSTRAINT [FK_Entitys_UrlSlugEntity_Entitys_EntityType_EntityTypeId] FOREIGN KEY([EntityTypeId])
REFERENCES [dbo].[Entitys_EntityType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Entitys_UrlSlugEntity] CHECK CONSTRAINT [FK_Entitys_UrlSlugEntity_Entitys_EntityType_EntityTypeId]
GO
ALTER TABLE [dbo].[Entitys_UserAddressEntity]  WITH CHECK ADD  CONSTRAINT [FK_Entitys_UserAddressEntity_Core_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Core_User] ([Id])
GO
ALTER TABLE [dbo].[Entitys_UserAddressEntity] CHECK CONSTRAINT [FK_Entitys_UserAddressEntity_Core_User_UserId]
GO
ALTER TABLE [dbo].[Entitys_UserAddressEntity]  WITH CHECK ADD  CONSTRAINT [FK_Entitys_UserAddressEntity_Entitys_AddressEntity_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Entitys_AddressEntity] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Entitys_UserAddressEntity] CHECK CONSTRAINT [FK_Entitys_UserAddressEntity_Entitys_AddressEntity_AddressId]
GO
ALTER TABLE [dbo].[Entitys_WidgetInstanceEntity]  WITH CHECK ADD  CONSTRAINT [FK_Entitys_WidgetInstanceEntity_Entitys_WidgetEntity_WidgetId] FOREIGN KEY([WidgetId])
REFERENCES [dbo].[Entitys_WidgetEntity] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Entitys_WidgetInstanceEntity] CHECK CONSTRAINT [FK_Entitys_WidgetInstanceEntity_Entitys_WidgetEntity_WidgetId]
GO
ALTER TABLE [dbo].[Entitys_WidgetInstanceEntity]  WITH CHECK ADD  CONSTRAINT [FK_Entitys_WidgetInstanceEntity_Entitys_WidgetZoneEntity_WidgetZoneId] FOREIGN KEY([WidgetZoneId])
REFERENCES [dbo].[Entitys_WidgetZoneEntity] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Entitys_WidgetInstanceEntity] CHECK CONSTRAINT [FK_Entitys_WidgetInstanceEntity_Entitys_WidgetZoneEntity_WidgetZoneId]
GO
ALTER TABLE [dbo].[Localization_Resource]  WITH CHECK ADD  CONSTRAINT [FK_Localization_Resource_Localization_Culture_CultureId] FOREIGN KEY([CultureId])
REFERENCES [dbo].[Localization_Culture] ([Id])
GO
ALTER TABLE [dbo].[Localization_Resource] CHECK CONSTRAINT [FK_Localization_Resource_Localization_Culture_CultureId]
GO
ALTER TABLE [HangFire].[JobParameter]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_JobParameter_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[JobParameter] CHECK CONSTRAINT [FK_HangFire_JobParameter_Job]
GO
ALTER TABLE [HangFire].[State]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_State_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[State] CHECK CONSTRAINT [FK_HangFire_State_Job]
GO
