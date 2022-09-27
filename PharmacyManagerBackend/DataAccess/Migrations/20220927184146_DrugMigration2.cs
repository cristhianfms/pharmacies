using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class DrugMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DrugDB_DrugInfoId",
                table: "DrugDB");

            migrationBuilder.CreateIndex(
                name: "IX_DrugDB_DrugInfoId",
                table: "DrugDB",
                column: "DrugInfoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DrugDB_DrugInfoId",
                table: "DrugDB");

            migrationBuilder.CreateIndex(
                name: "IX_DrugDB_DrugInfoId",
                table: "DrugDB",
                column: "DrugInfoId");
        }
    }
}
