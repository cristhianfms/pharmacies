using System;
using Domain;
using Exceptions;
using IDataAccess;
using Moq;
using System.Collections.Generic;

namespace BusinessLogic.Test
{
    [TestClass]
    public class DrugLogicTest
    {
        private DrugLogic _drugLogic;
        private Mock<IDrugRepository> _drugRepositoryMock;
        private Mock<IDrugInfoRepository> _drugInfoRepositoryMock;
        private Mock<IPharmacyRepository> _pharmacyRepositoryMock;
        private User _userEmployeeForTest;

        [TestInitialize]
        public void Initialize()
        {
            this._drugRepositoryMock = new Mock<IDrugRepository>(MockBehavior.Strict);
            this._drugInfoRepositoryMock = new Mock<IDrugInfoRepository>(MockBehavior.Strict);
            this._pharmacyRepositoryMock = new Mock<IPharmacyRepository>(MockBehavior.Strict);
            this._drugLogic = new DrugLogic(_drugRepositoryMock.Object, _drugInfoRepositoryMock.Object, _pharmacyRepositoryMock.Object);


            _userEmployeeForTest = new User()
            {
                Id = 1,
                UserName = "Usuario1",
                Email = "ususario@user.com",
                Address = "Cuareim 123",
                Password = "Usuario+1EsLacontraseña*",
                EmployeePharmacy = new Pharmacy()
                {
                    Id = 1,
                    Name = "Pharmashop",
                    Drugs = new List<Drug>()
                },
                Role = new Role()
                {
                    Name = "Employee"
                }
            };
            _drugLogic.SetContext(_userEmployeeForTest);
        }

        [TestMethod]
        public void CreateNewDrugOk()
        {
            Pharmacy pharmacy = _userEmployeeForTest.Pharmacy;

            Drug drug = new Drug()
            {
                Id = 1,
                DrugCode = "2a5678bx1",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false,
                DrugInfo = new DrugInfo(),
                PharmacyId = pharmacy.Id,
            };

            _pharmacyRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Pharmacy, bool>>())).Returns(pharmacy);
            _drugRepositoryMock.Setup(m => m.Create(It.IsAny<Drug>())).Returns(drug);
            _drugInfoRepositoryMock.Setup(m => m.Create(It.IsAny<DrugInfo>())).Returns(new DrugInfo());
            _pharmacyRepositoryMock.Setup(m => m.Update(It.IsAny<Pharmacy>()));

            Drug createdDrug = _drugLogic.Create(drug);

            _drugRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateNewDrugPharmacyDoesNotExist()
        {
            Pharmacy pharmacy = new Pharmacy
            {
                Id = 2
            };

            Drug drug = new Drug()
            {
                Id = 1,
                DrugCode = "2a5678bx1",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false,
                DrugInfo = new DrugInfo(),
                PharmacyId = pharmacy.Id
            };


            _pharmacyRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Pharmacy, bool>>())).Throws(new ValidationException(""));

            Drug createdDrug = _drugLogic.Create(drug);

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateNewDrugRepeatedPharmacy()
        {
            List<Drug> drugs = new List<Drug>();


            Pharmacy pharmacy = new Pharmacy
            {
                Id = 1,
                Drugs = drugs
            };

            Drug drug = new Drug()
            {
                Id = 1,
                DrugCode = "2a5678bx1",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false,
                DrugInfo = new DrugInfo(),
                PharmacyId = pharmacy.Id
            };


            _pharmacyRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Pharmacy, bool>>())).Returns(pharmacy);
            _drugRepositoryMock.Setup(m => m.Create(It.IsAny<Drug>())).Returns(drug);
            _drugInfoRepositoryMock.Setup(m => m.Create(It.IsAny<DrugInfo>())).Returns(new DrugInfo());
            _pharmacyRepositoryMock.Setup(m => m.Update(It.IsAny<Pharmacy>()));

            Drug createdDrug = _drugLogic.Create(drug);
            _drugLogic.Create(drug);

            _drugRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteDrugOk()
        {
            List<Drug> drugs = new List<Drug>();


            Pharmacy pharmacy = new Pharmacy
            {
                Id = 1,
                Drugs = drugs
            };

            DrugInfo drugInfo = new DrugInfo();


            Drug drug = new Drug()
            {
                Id = 1,
                DrugCode = "2a5678bx",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false,
                DrugInfo = drugInfo,
                PharmacyId = pharmacy.Id
            };

            _pharmacyRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Pharmacy, bool>>())).Returns(pharmacy);
            _drugRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Drug, bool>>())).Returns(drug);
            _pharmacyRepositoryMock.Setup(m => m.Update(It.IsAny<Pharmacy>()));
            _drugInfoRepositoryMock.Setup(m => m.Delete(It.IsAny<DrugInfo>()));
            _drugInfoRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<DrugInfo, bool>>())).Returns(drugInfo);
            _drugLogic.Delete(drug.Id);

            _drugRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void AddStockOk()
        {

            SolicitudeItem solicitudeItem1 = new SolicitudeItem()
            {
                DrugCode = "RS546",
                DrugQuantity = 10
            };
            List<SolicitudeItem> solicitudeItems = new List<SolicitudeItem>()
            {
                solicitudeItem1,
            };
            Drug drug = new Drug()
            {
                Id = 1,
                DrugCode = "RS546",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false
            };

            _drugRepositoryMock.Setup(s => s.GetFirst(It.IsAny<Func<Drug, bool>>())).Returns(drug);
            _drugRepositoryMock.Setup(s => s.Update(It.IsAny<Drug>()));
            _drugLogic.AddStock(solicitudeItems);

            Assert.AreEqual(drug.Stock, 25);
            Assert.AreEqual(drug.DrugCode, solicitudeItem1.DrugCode);

            _drugRepositoryMock.VerifyAll();
        }


        [TestMethod]
        public void UpdateDrugOk()
        {
            int drugId = 1;
            Drug drugToUpdate = new Drug()
            {
                DrugCode = "RS546",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false
            };
            Drug drugInRepository = new Drug()
            {
                Id = drugId,
                DrugCode = "RS546",
                Price = 25.99,
                Stock = 20,
                NeedsPrescription = false
            };
            _drugRepositoryMock.Setup(s => s.GetFirst(It.IsAny<Func<Drug, bool>>())).Returns(drugInRepository);
            _drugRepositoryMock.Setup(m => m.Update(It.IsAny<Drug>())); ;

            Drug drugUpdated = _drugLogic.Update(drugId, drugToUpdate);

            Assert.AreEqual(drugToUpdate.Stock, drugUpdated.Stock);
            _drugRepositoryMock.VerifyAll();
        }
    }
}