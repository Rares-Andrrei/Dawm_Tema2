﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class migratie2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
