using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class SolicitudesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DrugInfoId",
                table: "DrugDB",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SolicitudeDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudeDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudeDB_PharmacyDB_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PharmacyDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudeDB_UserDB_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "UserDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudeItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugQuantity = table.Column<int>(type: "int", nullable: false),
                    DrugCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SolicitudeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudeItem_SolicitudeDB_SolicitudeId",
                        column: x => x.SolicitudeId,
                        principalTable: "SolicitudeDB",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrugDB_DrugInfoId",
                table: "DrugDB",
                column: "DrugInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudeDB_EmployeeId",
                table: "SolicitudeDB",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudeDB_PharmacyId",
                table: "SolicitudeDB",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudeItem_SolicitudeId",
                table: "SolicitudeItem",
                column: "SolicitudeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrugDB_DrugInfoDB_DrugInfoId",
                table: "DrugDB",
                column: "DrugInfoId",
                principalTable: "DrugInfoDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrugDB_DrugInfoDB_DrugInfoId",
                table: "DrugDB");

            migrationBuilder.DropTable(
                name: "SolicitudeItem");

            migrationBuilder.DropTable(
                name: "SolicitudeDB");

            migrationBuilder.DropIndex(
                name: "IX_DrugDB_DrugInfoId",
                table: "DrugDB");

            migrationBuilder.DropColumn(
                name: "DrugInfoId",
                table: "DrugDB");
        }
    }
}
