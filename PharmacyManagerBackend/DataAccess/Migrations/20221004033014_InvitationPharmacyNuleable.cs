using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class InvitationPharmacyNuleable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvitationSet_PharmacySet_PharmacyId",
                table: "InvitationSet");

            migrationBuilder.AlterColumn<int>(
                name: "PharmacyId",
                table: "InvitationSet",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_InvitationSet_PharmacySet_PharmacyId",
                table: "InvitationSet",
                column: "PharmacyId",
                principalTable: "PharmacySet",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvitationSet_PharmacySet_PharmacyId",
                table: "InvitationSet");

            migrationBuilder.AlterColumn<int>(
                name: "PharmacyId",
                table: "InvitationSet",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvitationSet_PharmacySet_PharmacyId",
                table: "InvitationSet",
                column: "PharmacyId",
                principalTable: "PharmacySet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
