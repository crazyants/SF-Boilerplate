using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleFramework.WebHost.Migrations
{
    public partial class ModifyIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entitys_UserAddressEntity_Core_User_UserId",
                table: "Entitys_UserAddressEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_User_Entitys_UserAddressEntity_CurrentShippingAddressId",
                table: "Core_User");

            migrationBuilder.DropIndex(
                name: "IX_Core_User_CurrentShippingAddressId",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "AccountState",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "CurrentShippingAddressId",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "IsAdministrator",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Core_User");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Core_User");

            migrationBuilder.AddForeignKey(
                name: "FK_Entitys_UserAddressEntity_Core_User_UserId",
                table: "Entitys_UserAddressEntity",
                column: "UserId",
                principalTable: "Core_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entitys_UserAddressEntity_Core_User_UserId",
                table: "Entitys_UserAddressEntity");

            migrationBuilder.AddColumn<string>(
                name: "AccountState",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "Core_User",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<long>(
                name: "CurrentShippingAddressId",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdministrator",
                table: "Core_User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedDate",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Core_User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Core_User_CurrentShippingAddressId",
                table: "Core_User",
                column: "CurrentShippingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entitys_UserAddressEntity_Core_User_UserId",
                table: "Entitys_UserAddressEntity",
                column: "UserId",
                principalTable: "Core_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_User_Entitys_UserAddressEntity_CurrentShippingAddressId",
                table: "Core_User",
                column: "CurrentShippingAddressId",
                principalTable: "Entitys_UserAddressEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
