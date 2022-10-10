using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class UpdateInvitationAdminPermision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PermissionSet",
                columns: new[] { "Id", "Endpoint" },
                values: new object[] { 10, "PUT/api/invitations/.*" });

            migrationBuilder.InsertData(
                table: "PermissionRoleSet",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 10, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionRoleSet",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "PermissionSet",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
