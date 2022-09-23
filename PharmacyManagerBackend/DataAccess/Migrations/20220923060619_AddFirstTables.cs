using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddFirstTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PharmacieDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacieDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    NeedsPrescription = table.Column<bool>(type: "bit", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugDB_PharmacieDB_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PharmacieDB",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvitationDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitationDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvitationDB_PharmacieDB_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PharmacieDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvitationDB_RoleDB_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerPharmacyId = table.Column<int>(type: "int", nullable: true),
                    EmployeePharmacyId = table.Column<int>(type: "int", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDB_PharmacieDB_EmployeePharmacyId",
                        column: x => x.EmployeePharmacyId,
                        principalTable: "PharmacieDB",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserDB_PharmacieDB_OwnerPharmacyId",
                        column: x => x.OwnerPharmacyId,
                        principalTable: "PharmacieDB",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserDB_PharmacieDB_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PharmacieDB",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserDB_RoleDB_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionDB_UserDB_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrugDB_PharmacyId",
                table: "DrugDB",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitationDB_PharmacyId",
                table: "InvitationDB",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitationDB_RoleId",
                table: "InvitationDB",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionDB_UserId",
                table: "SessionDB",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDB_EmployeePharmacyId",
                table: "UserDB",
                column: "EmployeePharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDB_OwnerPharmacyId",
                table: "UserDB",
                column: "OwnerPharmacyId",
                unique: true,
                filter: "[OwnerPharmacyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserDB_PharmacyId",
                table: "UserDB",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDB_RoleId",
                table: "UserDB",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrugDB");

            migrationBuilder.DropTable(
                name: "InvitationDB");

            migrationBuilder.DropTable(
                name: "SessionDB");

            migrationBuilder.DropTable(
                name: "UserDB");

            migrationBuilder.DropTable(
                name: "PharmacieDB");

            migrationBuilder.DropTable(
                name: "RoleDB");
        }
    }
}
