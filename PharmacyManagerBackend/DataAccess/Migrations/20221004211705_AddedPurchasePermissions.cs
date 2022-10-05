﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddedPurchasePermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PermissionSet",
                columns: new[] { "Id", "Endpoint" },
                values: new object[] { 9, "GET/api/purchases" });

            migrationBuilder.InsertData(
                table: "PermissionRoleSet",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 9, 1 });

            migrationBuilder.InsertData(
                table: "PermissionRoleSet",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 9, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionRoleSet",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "PermissionRoleSet",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 9, 3 });

            migrationBuilder.DeleteData(
                table: "PermissionSet",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
