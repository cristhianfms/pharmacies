﻿// <auto-generated />
using System;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(PharmacyManagerContext))]
    partial class PharmacyManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.ToTable("PermissionSet");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Endpoint = "POST/api/invitations"
                        },
                        new
                        {
                            Id = 2,
                            Endpoint = "POST/api/solicitudes"
                        },
                        new
                        {
                            Id = 3,
                            Endpoint = "GET/api/solicitudes"
                        },
                        new
                        {
                            Id = 4,
                            Endpoint = "PUT/api/solicitudes/.*"
                        },
                        new
                        {
                            Id = 5,
                            Endpoint = "POST/api/drugs"
                        },
                        new
                        {
                            Id = 6,
                            Endpoint = "DELETE/api/drugs/.*"
                        },
                        new
                        {
                            Id = 7,
                            Endpoint = "GET/api/drugs/.*"
                        },
                        new
                        {
                            Id = 16,
                            Endpoint = "GET/api/drugs"
                        },
                        new
                        {
                            Id = 8,
                            Endpoint = "POST/api/pharmacies"
                        },
                        new
                        {
                            Id = 15,
                            Endpoint = "GET/api/pharmacies"
                        },
                        new
                        {
                            Id = 9,
                            Endpoint = "GET/api/purchases"
                        },
                        new
                        {
                            Id = 10,
                            Endpoint = "PUT/api/invitations/.*"
                        },
                        new
                        {
                            Id = 11,
                            Endpoint = "GET/api/invitations"
                        },
                        new
                        {
                            Id = 12,
                            Endpoint = "PUT/api/purchases/.*"
                        },
                        new
                        {
                            Id = 13,
                            Endpoint = "GET/api/drug-exporters"
                        },
                        new
                        {
                            Id = 14,
                            Endpoint = "POST/api/drug-exporters/export"
                        });
                });

            modelBuilder.Entity("Domain.AuthDomain.PermissionRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("PermissionRoleSet");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 5
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 6
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 7
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 16
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 8
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 15
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 9
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 9
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 9
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 12
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 10
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 11
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 13
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 14
                        });
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

                    b.ToTable("SessionSet");
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

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("NeedsPrescription")
                        .HasColumnType("bit");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DrugInfoId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("DrugSet");
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

                    b.ToTable("DrugInfoSet");
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

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<bool>("Used")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("RoleId");

                    b.ToTable("InvitationSet");
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

                    b.ToTable("PharmacySet");
                });

            modelBuilder.Entity("Domain.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PurchaseSet");
                });

            modelBuilder.Entity("Domain.PurchaseItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DrugId")
                        .HasColumnType("int");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<int?>("PurchaseId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DrugId");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("PurchaseId");

                    b.ToTable("PurchaseItem");
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

                    b.ToTable("RoleSet");

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

            modelBuilder.Entity("Domain.Solicitude", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("SolicitudeSet");
                });

            modelBuilder.Entity("Domain.SolicitudeItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DrugCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DrugQuantity")
                        .HasColumnType("int");

                    b.Property<int?>("SolicitudeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SolicitudeId");

                    b.ToTable("SolicitudeItem");
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

                    b.ToTable("UserSet");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "dirección",
                            Email = "admin@admin",
                            Password = "admin1234-",
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

                    b.HasOne("Domain.Pharmacy", "Pharmacy")
                        .WithMany("Drugs")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DrugInfo");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("Domain.Invitation", b =>
                {
                    b.HasOne("Domain.Pharmacy", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyId");

                    b.HasOne("Domain.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pharmacy");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.PurchaseItem", b =>
                {
                    b.HasOne("Domain.Drug", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Pharmacy", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Purchase", null)
                        .WithMany("Items")
                        .HasForeignKey("PurchaseId");

                    b.Navigation("Drug");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("Domain.Solicitude", b =>
                {
                    b.HasOne("Domain.User", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Pharmacy", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("Domain.SolicitudeItem", b =>
                {
                    b.HasOne("Domain.Solicitude", null)
                        .WithMany("Items")
                        .HasForeignKey("SolicitudeId");
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

            modelBuilder.Entity("Domain.Purchase", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Domain.Role", b =>
                {
                    b.Navigation("PermissionRoles");
                });

            modelBuilder.Entity("Domain.Solicitude", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
