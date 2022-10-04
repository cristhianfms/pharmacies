using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AdminByDefaultUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PermissionSet",
                keyColumn: "Id",
                keyValue: 7,
                column: "Endpoint",
                value: "GET/api/drugs*");

            migrationBuilder.UpdateData(
                table: "UserSet",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Password" },
                values: new object[] { "dirección", "admin1234-" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PermissionSet",
                keyColumn: "Id",
                keyValue: 7,
                column: "Endpoint",
                value: "GET/api/drugs/*");

            migrationBuilder.UpdateData(
                table: "UserSet",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Password" },
                values: new object[] { "", "admin1234" });
        }
    }
}
