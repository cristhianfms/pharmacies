using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class PharmacyManagerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrugInfoSet",
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
                    table.PrimaryKey("PK_DrugInfoSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Endpoint = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PharmacySet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacySet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    NeedsPrescription = table.Column<bool>(type: "bit", nullable: false),
                    DrugInfoId = table.Column<int>(type: "int", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugSet_DrugInfoSet_DrugInfoId",
                        column: x => x.DrugInfoId,
                        principalTable: "DrugInfoSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugSet_PharmacySet_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PharmacySet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvitationSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: true),
                    Used = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitationSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvitationSet_PharmacySet_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PharmacySet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvitationSet_RoleSet_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionRoleSet",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRoleSet", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_PermissionRoleSet_PermissionSet_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "PermissionSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionRoleSet_RoleSet_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSet",
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
                    table.PrimaryKey("PK_UserSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSet_PharmacySet_EmployeePharmacyId",
                        column: x => x.EmployeePharmacyId,
                        principalTable: "PharmacySet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSet_PharmacySet_OwnerPharmacyId",
                        column: x => x.OwnerPharmacyId,
                        principalTable: "PharmacySet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSet_PharmacySet_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PharmacySet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSet_RoleSet_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugId = table.Column<int>(type: "int", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PurchaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseItem_DrugSet_DrugId",
                        column: x => x.DrugId,
                        principalTable: "DrugSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseItem_PharmacySet_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PharmacySet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseItem_PurchaseSet_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "PurchaseSet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SessionSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionSet_UserSet_UserId",
                        column: x => x.UserId,
                        principalTable: "UserSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudeSet",
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
                    table.PrimaryKey("PK_SolicitudeSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudeSet_PharmacySet_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PharmacySet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudeSet_UserSet_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "UserSet",
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
                        name: "FK_SolicitudeItem_SolicitudeSet_SolicitudeId",
                        column: x => x.SolicitudeId,
                        principalTable: "SolicitudeSet",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "PermissionSet",
                columns: new[] { "Id", "Endpoint" },
                values: new object[,]
                {
                    { 1, "POST/api/invitations" },
                    { 2, "POST/api/solicitudes" },
                    { 3, "GET/api/solicitudes" },
                    { 4, "PUT/api/solicitudes/.*" },
                    { 5, "POST/api/drugs" },
                    { 6, "DELETE/api/drugs/.*" },
                    { 7, "GET/api/drugs/.*" },
                    { 8, "POST/api/pharmacies" },
                    { 9, "GET/api/purchases" },
                    { 10, "PUT/api/invitations/.*" },
                    { 11, "GET/api/invitations" }
                });

            migrationBuilder.InsertData(
                table: "RoleSet",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Owner" },
                    { 3, "Employee" }
                });

            migrationBuilder.InsertData(
                table: "PermissionRoleSet",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 1, 2 },
                    { 3, 2 },
                    { 4, 2 },
                    { 9, 2 },
                    { 2, 3 },
                    { 3, 3 },
                    { 5, 3 },
                    { 6, 3 },
                    { 7, 3 },
                    { 9, 3 }
                });

            migrationBuilder.InsertData(
                table: "UserSet",
                columns: new[] { "Id", "Address", "Email", "EmployeePharmacyId", "OwnerPharmacyId", "Password", "PharmacyId", "RegistrationDate", "RoleId", "UserName" },
                values: new object[] { 1, "dirección", "admin@admin", null, null, "admin1234-", null, new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_DrugSet_DrugInfoId",
                table: "DrugSet",
                column: "DrugInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugSet_PharmacyId",
                table: "DrugSet",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitationSet_PharmacyId",
                table: "InvitationSet",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitationSet_RoleId",
                table: "InvitationSet",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRoleSet_PermissionId",
                table: "PermissionRoleSet",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItem_DrugId",
                table: "PurchaseItem",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItem_PharmacyId",
                table: "PurchaseItem",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItem_PurchaseId",
                table: "PurchaseItem",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSet_UserId",
                table: "SessionSet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudeItem_SolicitudeId",
                table: "SolicitudeItem",
                column: "SolicitudeId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudeSet_EmployeeId",
                table: "SolicitudeSet",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudeSet_PharmacyId",
                table: "SolicitudeSet",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSet_EmployeePharmacyId",
                table: "UserSet",
                column: "EmployeePharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSet_OwnerPharmacyId",
                table: "UserSet",
                column: "OwnerPharmacyId",
                unique: true,
                filter: "[OwnerPharmacyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserSet_PharmacyId",
                table: "UserSet",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSet_RoleId",
                table: "UserSet",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvitationSet");

            migrationBuilder.DropTable(
                name: "PermissionRoleSet");

            migrationBuilder.DropTable(
                name: "PurchaseItem");

            migrationBuilder.DropTable(
                name: "SessionSet");

            migrationBuilder.DropTable(
                name: "SolicitudeItem");

            migrationBuilder.DropTable(
                name: "PermissionSet");

            migrationBuilder.DropTable(
                name: "DrugSet");

            migrationBuilder.DropTable(
                name: "PurchaseSet");

            migrationBuilder.DropTable(
                name: "SolicitudeSet");

            migrationBuilder.DropTable(
                name: "DrugInfoSet");

            migrationBuilder.DropTable(
                name: "UserSet");

            migrationBuilder.DropTable(
                name: "PharmacySet");

            migrationBuilder.DropTable(
                name: "RoleSet");
        }
    }
}
