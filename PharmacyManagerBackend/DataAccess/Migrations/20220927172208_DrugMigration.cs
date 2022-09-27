using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class DrugMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DrugInfoId",
                table: "DrugDB",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DrugDB_DrugInfoId",
                table: "DrugDB",
                column: "DrugInfoId");

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

            migrationBuilder.DropIndex(
                name: "IX_DrugDB_DrugInfoId",
                table: "DrugDB");

            migrationBuilder.DropColumn(
                name: "DrugInfoId",
                table: "DrugDB");
        }
    }
}
