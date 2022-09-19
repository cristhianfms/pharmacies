using System;
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
        private PharmacyManagerContext _pharmacyManagerContext;
        private DbContextOptions<PharmacyManagerContext> _contextOptions;

        [TestMethod]
        public void ConcertRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<PharmacyManagerContext>().UseSqlite(this._connection).Options;

            this._pharmacyManagerContext = new PharmacyManagerContext(this._contextOptions);
            this._userRepository = new BaseRepository<User>(this._pharmacyManagerContext);
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
                    Id = 1
                },
                Email = "cris@gmail.com",
                Address = "Address",
                Password = "Password",
                RegistrationDate = DateTime.Now,
                Pharmacy = new Pharmacy()
                {
                    Id = 1
                }
            };

            User createdUser = this._userRepository.Create(userToCreate);

            using (var context = new PharmacyManagerContext(this._contextOptions))
            {
                var users = context.Set<User>();
                Assert.AreEqual(1, users.Count());
                User userInDB = users.FirstOrDefault(user => user.Id == createdUser.Id);
                Assert.AreEqual(createdUser, userInDB);
            }
        }
        
    }
}
