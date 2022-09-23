using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context;

public class PharmacyManagerContext : DbContext
{
    public DbSet<User> UserDB { get; set; }
    public DbSet<Role> RoleDB { get; set; }
    public DbSet<Session> SessionDB { get; set; }
    public DbSet<Invitation> InvitationDB { get; set; }
    public DbSet<Pharmacy> PharmacieDB { get; set; }
    public DbSet<Drug> DrugDB { get; set; }

    public PharmacyManagerContext(DbContextOptions options) : base(options) { }
    public PharmacyManagerContext() : base() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pharmacy>()
            .HasMany(e => e.Employees)
            .WithOne(u => u.EmployeePharmacy);
        modelBuilder.Entity<Pharmacy>()
            .HasOne(p => p.Owner)
            .WithOne(u => u.OwnerPharmacy);
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