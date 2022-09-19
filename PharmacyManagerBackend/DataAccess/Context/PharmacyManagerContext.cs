using System.IO;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context
{
    public class PharmacyManagerContext : DbContext
    {
        public DbSet<User> UserDB { get; set; }
        public DbSet<Role> RoleDB { get; set; }
        public DbSet<Session> SessionDB { get; set; }
        public DbSet<Invitation> InvitationDB { get; set; }
        public DbSet<Pharmacy> PharmacieDB { get; set; }
        public DbSet<Drug> DrugDB { get; set; }

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


            // Roles
            Role admin = new Role() { Id = 1, Name = "ADMIN" };
            Role owner = new Role() { Id = 2, Name = "OWNER" };
            Role employee = new Role() { Id = 3, Name = "EMPLOYEE" };
            modelBuilder.Entity<Role>().HasData(
                admin,
                owner,
                employee
            );

            //TODO: delete after tests
            modelBuilder.Entity<Pharmacy>().HasData(
                new Pharmacy() { Id = 1, Name = "pharmacy", Address = "address 1" }
            );

            // Users
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, UserName = "Admin", Email = "admin@admin", Password = "admin1234", RoleId = admin.Id }
            );
        }
    }
}