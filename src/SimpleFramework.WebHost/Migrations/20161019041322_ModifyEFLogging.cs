using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SimpleFramework.WebHost.Migrations
{
    public partial class ModifyEFLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLog_ActivityType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog_ActivityType", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Localization_Culture",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLog_Activity_ActivityTypeId",
                table: "ActivityLog_Activity",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Localization_Resource_CultureId",
                table: "Localization_Resource",
                column: "CultureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLog_Activity");

            migrationBuilder.DropTable(
                name: "cs_SystemLog");

            migrationBuilder.DropTable(
                name: "Localization_Resource");

            migrationBuilder.DropTable(
                name: "ActivityLog_ActivityType");

            migrationBuilder.DropTable(
                name: "Localization_Culture");
        }
    }
}
