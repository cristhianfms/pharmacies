﻿// <auto-generated />
using System;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(PharmacyManagerContext))]
    [Migration("20220928121652_DrugMigration")]
    partial class DrugMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.AuthDomain.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Endpoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PermissionDB");
                });

            modelBuilder.Entity("Domain.AuthDomain.PermissionRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("PermissionRole");
                });

            modelBuilder.Entity("Domain.AuthDomain.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("Token")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SessionDB");
                });

            modelBuilder.Entity("Domain.Drug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DrugCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DrugInfoId")
                        .HasColumnType("int");

                    b.Property<bool>("NeedsPrescription")
                        .HasColumnType("bit");

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DrugInfoId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("DrugDB");
                });

            modelBuilder.Entity("Domain.DrugInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Presentation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("QuantityPerPresentation")
                        .HasColumnType("real");

                    b.Property<string>("Symptoms")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitOfMeasurement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DrugInfoDB");
                });

            modelBuilder.Entity("Domain.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("RoleId");

                    b.ToTable("InvitationDB");
                });

            modelBuilder.Entity("Domain.Pharmacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PharmacyDB");
                });

            modelBuilder.Entity("Domain.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoleDB");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Owner"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Employee"
                        });
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmployeePharmacyId")
                        .HasColumnType("int");

                    b.Property<int?>("OwnerPharmacyId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeePharmacyId");

                    b.HasIndex("OwnerPharmacyId")
                        .IsUnique()
                        .HasFilter("[OwnerPharmacyId] IS NOT NULL");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserDB");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "",
                            Email = "admin@admin",
                            Password = "admin1234",
                            RegistrationDate = new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoleId = 1,
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("Domain.AuthDomain.PermissionRole", b =>
                {
                    b.HasOne("Domain.AuthDomain.Permission", "Permission")
                        .WithMany("PermissionRoles")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Role", "Role")
                        .WithMany("PermissionRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.AuthDomain.Session", b =>
                {
                    b.HasOne("Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Drug", b =>
                {
                    b.HasOne("Domain.DrugInfo", "DrugInfo")
                        .WithMany()
                        .HasForeignKey("DrugInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Pharmacy", null)
                        .WithMany("Drugs")
                        .HasForeignKey("PharmacyId");

                    b.Navigation("DrugInfo");
                });

            modelBuilder.Entity("Domain.Invitation", b =>
                {
                    b.HasOne("Domain.Pharmacy", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pharmacy");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.HasOne("Domain.Pharmacy", "EmployeePharmacy")
                        .WithMany("Employees")
                        .HasForeignKey("EmployeePharmacyId");

                    b.HasOne("Domain.Pharmacy", "OwnerPharmacy")
                        .WithOne("Owner")
                        .HasForeignKey("Domain.User", "OwnerPharmacyId");

                    b.HasOne("Domain.Pharmacy", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyId");

                    b.HasOne("Domain.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeePharmacy");

                    b.Navigation("OwnerPharmacy");

                    b.Navigation("Pharmacy");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.AuthDomain.Permission", b =>
                {
                    b.Navigation("PermissionRoles");
                });

            modelBuilder.Entity("Domain.Pharmacy", b =>
                {
                    b.Navigation("Drugs");

                    b.Navigation("Employees");

                    b.Navigation("Owner")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Role", b =>
                {
                    b.Navigation("PermissionRoles");
                });
#pragma warning restore 612, 618
        }
    }
}