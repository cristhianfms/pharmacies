﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class DrugMigration : Migration
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
                name: "PharmacyDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyDB", x => x.Id);
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
                    DrugInfoId = table.Column<int>(type: "int", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugDB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugDB_DrugInfoDB_DrugInfoId",
                        column: x => x.DrugInfoId,
                        principalTable: "DrugInfoDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugDB_PharmacyDB_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PharmacyDB",
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
                        name: "FK_InvitationDB_PharmacyDB_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PharmacyDB",
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
                        name: "FK_UserDB_PharmacyDB_EmployeePharmacyId",
                        column: x => x.EmployeePharmacyId,
                        principalTable: "PharmacyDB",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserDB_PharmacyDB_OwnerPharmacyId",
                        column: x => x.OwnerPharmacyId,
                        principalTable: "PharmacyDB",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserDB_PharmacyDB_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "PharmacyDB",
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

            migrationBuilder.CreateIndex(
                name: "IX_DrugDB_DrugInfoId",
                table: "DrugDB",
                column: "DrugInfoId");

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
                name: "IX_PermissionRole_PermissionId",
                table: "PermissionRole",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRole_RoleId",
                table: "PermissionRole",
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
                name: "PermissionRole");

            migrationBuilder.DropTable(
                name: "SessionDB");

            migrationBuilder.DropTable(
                name: "DrugInfoDB");

            migrationBuilder.DropTable(
                name: "PermissionDB");

            migrationBuilder.DropTable(
                name: "UserDB");

            migrationBuilder.DropTable(
                name: "PharmacyDB");

            migrationBuilder.DropTable(
                name: "RoleDB");
        }
    }
}
