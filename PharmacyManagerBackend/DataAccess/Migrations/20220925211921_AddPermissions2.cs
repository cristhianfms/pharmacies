using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddPermissions2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrugDB_PharmacieDB_PharmacyId",
                table: "DrugDB");

            migrationBuilder.DropForeignKey(
                name: "FK_InvitationDB_PharmacieDB_PharmacyId",
                table: "InvitationDB");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDB_PharmacieDB_EmployeePharmacyId",
                table: "UserDB");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDB_PharmacieDB_OwnerPharmacyId",
                table: "UserDB");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDB_PharmacieDB_PharmacyId",
                table: "UserDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PharmacieDB",
                table: "PharmacieDB");

            migrationBuilder.RenameTable(
                name: "PharmacieDB",
                newName: "PharmacyDB");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PharmacyDB",
                table: "PharmacyDB",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PermissionDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Endpoint = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionRole_PermissionDB_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "PermissionDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionRole_RoleDB_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRole_PermissionId",
                table: "PermissionRole",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRole_RoleId",
                table: "PermissionRole",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrugDB_PharmacyDB_PharmacyId",
                table: "DrugDB",
                column: "PharmacyId",
                principalTable: "PharmacyDB",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvitationDB_PharmacyDB_PharmacyId",
                table: "InvitationDB",
                column: "PharmacyId",
                principalTable: "PharmacyDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDB_PharmacyDB_EmployeePharmacyId",
                table: "UserDB",
                column: "EmployeePharmacyId",
                principalTable: "PharmacyDB",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDB_PharmacyDB_OwnerPharmacyId",
                table: "UserDB",
                column: "OwnerPharmacyId",
                principalTable: "PharmacyDB",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDB_PharmacyDB_PharmacyId",
                table: "UserDB",
                column: "PharmacyId",
                principalTable: "PharmacyDB",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrugDB_PharmacyDB_PharmacyId",
                table: "DrugDB");

            migrationBuilder.DropForeignKey(
                name: "FK_InvitationDB_PharmacyDB_PharmacyId",
                table: "InvitationDB");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDB_PharmacyDB_EmployeePharmacyId",
                table: "UserDB");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDB_PharmacyDB_OwnerPharmacyId",
                table: "UserDB");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDB_PharmacyDB_PharmacyId",
                table: "UserDB");

            migrationBuilder.DropTable(
                name: "PermissionRole");

            migrationBuilder.DropTable(
                name: "PermissionDB");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PharmacyDB",
                table: "PharmacyDB");

            migrationBuilder.RenameTable(
                name: "PharmacyDB",
                newName: "PharmacieDB");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PharmacieDB",
                table: "PharmacieDB",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrugDB_PharmacieDB_PharmacyId",
                table: "DrugDB",
                column: "PharmacyId",
                principalTable: "PharmacieDB",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvitationDB_PharmacieDB_PharmacyId",
                table: "InvitationDB",
                column: "PharmacyId",
                principalTable: "PharmacieDB",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDB_PharmacieDB_EmployeePharmacyId",
                table: "UserDB",
                column: "EmployeePharmacyId",
                principalTable: "PharmacieDB",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDB_PharmacieDB_OwnerPharmacyId",
                table: "UserDB",
                column: "OwnerPharmacyId",
                principalTable: "PharmacieDB",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDB_PharmacieDB_PharmacyId",
                table: "UserDB",
                column: "PharmacyId",
                principalTable: "PharmacieDB",
                principalColumn: "Id");
        }
    }
}
