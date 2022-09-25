using Domain;
using Domain.AuthDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context;

public class PharmacyManagerContext : DbContext
{
    public DbSet<User> UserDB { get; set; }
    public DbSet<Role> RoleDB { get; set; }
    public DbSet<Session> SessionDB { get; set; }
    public DbSet<Invitation> InvitationDB { get; set; }
    public DbSet<Pharmacy> PharmacyDB { get; set; }
    public DbSet<Drug> DrugDB { get; set; }
    public DbSet<DrugInfo> DrugInfoDB { get; set; }
    public DbSet<Permission> PermissionDB { get; set; }

    public PharmacyManagerContext() : base() { }
    public PharmacyManagerContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pharmacy>()
            .HasMany(e => e.Employees)
            .WithOne(u => u.EmployeePharmacy);
        modelBuilder.Entity<Pharmacy>()
            .HasOne(p => p.Owner)
            .WithOne(u => u.OwnerPharmacy);

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

        // Default admin
        modelBuilder.Entity<User>().HasData(
            new User() { Id = 1, UserName = "Admin", Email = "admin@admin", Address = "", Password = "admin1234", RoleId = admin.Id, RegistrationDate = DateTime.Parse("2022-09-01") }
        );
    }

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

}