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
INSERT [dbo].[Core_User] ([Id], [AccessFailedCount], [AccountState], [ConcurrencyStamp], [CreatedBy], [CreatedDate], [CurrentShippingAddressId], [Email], [EmailConfirmed], [FullName], [IsAdministrator], [IsDeleted], [LockoutEnabled], [LockoutEnd], [ModifiedBy], [ModifiedDate], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserGuid], [UserName], [UserType]) VALUES (1, 0, NULL, N'8620916f-e6b6-4f12-9041-83737154b338', NULL, CAST(0x07105622F6C65F3B0B0000 AS DateTimeOffset), NULL, N'admin@SimpleFramework.com', 0, N'Shop Admin', 1, 0, 1, NULL, NULL, CAST(0x07105622F6C65F3B0B0000 AS DateTimeOffset), N'ADMIN@SimpleFramework.COM', N'ADMIN@SimpleFramework.COM', N'AQAAAAEAACcQAAAAEAEqSCV8Bpg69irmeg8N86U503jGEAYf75fBuzvL00/mr/FGEsiUqfR0rWBbBUwqtw==', NULL, 0, N'9e87ce89-64c0-45b9-8b52-6e0eaa79e5b7', 0, N'1fff10ce-0231-43a2-8b7d-c8db18504f65', N'admin@SimpleFramework.com', NULL)
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
