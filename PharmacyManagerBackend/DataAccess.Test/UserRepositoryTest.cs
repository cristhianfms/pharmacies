using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using DataAccess.Context;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        private DbConnection _connection;
        private BaseRepository<User> _userRepository;
        private BaseRepository<Role> _roleRepository;
        private BaseRepository<Pharmacy> _pharmacyRepository;

        private PharmacyManagerContext _pharmacyManagerContext;
        private DbContextOptions<PharmacyManagerContext> _contextOptions;

        public UserRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<PharmacyManagerContext>().UseSqlite(this._connection).Options;

            this._pharmacyManagerContext = new PharmacyManagerContext(this._contextOptions);
            this._userRepository = new BaseRepository<User>(this._pharmacyManagerContext);
            this._roleRepository = new BaseRepository<Role>(this._pharmacyManagerContext);
            this._pharmacyRepository = new BaseRepository<Pharmacy>(this._pharmacyManagerContext);
        }

        [TestInitialize]
        public void SetUp()
        {
            this._connection.Open();
            this._pharmacyManagerContext.Database.EnsureCreated();
        }

        [TestCleanup]
        public void CleanUp()
        {
            this._pharmacyManagerContext.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CreateUserOk()
        {
            User userToCreate = new User()
            {
                UserName = "Cris",
                Role = new Role()
                {
                    Name = "ADMIN"
                },
                Email = "cris@gmail.com",
                Address = "Address",
                Password = "Password",
                RegistrationDate = DateTime.Now,
                Pharmacy = new Pharmacy()
                {
                    Name = "Pharmashop"
                }
            };

            User createdUser = this._userRepository.Create(userToCreate);

            using (var context = new PharmacyManagerContext(this._contextOptions))
            {
                var users = context.Set<User>();
                User userInDB = users.FirstOrDefault(user => user.Id == createdUser.Id);
                Assert.AreEqual(createdUser, userInDB);
            }
        }
    }
}
