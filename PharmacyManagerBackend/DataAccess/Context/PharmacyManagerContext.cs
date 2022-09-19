using System.IO;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context
{
    public class PharmacyManagerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }

        public PharmacyManagerContext(DbContextOptions options) : base(options)
        {
        }

        protected PharmacyManagerContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString(@"PharmacyManagerDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: delete after tests
            modelBuilder.Entity<Pharmacy>().HasData(
                new Pharmacy() { Id = 1, Name = "pharmacy", Address = "address 1" }
            );

            // Roles
            Role admin = new Role() { Id = 1, Name = "ADMIN" };
            Role owner = new Role() { Id = 2, Name = "OWNER" };
            Role employee = new Role() { Id = 3, Name = "EMPLOYEE" };
            modelBuilder.Entity<Role>().HasData(
                admin,
                owner,
                employee
            );

            // Users
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, UserName = "Admin", Email = "admin@admin", Password = "admin1234", RoleId = admin.Id }
            );
        }
    }
}