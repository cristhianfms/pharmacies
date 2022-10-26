using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class DrugExporterPermissionFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PermissionSet",
                keyColumn: "Id",
                keyValue: 13,
                column: "Endpoint",
                value: "GET/api/drug-exporters");

            migrationBuilder.UpdateData(
                table: "PermissionSet",
                keyColumn: "Id",
                keyValue: 14,
                column: "Endpoint",
                value: "POST/api/drug-exporters/export");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PermissionSet",
                keyColumn: "Id",
                keyValue: 13,
                column: "Endpoint",
                value: "GET/drug-exporters");

            migrationBuilder.UpdateData(
                table: "PermissionSet",
                keyColumn: "Id",
                keyValue: 14,
                column: "Endpoint",
                value: "POST/drug-exporters/export");
        }
    }
}
