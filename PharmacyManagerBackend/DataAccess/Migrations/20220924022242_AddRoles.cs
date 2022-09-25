using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrugInfoDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symptoms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Presentation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityPerPresentation = table.Column<float>(type: "real", nullable: false),
                    UnitOfMeasurement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugInfoDB", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RoleDB",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "RoleDB",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Owner" });

            migrationBuilder.InsertData(
                table: "RoleDB",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Employee" });

            migrationBuilder.InsertData(
                table: "UserDB",
                columns: new[] { "Id", "Address", "Email", "EmployeePharmacyId", "OwnerPharmacyId", "Password", "PharmacyId", "RegistrationDate", "RoleId", "UserName" },
                values: new object[] { 1, "", "admin@admin", null, null, "admin1234", null, new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrugInfoDB");

            migrationBuilder.DeleteData(
                table: "RoleDB",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoleDB",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserDB",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoleDB",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
