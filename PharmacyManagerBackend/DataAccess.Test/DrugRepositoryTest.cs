using DataAccess.Context;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DataAccess.Test
{
    [TestClass]
    public class DrugRepositoryTest
    {
        private DbConnection _connection;
        private BaseRepository<Drug> _drugRepository;

        private PharmacyManagerContext _pharmacyManagerContext;
        private DbContextOptions<PharmacyManagerContext> _contextOptions;

        public DrugRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<PharmacyManagerContext>().UseSqlite(this._connection).Options;

            this._pharmacyManagerContext = new PharmacyManagerContext(this._contextOptions);
            this._drugRepository = new BaseRepository<Drug>(this._pharmacyManagerContext);
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
        public void DeleteDrugOk()
        {
            DrugInfo di = new DrugInfo()
            {
                Name = "Perifar Flex",
                Symptoms = "Dolor de cabeza",
                Presentation = "Blister",
                QuantityPerPresentation = 8,
                UnitOfMeasurement = "Gramos"
            };

            Drug drugToDelete = new Drug()
            {
                DrugCode = "2a5678bx1",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false,
                DrugInfo = di
            };

            using (var context = new PharmacyManagerContext(this._contextOptions))
            {
                Drug drugCreated = this._drugRepository.Create(drugToDelete);
                var drugs = context.Set<Drug>();
                _drugRepository.Delete(drugCreated);
                Assert.IsFalse(_drugRepository.GetAll().Contains<Drug>(drugCreated));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteDrugFail()
        {
            Drug drugToDelete = new Drug()
            {
                DrugCode = "2a5678bx1",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false
            };

            using (var context = new PharmacyManagerContext(this._contextOptions))
            {
                var drugs = context.Set<Drug>();
                _drugRepository.Delete(drugToDelete);                
            }
        }

    }
}
