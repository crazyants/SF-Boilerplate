USE [SFramework_Base]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 2016/11/8 15:42:26 ******/
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
ALTER TABLE [dbo].[Event] ADD  DEFAULT (getutcdate()) FOR [InsertedDate]
GO

GO
SET IDENTITY_INSERT [dbo].[Core_Role] ON 

GO
INSERT [dbo].[Core_Role] ([Id], [ConcurrencyStamp], [Description], [Name], [NormalizedName]) VALUES (1, N'bd3bee0b-5f1d-482d-b890-ffdc01915da3', NULL, N'admin', N'ADMIN')
GO
INSERT [dbo].[Core_Role] ([Id], [ConcurrencyStamp], [Description], [Name], [NormalizedName]) VALUES (2, N'bd3bee0b-5f1d-482d-b890-ffdc01915da3', NULL, N'customer', N'CUSTOMER')
GO
INSERT [dbo].[Core_Role] ([Id], [ConcurrencyStamp], [Description], [Name], [NormalizedName]) VALUES (3, N'bd3bee0b-5f1d-482d-b890-ffdc01915da3', NULL, N'guest', N'GUEST')
GO
SET IDENTITY_INSERT [dbo].[Core_Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Core_User] ON 

GO
INSERT [dbo].[Core_User] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [FullName], [IsDeleted], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserGuid], [UserName],[CreatedOn],[CreatedBy],[UpdatedOn],[UpdatedBy],[IsAdministrator]) VALUES (1, 0, N'adfeea54-cee5-4a6f-9c0f-a8fc47883971', N'admin@admin.com', 0, N'admin', 0, 0, NULL, N'ADMIN@ADMIN.COM', N'ADMIN@ADMIN.COM', N'AQAAAAEAACcQAAAAEIt+m8p/mFcVlh8N7V8hoNaBEQuejYWsrZnK45oB4syrCkYv3SDr7l1JbsCxJoC0tA==', NULL, 0, N'e74db142-844b-4369-b9d1-c867ebb05198', 0, N'00000000-0000-0000-0000-000000000000', N'admin@admin.com',getdate(),'Admin',getdate(),'Admin',0)
GO
SET IDENTITY_INSERT [dbo].[Core_User] OFF
GO
INSERT [dbo].[Core_UserRole] ([UserId], [RoleId]) VALUES (1, 1)
GO

SET IDENTITY_INSERT [dbo].[Plugins_InstalledPlugin] ON 

GO
INSERT [dbo].[Plugins_InstalledPlugin] ([Id], [Active], [DateActivated], [DateDeactivated], [DateInstalled], [Installed], [PluginAssemblyName], [PluginName], [PluginVersion],[Sortindex]) VALUES (1, 1, CAST(0x070000000000000000 AS DateTime2), CAST(0x070000000000000000 AS DateTime2), CAST(0x0729AE988793F43B0B AS DateTime2), 1, N'SimpleFramework.Plugin.Blogs', N'ModBlog', N'1.0',1)
GO
SET IDENTITY_INSERT [dbo].[Plugins_InstalledPlugin] OFF
GO
