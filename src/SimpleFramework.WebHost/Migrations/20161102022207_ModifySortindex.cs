using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleFramework.WebHost.Migrations
{
    public partial class ModifySortindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cs_SystemLog");

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Localization_Resource",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Localization_Culture",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "ActivityLog_ActivityType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "ActivityLog_Activity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Plugins_InstalledPlugin",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Entitys_WidgetZoneEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Entitys_WidgetInstanceEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Entitys_WidgetEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Entitys_UserAddressEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Entitys_UrlSlugEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Entitys_StateOrProvinceEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Entitys_SettingValueEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Core_SettingValue",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Core_RolePermission",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Core_PermissionScope",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Core_Permission",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Entitys_NotificationTemplateEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Entitys_NotificationEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Entitys_MediaEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Entitys_EntityType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Entitys_DistrictEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Entitys_CountryEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Core_ApiAccount",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sortindex",
                table: "Entitys_AddressEntity",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Localization_Resource");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Localization_Culture");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "ActivityLog_ActivityType");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "ActivityLog_Activity");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Plugins_InstalledPlugin");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Entitys_WidgetZoneEntity");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Entitys_WidgetInstanceEntity");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Entitys_WidgetEntity");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Entitys_UserAddressEntity");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Entitys_UrlSlugEntity");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Entitys_StateOrProvinceEntity");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Entitys_SettingValueEntity");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Core_SettingValue");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Core_RolePermission");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Core_PermissionScope");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Core_Permission");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Entitys_NotificationTemplateEntity");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Entitys_NotificationEntity");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Entitys_MediaEntity");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Entitys_EntityType");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Entitys_DistrictEntity");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Entitys_CountryEntity");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Core_ApiAccount");

            migrationBuilder.DropColumn(
                name: "Sortindex",
                table: "Entitys_AddressEntity");

            migrationBuilder.CreateTable(
                name: "cs_SystemLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    Culture = table.Column<string>(maxLength: 10, nullable: true),
                    EventId = table.Column<int>(nullable: false),
                    IpAddress = table.Column<string>(maxLength: 50, nullable: true),
                    LogDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getutcdate()"),
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
                    table.PrimaryKey("PK_cs_SystemLog", x => x.Id);
                });
        }
    }
}
