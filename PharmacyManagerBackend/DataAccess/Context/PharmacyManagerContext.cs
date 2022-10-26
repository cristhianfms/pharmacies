using Domain;
using Domain.AuthDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context;

public class PharmacyManagerContext : DbContext
{
    public DbSet<User> UserSet { get; set; }
    public DbSet<Role> RoleSet { get; set; }
    public DbSet<Session> SessionSet { get; set; }
    public DbSet<Invitation> InvitationSet { get; set; }
    public DbSet<Pharmacy> PharmacySet { get; set; }
    public DbSet<DrugInfo> DrugInfoSet { get; set; }
    public DbSet<Drug> DrugSet { get; set; }
    public DbSet<Permission> PermissionSet { get; set; }
    public DbSet<PermissionRole> PermissionRoleSet { get; set; }
    public DbSet<Solicitude> SolicitudeSet { get; set; }
    public DbSet<Purchase> PurchaseSet { get; set; }
    public PharmacyManagerContext() : base() { }
    public PharmacyManagerContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var directory = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("PharmacyManagerDb");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PermissionRole>()
            .HasKey(p => new { p.RoleId, p.PermissionId });
        modelBuilder.Entity<PermissionRole>()
            .HasOne(bc => bc.Role)
            .WithMany(b => b.PermissionRoles)
            .HasForeignKey(bc => bc.RoleId);
        modelBuilder.Entity<PermissionRole>()
            .HasOne(bc => bc.Permission)
            .WithMany(c => c.PermissionRoles)
            .HasForeignKey(bc => bc.PermissionId);
        modelBuilder.Entity<Pharmacy>()
            .HasMany(e => e.Employees)
            .WithOne(u => u.EmployeePharmacy);
        modelBuilder.Entity<Pharmacy>()
            .HasOne(p => p.Owner)
            .WithOne(u => u.OwnerPharmacy);
        modelBuilder.Entity<PurchaseItem>()
            .HasOne(pi => pi.Pharmacy)
            .WithMany()
            .HasForeignKey("PharmacyId")
            .OnDelete(DeleteBehavior.NoAction);

        // Data seed
        // Roles
        Role admin = new Role() { Id = 1, Name = "Admin" };
        Role owner = new Role() { Id = 2, Name = "Owner" };
        Role employee = new Role() { Id = 3, Name = "Employee" };

        modelBuilder.Entity<Role>().HasData(
            admin,
            owner,
            employee
        );

        // Invitations Permissions
        Permission createInvitation = new Permission() { Id = 1, Endpoint = "POST/api/invitations" };
        Permission updateInvitation = new Permission() { Id = 10, Endpoint = "PUT/api/invitations/.*" };
        Permission getAllInvitations = new Permission() { Id = 11, Endpoint = "GET/api/invitations" };
        
        //Pharmacy permissions
        Permission createPharmacy = new Permission() { Id = 8, Endpoint = "POST/api/pharmacies" };

        //Solicitude Permissions
        Permission createSolicitude = new Permission() { Id = 2, Endpoint = "POST/api/solicitudes" };
        Permission getSolicitudes = new Permission() { Id = 3, Endpoint = "GET/api/solicitudes" };
        Permission updateSolicitude = new Permission() { Id = 4, Endpoint = "PUT/api/solicitudes/.*" };

        //Drug Permissions
        Permission createDrug = new Permission() { Id = 5, Endpoint = "POST/api/drugs" };
        Permission deleteDrug = new Permission() { Id = 6, Endpoint = "DELETE/api/drugs/.*" };
        Permission getDrug = new Permission() { Id = 7, Endpoint = "GET/api/drugs/.*" };

        //Purchase Permissions
        Permission getAllPurchases = new Permission() { Id = 9, Endpoint = "GET/api/purchases" };
        Permission updatePurchase = new Permission() { Id = 12, Endpoint = "PUT/api/purchases/.*" };
        
        //Drug Exporter permissons 
        Permission getDrugExporters = new Permission() { Id = 13, Endpoint = "GET/api/drug-exporters" };
        Permission exportDrugs = new Permission() { Id = 14, Endpoint = "POST/api/drug-exporters/export" };
        
        modelBuilder.Entity<Permission>().HasData(
            createInvitation,
            createSolicitude,
            getSolicitudes,
            updateSolicitude,
            createDrug,
            deleteDrug,
            getDrug,
            createPharmacy,
            getAllPurchases,
            updateInvitation,
            getAllInvitations,
            updatePurchase,
            getDrugExporters,
            exportDrugs);

        // Permission - Role
        modelBuilder.Entity<PermissionRole>().HasData(
            new PermissionRole() { PermissionId = createInvitation.Id, RoleId = admin.Id },
            new PermissionRole() { PermissionId = createInvitation.Id, RoleId = owner.Id },
            new PermissionRole() { PermissionId = createSolicitude.Id, RoleId = employee.Id },
            new PermissionRole() { PermissionId = getSolicitudes.Id, RoleId = employee.Id },
            new PermissionRole() { PermissionId = getSolicitudes.Id, RoleId = owner.Id },
            new PermissionRole() { PermissionId = updateSolicitude.Id, RoleId = owner.Id },
            new PermissionRole() { PermissionId = createDrug.Id, RoleId = employee.Id },
            new PermissionRole() { PermissionId = deleteDrug.Id, RoleId = employee.Id },
            new PermissionRole() { PermissionId = getDrug.Id, RoleId = employee.Id },
            new PermissionRole() { PermissionId = createPharmacy.Id, RoleId = admin.Id },
            new PermissionRole() { PermissionId = getAllPurchases.Id, RoleId = admin.Id },
            new PermissionRole() { PermissionId = getAllPurchases.Id, RoleId = employee.Id },
            new PermissionRole() { PermissionId = getAllPurchases.Id, RoleId = owner.Id },
            new PermissionRole() { PermissionId = updatePurchase.Id, RoleId = employee.Id },
            new PermissionRole() { PermissionId = updateInvitation.Id, RoleId = admin.Id },
            new PermissionRole() { PermissionId = getAllInvitations.Id, RoleId = admin.Id },
            new PermissionRole() { PermissionId = getDrugExporters.Id, RoleId = employee.Id },
            new PermissionRole() { PermissionId = exportDrugs.Id, RoleId = employee.Id }
        );


        // Default admin
        modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = 1,
                UserName = "Admin",
                Email = "admin@admin",
                Address = "dirección",
                Password = "admin1234-",
                RoleId = admin.Id,
                RegistrationDate = DateTime.Parse("2022-09-01")
            }
        );

    }



}