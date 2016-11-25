using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SF.WebHost.Migrations
{
    public partial class ModifyAutoHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SourceId",
                table: "Core_AutoHistory",
                maxLength: 50,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SourceId",
                table: "Core_AutoHistory",
                maxLength: 100,
                nullable: false);
        }
    }
}
