using System;
using Domain;
using Exceptions;
using IDataAccess;
using Moq;
using Domain.Dtos;

namespace BusinessLogic.Test
{
    [TestClass]
    public class DrugLogicTest
    {
        private DrugLogic _drugLogic;
        private Mock<IDrugRepository> _drugRepositoryMock;
        private Mock<IDrugInfoRepository> _drugInfoRepositoryMock;
        private Mock<PharmacyLogic> _pharmacyLogic;
        private Mock<SolicitudeLogic> _solicitudeLogic;
        private Mock<PurchaseLogic> _purchaseLogic;
        private Mock<Context> _context;
        private User _userEmployeeForTest;
        private Pharmacy _pharmacy;

        [TestInitialize]
        public void Initialize()
        {
            this._context = new Mock<Context>(MockBehavior.Strict);
            this._drugRepositoryMock = new Mock<IDrugRepository>(MockBehavior.Strict);
            this._drugInfoRepositoryMock = new Mock<IDrugInfoRepository>(MockBehavior.Strict);
            this._pharmacyLogic = new Mock<PharmacyLogic>(MockBehavior.Strict, null);
            this._purchaseLogic = new Mock<PurchaseLogic>(MockBehavior.Strict, null, null, null, null);
            this._solicitudeLogic = new Mock<SolicitudeLogic>(MockBehavior.Strict, null, null, null, null);
            this._drugLogic = new DrugLogic(_drugRepositoryMock.Object,
                _drugInfoRepositoryMock.Object,
                _pharmacyLogic.Object,
                _context.Object, _purchaseLogic.Object,
                _solicitudeLogic.Object);

            _pharmacy = new Pharmacy()
            {
                Id = 1,
                Name = "Pharmashop",
                Drugs = new List<Drug>(),
                Employees = new List<User>()
            };

            _userEmployeeForTest = new User()
            {
                Id = 1,
                UserName = "Usuario1",
                Email = "ususario@user.com",
                Address = "Cuareim 123",
                Password = "Usuario+1EsLacontraseña*",
                EmployeePharmacy = _pharmacy,
                Role = new Role()
                {
                    Name = "Employee"
                }
            };

            _context.Setup(m => m.CurrentUser).Returns(_userEmployeeForTest);
        }

        [TestMethod]
        public void CreateNewDrugOk()
        {
            Drug drug = new Drug()
            {
                Id = 1,
                DrugCode = "2a5678bx1",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false,
                DrugInfo = new DrugInfo(),
                PharmacyId = _pharmacy.Id,
            };

            _pharmacy.Employees.Add(_userEmployeeForTest);

            _pharmacyLogic.Setup(m => m.GetPharmacyByName(_pharmacy.Name)).Returns(_pharmacy);
            _drugRepositoryMock.Setup(m => m.Create(It.IsAny<Drug>())).Returns(drug);
            _drugInfoRepositoryMock.Setup(m => m.Create(It.IsAny<DrugInfo>())).Returns(new DrugInfo());
            _pharmacyLogic.Setup(m => m.UpdatePharmacy(It.IsAny<Pharmacy>()));

            Drug createdDrug = _drugLogic.Create(drug);

            _drugRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateNewDrugRepeatedPharmacy()
        {
            Drug drug = new Drug()
            {
                Id = 1,
                DrugCode = "2a5678bx1",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false,
                DrugInfo = new DrugInfo(),
                PharmacyId = _pharmacy.Id
            };

            _pharmacy.Drugs.Add(drug);
            _pharmacyLogic.Setup(m => m.GetPharmacyByName(_pharmacy.Name)).Returns(_pharmacy);
            _drugRepositoryMock.Setup(m => m.Create(It.IsAny<Drug>())).Returns(drug);
            _drugInfoRepositoryMock.Setup(m => m.Create(It.IsAny<DrugInfo>())).Returns(new DrugInfo());
            _pharmacyLogic.Setup(m => m.UpdatePharmacy(It.IsAny<Pharmacy>()));

            Drug createdDrug = _drugLogic.Create(drug);
            _drugLogic.Create(drug);

            _drugRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteDrugOk()
        {
            DrugInfo drugInfo = new DrugInfo();


            Drug drug = new Drug()
            {
                Id = 1,
                DrugCode = "2a5678bx",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false,
                DrugInfo = drugInfo,
                PharmacyId = _pharmacy.Id
            };

            _pharmacyLogic.Setup(m => m.GetPharmacyByName(_pharmacy.Name)).Returns(_pharmacy);
            _drugRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Drug, bool>>())).Returns(drug);
            _solicitudeLogic.Setup(m => m.DrugExistsInSolicitude(drug));
            _purchaseLogic.Setup(m => m.DrugExistsInPurchase(drug));
            _pharmacyLogic.Setup(m => m.UpdatePharmacy(It.IsAny<Pharmacy>()));
            _drugInfoRepositoryMock.Setup(m => m.Delete(It.IsAny<DrugInfo>()));
            _drugInfoRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<DrugInfo, bool>>())).Returns(drugInfo);
            _drugLogic.Delete(drug.Id);

            _drugRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void GetAllDrugsOk()
        {
            DrugInfo drugInfo = new DrugInfo();


            Drug drug = new Drug()
            {
                Id = 1,
                DrugCode = "2a5678bx",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false,
                DrugInfo = drugInfo,
                PharmacyId = _pharmacy.Id
            };

            List<Drug> drugs = new List<Drug>();
            drugs.Add(drug);
            _drugRepositoryMock.Setup(m => m.GetAll(It.IsAny<Func<Drug, bool>>())).Returns(drugs);
            _drugLogic.GetAll(new QueryDrugDto());

            _drugRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void GetDrugOk()
        {
            DrugInfo drugInfo = new DrugInfo();


            Drug drug = new Drug()
            {
                Id = 1,
                DrugCode = "2a5678bx",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false,
                DrugInfo = drugInfo,
                PharmacyId = _pharmacy.Id
            };

            _drugRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Drug, bool>>())).Returns(drug);
            _drugLogic.Get(drug.Id);

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