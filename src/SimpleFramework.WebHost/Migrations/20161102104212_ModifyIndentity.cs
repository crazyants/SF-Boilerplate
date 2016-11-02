using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleFramework.WebHost.Migrations
{
    public partial class ModifyIndentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Core_RoleClaim_Core_Role_RoleId",
                table: "Core_RoleClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_UserClaim_Core_User_UserId",
                table: "Core_UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_Core_UserLogin_Core_User_UserId",
                table: "Core_UserLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_UserToken",
                table: "Core_UserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_UserLogin",
                table: "Core_UserLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_UserClaim",
                table: "Core_UserClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Core_RoleClaim",
                table: "Core_RoleClaim");

            migrationBuilder.DropTable(
                name: "Core_UserRole");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Core_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Core_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Core_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Core_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AlterColumn<string>(
                name: "UserType",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountState",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "Core_UserToken",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "Core_UserLogin",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "Core_UserClaim",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "Core_RoleClaim",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_Core_Role_RoleId",
                table: "Core_RoleClaim",
                column: "RoleId",
                principalTable: "Core_Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_Core_User_UserId",
                table: "Core_UserClaim",
                column: "UserId",
                principalTable: "Core_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Core_User_UserId",
                table: "Core_UserLogin",
                column: "UserId",
                principalTable: "Core_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_Core_UserLogin_UserId",
                table: "Core_UserLogin",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Core_UserClaim_UserId",
                table: "Core_UserClaim",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Core_RoleClaim_RoleId",
                table: "Core_RoleClaim",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.RenameTable(
                name: "Core_UserToken",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "Core_UserLogin",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "Core_UserClaim",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "Core_RoleClaim",
                newName: "AspNetRoleClaims");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_Core_Role_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_Core_User_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Core_User_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

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

            migrationBuilder.AlterColumn<string>(
                name: "UserType",
                table: "Core_User",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountState",
                table: "Core_User",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_UserToken",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_UserLogin",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_UserClaim",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Core_RoleClaim",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Core_UserRole_RoleId",
                table: "Core_UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Core_UserRole_UserId",
                table: "Core_UserRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Core_RoleClaim_Core_Role_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "Core_Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_UserClaim_Core_User_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "Core_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Core_UserLogin_Core_User_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "Core_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_Core_UserLogin_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_Core_UserClaim_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_Core_RoleClaim_RoleId");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "Core_UserToken");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "Core_UserLogin");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "Core_UserClaim");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "Core_RoleClaim");
        }
    }
}
