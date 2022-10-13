using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class UpdatePurchaseProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseSet_PharmacySet_PharmacyId",
                table: "PurchaseSet");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseSet_PharmacyId",
                table: "PurchaseSet");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "PurchaseSet");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "PurchaseSet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PharmacyId",
                table: "PurchaseItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "PurchaseItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItem_PharmacyId",
                table: "PurchaseItem",
                column: "PharmacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItem_PharmacySet_PharmacyId",
                table: "PurchaseItem",
                column: "PharmacyId",
                principalTable: "PharmacySet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItem_PharmacySet_PharmacyId",
                table: "PurchaseItem");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseItem_PharmacyId",
                table: "PurchaseItem");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "PurchaseSet");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "PurchaseItem");

            migrationBuilder.DropColumn(
                name: "State",
                table: "PurchaseItem");

            migrationBuilder.AddColumn<int>(
                name: "PharmacyId",
                table: "PurchaseSet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseSet_PharmacyId",
                table: "PurchaseSet",
                column: "PharmacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseSet_PharmacySet_PharmacyId",
                table: "PurchaseSet",
                column: "PharmacyId",
                principalTable: "PharmacySet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
