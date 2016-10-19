using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SimpleFramework.WebHost.Migrations
{
    public partial class ModifyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Core_AddressEntity_Core_CountryEntity_CountryId",
                table: "Core_AddressEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_AddressEntity_Core_DistrictEntity_DistrictId",
                table: "Core_AddressEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_AddressEntity_Core_StateOrProvinceEntity_StateOrProvinceId",
                table: "Core_AddressEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_DistrictEntity_Core_StateOrProvinceEntity_StateOrProvinceId",
                table: "Core_DistrictEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_SettingValueEntity_Core_SettingValue_SettingId",
                table: "Core_SettingValueEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_StateOrProvinceEntity_Core_CountryEntity_CountryId",
                table: "Core_StateOrProvinceEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_UrlSlugEntity_Core_EntityType_EntityTypeId",
                table: "Core_UrlSlugEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_UserAddressEntity_Core_AddressEntity_AddressId",
                table: "Core_UserAddressEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_UserAddressEntity_Core_User_UserId",
                table: "Core_UserAddressEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_User_Core_UserAddressEntity_CurrentShippingAddressId",
                table: "Core_User");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_WidgetInstanceEntity_Core_WidgetEntity_WidgetId",
                table: "Core_WidgetInstanceEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_WidgetInstanceEntity_Core_WidgetZoneEntity_WidgetZoneId",
                table: "Core_WidgetInstanceEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_InstalledPlugin",
                table: "Core_InstalledPlugin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_WidgetZoneEntity",
                table: "Core_WidgetZoneEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_WidgetInstanceEntity",
                table: "Core_WidgetInstanceEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_WidgetEntity",
                table: "Core_WidgetEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_UserAddressEntity",
                table: "Core_UserAddressEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_UrlSlugEntity",
                table: "Core_UrlSlugEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_StateOrProvinceEntity",
                table: "Core_StateOrProvinceEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_SettingValueEntity",
                table: "Core_SettingValueEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_NotificationTemplateEntity",
                table: "Core_NotificationTemplateEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_NotificationEntity",
                table: "Core_NotificationEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_MediaEntity",
                table: "Core_MediaEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_EntityType",
                table: "Core_EntityType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_DistrictEntity",
                table: "Core_DistrictEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_CountryEntity",
                table: "Core_CountryEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_AddressEntity",
                table: "Core_AddressEntity");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "Core_SettingValue");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Core_Permission");

            migrationBuilder.DropTable(
                name: "Core_RoleAssignment");

            migrationBuilder.DropTable(
                name: "ActivityLog_Activity");

            migrationBuilder.DropTable(
                name: "Localization_Resource");

            migrationBuilder.DropTable(
                name: "ActivityLog_ActivityType");

            migrationBuilder.DropTable(
                name: "Localization_Culture");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_InstalledPlugin",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plugins_InstalledPlugin",
                table: "Core_InstalledPlugin",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_WidgetZoneEntity",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entitys_WidgetZoneEntity",
                table: "Core_WidgetZoneEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_WidgetInstanceEntity",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entitys_WidgetInstanceEntity",
                table: "Core_WidgetInstanceEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_WidgetEntity",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entitys_WidgetEntity",
                table: "Core_WidgetEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_User",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_UserAddressEntity",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entitys_UserAddressEntity",
                table: "Core_UserAddressEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_UrlSlugEntity",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entitys_UrlSlugEntity",
                table: "Core_UrlSlugEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_StateOrProvinceEntity",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entitys_StateOrProvinceEntity",
                table: "Core_StateOrProvinceEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_SettingValueEntity",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entitys_SettingValueEntity",
                table: "Core_SettingValueEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_SettingValue",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_RolePermission",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_Role",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_PermissionScope",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_Permission",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_NotificationTemplateEntity",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entitys_NotificationTemplateEntity",
                table: "Core_NotificationTemplateEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_NotificationEntity",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entitys_NotificationEntity",
                table: "Core_NotificationEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_MediaEntity",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entitys_MediaEntity",
                table: "Core_MediaEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_EntityType",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entitys_EntityType",
                table: "Core_EntityType",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_DistrictEntity",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entitys_DistrictEntity",
                table: "Core_DistrictEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_CountryEntity",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entitys_CountryEntity",
                table: "Core_CountryEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_ApiAccount",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_AddressEntity",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entitys_AddressEntity",
                table: "Core_AddressEntity",
                column: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Core_UserClaim",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Core_RoleClaim",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Core_AutoHistory",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Entitys_AddressEntity_Entitys_CountryEntity_CountryId",
                table: "Core_AddressEntity",
                column: "CountryId",
                principalTable: "Core_CountryEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entitys_AddressEntity_Entitys_DistrictEntity_DistrictId",
                table: "Core_AddressEntity",
                column: "DistrictId",
                principalTable: "Core_DistrictEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entitys_AddressEntity_Entitys_StateOrProvinceEntity_StateOrProvinceId",
                table: "Core_AddressEntity",
                column: "StateOrProvinceId",
                principalTable: "Core_StateOrProvinceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entitys_DistrictEntity_Entitys_StateOrProvinceEntity_StateOrProvinceId",
                table: "Core_DistrictEntity",
                column: "StateOrProvinceId",
                principalTable: "Core_StateOrProvinceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entitys_SettingValueEntity_Core_SettingValue_SettingId",
                table: "Core_SettingValueEntity",
                column: "SettingId",
                principalTable: "Core_SettingValue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entitys_StateOrProvinceEntity_Entitys_CountryEntity_CountryId",
                table: "Core_StateOrProvinceEntity",
                column: "CountryId",
                principalTable: "Core_CountryEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entitys_UrlSlugEntity_Entitys_EntityType_EntityTypeId",
                table: "Core_UrlSlugEntity",
                column: "EntityTypeId",
                principalTable: "Core_EntityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entitys_UserAddressEntity_Entitys_AddressEntity_AddressId",
                table: "Core_UserAddressEntity",
                column: "AddressId",
                principalTable: "Core_AddressEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entitys_UserAddressEntity_Core_User_UserId",
                table: "Core_UserAddressEntity",
                column: "UserId",
                principalTable: "Core_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_User_Entitys_UserAddressEntity_CurrentShippingAddressId",
                table: "Core_User",
                column: "CurrentShippingAddressId",
                principalTable: "Core_UserAddressEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entitys_WidgetInstanceEntity_Entitys_WidgetEntity_WidgetId",
                table: "Core_WidgetInstanceEntity",
                column: "WidgetId",
                principalTable: "Core_WidgetEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entitys_WidgetInstanceEntity_Entitys_WidgetZoneEntity_WidgetZoneId",
                table: "Core_WidgetInstanceEntity",
                column: "WidgetZoneId",
                principalTable: "Core_WidgetZoneEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_Core_WidgetInstanceEntity_WidgetZoneId",
                table: "Core_WidgetInstanceEntity",
                newName: "IX_Entitys_WidgetInstanceEntity_WidgetZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_Core_WidgetInstanceEntity_WidgetId",
                table: "Core_WidgetInstanceEntity",
                newName: "IX_Entitys_WidgetInstanceEntity_WidgetId");

            migrationBuilder.RenameIndex(
                name: "IX_Core_UserAddressEntity_UserId",
                table: "Core_UserAddressEntity",
                newName: "IX_Entitys_UserAddressEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Core_UserAddressEntity_AddressId",
                table: "Core_UserAddressEntity",
                newName: "IX_Entitys_UserAddressEntity_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Core_UrlSlugEntity_EntityTypeId",
                table: "Core_UrlSlugEntity",
                newName: "IX_Entitys_UrlSlugEntity_EntityTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Core_StateOrProvinceEntity_CountryId",
                table: "Core_StateOrProvinceEntity",
                newName: "IX_Entitys_StateOrProvinceEntity_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Core_SettingValueEntity_SettingId",
                table: "Core_SettingValueEntity",
                newName: "IX_Entitys_SettingValueEntity_SettingId");

            migrationBuilder.RenameIndex(
                name: "IX_Core_DistrictEntity_StateOrProvinceId",
                table: "Core_DistrictEntity",
                newName: "IX_Entitys_DistrictEntity_StateOrProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Core_AddressEntity_StateOrProvinceId",
                table: "Core_AddressEntity",
                newName: "IX_Entitys_AddressEntity_StateOrProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Core_AddressEntity_DistrictId",
                table: "Core_AddressEntity",
                newName: "IX_Entitys_AddressEntity_DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Core_AddressEntity_CountryId",
                table: "Core_AddressEntity",
                newName: "IX_Entitys_AddressEntity_CountryId");

            migrationBuilder.RenameTable(
                name: "Core_InstalledPlugin",
                newName: "Plugins_InstalledPlugin");

            migrationBuilder.RenameTable(
                name: "Core_WidgetZoneEntity",
                newName: "Entitys_WidgetZoneEntity");

            migrationBuilder.RenameTable(
                name: "Core_WidgetInstanceEntity",
                newName: "Entitys_WidgetInstanceEntity");

            migrationBuilder.RenameTable(
                name: "Core_WidgetEntity",
                newName: "Entitys_WidgetEntity");

            migrationBuilder.RenameTable(
                name: "Core_UserAddressEntity",
                newName: "Entitys_UserAddressEntity");

            migrationBuilder.RenameTable(
                name: "Core_UrlSlugEntity",
                newName: "Entitys_UrlSlugEntity");

            migrationBuilder.RenameTable(
                name: "Core_StateOrProvinceEntity",
                newName: "Entitys_StateOrProvinceEntity");

            migrationBuilder.RenameTable(
                name: "Core_SettingValueEntity",
                newName: "Entitys_SettingValueEntity");

            migrationBuilder.RenameTable(
                name: "Core_NotificationTemplateEntity",
                newName: "Entitys_NotificationTemplateEntity");

            migrationBuilder.RenameTable(
                name: "Core_NotificationEntity",
                newName: "Entitys_NotificationEntity");

            migrationBuilder.RenameTable(
                name: "Core_MediaEntity",
                newName: "Entitys_MediaEntity");

            migrationBuilder.RenameTable(
                name: "Core_EntityType",
                newName: "Entitys_EntityType");

            migrationBuilder.RenameTable(
                name: "Core_DistrictEntity",
                newName: "Entitys_DistrictEntity");

            migrationBuilder.RenameTable(
                name: "Core_CountryEntity",
                newName: "Entitys_CountryEntity");

            migrationBuilder.RenameTable(
                name: "Core_AddressEntity",
                newName: "Entitys_AddressEntity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entitys_AddressEntity_Entitys_CountryEntity_CountryId",
                table: "Entitys_AddressEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entitys_AddressEntity_Entitys_DistrictEntity_DistrictId",
                table: "Entitys_AddressEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entitys_AddressEntity_Entitys_StateOrProvinceEntity_StateOrProvinceId",
                table: "Entitys_AddressEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entitys_DistrictEntity_Entitys_StateOrProvinceEntity_StateOrProvinceId",
                table: "Entitys_DistrictEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entitys_SettingValueEntity_Core_SettingValue_SettingId",
                table: "Entitys_SettingValueEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entitys_StateOrProvinceEntity_Entitys_CountryEntity_CountryId",
                table: "Entitys_StateOrProvinceEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entitys_UrlSlugEntity_Entitys_EntityType_EntityTypeId",
                table: "Entitys_UrlSlugEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entitys_UserAddressEntity_Entitys_AddressEntity_AddressId",
                table: "Entitys_UserAddressEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entitys_UserAddressEntity_Core_User_UserId",
                table: "Entitys_UserAddressEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_User_Entitys_UserAddressEntity_CurrentShippingAddressId",
                table: "Core_User");

            migrationBuilder.DropForeignKey(
                name: "FK_Entitys_WidgetInstanceEntity_Entitys_WidgetEntity_WidgetId",
                table: "Entitys_WidgetInstanceEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Entitys_WidgetInstanceEntity_Entitys_WidgetZoneEntity_WidgetZoneId",
                table: "Entitys_WidgetInstanceEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plugins_InstalledPlugin",
                table: "Plugins_InstalledPlugin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entitys_WidgetZoneEntity",
                table: "Entitys_WidgetZoneEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entitys_WidgetInstanceEntity",
                table: "Entitys_WidgetInstanceEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entitys_WidgetEntity",
                table: "Entitys_WidgetEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entitys_UserAddressEntity",
                table: "Entitys_UserAddressEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entitys_UrlSlugEntity",
                table: "Entitys_UrlSlugEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entitys_StateOrProvinceEntity",
                table: "Entitys_StateOrProvinceEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entitys_SettingValueEntity",
                table: "Entitys_SettingValueEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entitys_NotificationTemplateEntity",
                table: "Entitys_NotificationTemplateEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entitys_NotificationEntity",
                table: "Entitys_NotificationEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entitys_MediaEntity",
                table: "Entitys_MediaEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entitys_EntityType",
                table: "Entitys_EntityType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entitys_DistrictEntity",
                table: "Entitys_DistrictEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entitys_CountryEntity",
                table: "Entitys_CountryEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entitys_AddressEntity",
                table: "Entitys_AddressEntity");

            migrationBuilder.CreateTable(
                name: "Core_RoleAssignment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 64, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 64, nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    RoleId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Core_RoleAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Core_RoleAssignment_Core_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Core_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Core_RoleAssignment_Core_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Core_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLog_ActivityType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog_ActivityType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localization_Culture",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localization_Culture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLog_Activity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    ActivityTypeId = table.Column<long>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    EntityId = table.Column<long>(nullable: false),
                    EntityTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog_Activity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLog_Activity_ActivityLog_ActivityType_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityLog_ActivityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localization_Resource",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CultureId = table.Column<long>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localization_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localization_Resource_Localization_Culture_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Localization_Culture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<string>(
                name: "ObjectId",
                table: "Core_SettingValue",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Core_Permission",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Plugins_InstalledPlugin",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_InstalledPlugin",
                table: "Plugins_InstalledPlugin",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Entitys_WidgetZoneEntity",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_WidgetZoneEntity",
                table: "Entitys_WidgetZoneEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Entitys_WidgetInstanceEntity",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_WidgetInstanceEntity",
                table: "Entitys_WidgetInstanceEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Entitys_WidgetEntity",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_WidgetEntity",
                table: "Entitys_WidgetEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_User",
                nullable: false);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Entitys_UserAddressEntity",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_UserAddressEntity",
                table: "Entitys_UserAddressEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Entitys_UrlSlugEntity",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_UrlSlugEntity",
                table: "Entitys_UrlSlugEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Entitys_StateOrProvinceEntity",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_StateOrProvinceEntity",
                table: "Entitys_StateOrProvinceEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Entitys_SettingValueEntity",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_SettingValueEntity",
                table: "Entitys_SettingValueEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_SettingValue",
                nullable: false);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_RolePermission",
                nullable: false);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_Role",
                nullable: false);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_PermissionScope",
                nullable: false);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_Permission",
                nullable: false);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Entitys_NotificationTemplateEntity",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_NotificationTemplateEntity",
                table: "Entitys_NotificationTemplateEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Entitys_NotificationEntity",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_NotificationEntity",
                table: "Entitys_NotificationEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Entitys_MediaEntity",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_MediaEntity",
                table: "Entitys_MediaEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Entitys_EntityType",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_EntityType",
                table: "Entitys_EntityType",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Entitys_DistrictEntity",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_DistrictEntity",
                table: "Entitys_DistrictEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Entitys_CountryEntity",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_CountryEntity",
                table: "Entitys_CountryEntity",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Core_ApiAccount",
                nullable: false);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Entitys_AddressEntity",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_AddressEntity",
                table: "Entitys_AddressEntity",
                column: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Core_UserClaim",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Core_RoleClaim",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Core_AutoHistory",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Core_RoleAssignment_RoleId",
                table: "Core_RoleAssignment",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_RoleAssignment_UserId",
                table: "Core_RoleAssignment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLog_Activity_ActivityTypeId",
                table: "ActivityLog_Activity",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Localization_Resource_CultureId",
                table: "Localization_Resource",
                column: "CultureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Core_AddressEntity_Core_CountryEntity_CountryId",
                table: "Entitys_AddressEntity",
                column: "CountryId",
                principalTable: "Entitys_CountryEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_AddressEntity_Core_DistrictEntity_DistrictId",
                table: "Entitys_AddressEntity",
                column: "DistrictId",
                principalTable: "Entitys_DistrictEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_AddressEntity_Core_StateOrProvinceEntity_StateOrProvinceId",
                table: "Entitys_AddressEntity",
                column: "StateOrProvinceId",
                principalTable: "Entitys_StateOrProvinceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_DistrictEntity_Core_StateOrProvinceEntity_StateOrProvinceId",
                table: "Entitys_DistrictEntity",
                column: "StateOrProvinceId",
                principalTable: "Entitys_StateOrProvinceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_SettingValueEntity_Core_SettingValue_SettingId",
                table: "Entitys_SettingValueEntity",
                column: "SettingId",
                principalTable: "Core_SettingValue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_StateOrProvinceEntity_Core_CountryEntity_CountryId",
                table: "Entitys_StateOrProvinceEntity",
                column: "CountryId",
                principalTable: "Entitys_CountryEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_UrlSlugEntity_Core_EntityType_EntityTypeId",
                table: "Entitys_UrlSlugEntity",
                column: "EntityTypeId",
                principalTable: "Entitys_EntityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_UserAddressEntity_Core_AddressEntity_AddressId",
                table: "Entitys_UserAddressEntity",
                column: "AddressId",
                principalTable: "Entitys_AddressEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_UserAddressEntity_Core_User_UserId",
                table: "Entitys_UserAddressEntity",
                column: "UserId",
                principalTable: "Core_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_User_Core_UserAddressEntity_CurrentShippingAddressId",
                table: "Core_User",
                column: "CurrentShippingAddressId",
                principalTable: "Entitys_UserAddressEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_WidgetInstanceEntity_Core_WidgetEntity_WidgetId",
                table: "Entitys_WidgetInstanceEntity",
                column: "WidgetId",
                principalTable: "Entitys_WidgetEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_WidgetInstanceEntity_Core_WidgetZoneEntity_WidgetZoneId",
                table: "Entitys_WidgetInstanceEntity",
                column: "WidgetZoneId",
                principalTable: "Entitys_WidgetZoneEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_Entitys_WidgetInstanceEntity_WidgetZoneId",
                table: "Entitys_WidgetInstanceEntity",
                newName: "IX_Core_WidgetInstanceEntity_WidgetZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_Entitys_WidgetInstanceEntity_WidgetId",
                table: "Entitys_WidgetInstanceEntity",
                newName: "IX_Core_WidgetInstanceEntity_WidgetId");

            migrationBuilder.RenameIndex(
                name: "IX_Entitys_UserAddressEntity_UserId",
                table: "Entitys_UserAddressEntity",
                newName: "IX_Core_UserAddressEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Entitys_UserAddressEntity_AddressId",
                table: "Entitys_UserAddressEntity",
                newName: "IX_Core_UserAddressEntity_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Entitys_UrlSlugEntity_EntityTypeId",
                table: "Entitys_UrlSlugEntity",
                newName: "IX_Core_UrlSlugEntity_EntityTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Entitys_StateOrProvinceEntity_CountryId",
                table: "Entitys_StateOrProvinceEntity",
                newName: "IX_Core_StateOrProvinceEntity_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Entitys_SettingValueEntity_SettingId",
                table: "Entitys_SettingValueEntity",
                newName: "IX_Core_SettingValueEntity_SettingId");

            migrationBuilder.RenameIndex(
                name: "IX_Entitys_DistrictEntity_StateOrProvinceId",
                table: "Entitys_DistrictEntity",
                newName: "IX_Core_DistrictEntity_StateOrProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Entitys_AddressEntity_StateOrProvinceId",
                table: "Entitys_AddressEntity",
                newName: "IX_Core_AddressEntity_StateOrProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Entitys_AddressEntity_DistrictId",
                table: "Entitys_AddressEntity",
                newName: "IX_Core_AddressEntity_DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Entitys_AddressEntity_CountryId",
                table: "Entitys_AddressEntity",
                newName: "IX_Core_AddressEntity_CountryId");

            migrationBuilder.RenameTable(
                name: "Plugins_InstalledPlugin",
                newName: "Core_InstalledPlugin");

            migrationBuilder.RenameTable(
                name: "Entitys_WidgetZoneEntity",
                newName: "Core_WidgetZoneEntity");

            migrationBuilder.RenameTable(
                name: "Entitys_WidgetInstanceEntity",
                newName: "Core_WidgetInstanceEntity");

            migrationBuilder.RenameTable(
                name: "Entitys_WidgetEntity",
                newName: "Core_WidgetEntity");

            migrationBuilder.RenameTable(
                name: "Entitys_UserAddressEntity",
                newName: "Core_UserAddressEntity");

            migrationBuilder.RenameTable(
                name: "Entitys_UrlSlugEntity",
                newName: "Core_UrlSlugEntity");

            migrationBuilder.RenameTable(
                name: "Entitys_StateOrProvinceEntity",
                newName: "Core_StateOrProvinceEntity");

            migrationBuilder.RenameTable(
                name: "Entitys_SettingValueEntity",
                newName: "Core_SettingValueEntity");

            migrationBuilder.RenameTable(
                name: "Entitys_NotificationTemplateEntity",
                newName: "Core_NotificationTemplateEntity");

            migrationBuilder.RenameTable(
                name: "Entitys_NotificationEntity",
                newName: "Core_NotificationEntity");

            migrationBuilder.RenameTable(
                name: "Entitys_MediaEntity",
                newName: "Core_MediaEntity");

            migrationBuilder.RenameTable(
                name: "Entitys_EntityType",
                newName: "Core_EntityType");

            migrationBuilder.RenameTable(
                name: "Entitys_DistrictEntity",
                newName: "Core_DistrictEntity");

            migrationBuilder.RenameTable(
                name: "Entitys_CountryEntity",
                newName: "Core_CountryEntity");

            migrationBuilder.RenameTable(
                name: "Entitys_AddressEntity",
                newName: "Core_AddressEntity");
        }
    }
}
