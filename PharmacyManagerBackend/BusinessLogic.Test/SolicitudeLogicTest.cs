using Domain;
using Domain.Dtos;
using IDataAccess;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Test
{
        [TestClass]
    public class SolicitudeLogicTest
    {
        private SolicitudeLogic _solicitudeLogic;
        private Mock<ISolicitudeRepository> _solicitudeRepositoryMock;
        private User _userEmployeeForTest;
        private Solicitude _solicitudeForTest;

        [TestInitialize]
        public void Initialize()
        {
            this._solicitudeRepositoryMock = new Mock<ISolicitudeRepository>(MockBehavior.Strict);
            this._solicitudeLogic = new SolicitudeLogic(this._solicitudeRepositoryMock.Object);

            _userEmployeeForTest = new User()
            {
                Id = 1,
                UserName = "Usuario1",
                Email = "ususario@user.com",
                Address = "Cuareim 123",
                Password = "Usuario+1",
                Pharmacy = new Pharmacy()
                {
                    Id = 1,
                    Name = "Pharmashop"
                },
                Role = new Role()
                {
                    Name = "Employee"
                }
            };
            SolicitudeItem solicitudeItem3 = new SolicitudeItem()
            {
                DrugQuantity = 20,
                DrugCode = "XF324",
            };
            SolicitudeItem solicitudeItem4 = new SolicitudeItem()
            {
                DrugQuantity = 3,
                DrugCode = "AG123",
            };

            List<SolicitudeItem> solicitudeItems3and4 = new List<SolicitudeItem>()
            {
                solicitudeItem3, solicitudeItem4,
            };

            _solicitudeForTest = new Solicitude()
            {
                Id = 2,
                State = State.PENDING,
                Date = DateTime.Now,
                Employee = _userEmployeeForTest,
                Pharmacy = _userEmployeeForTest.Pharmacy,
                Items = solicitudeItems3and4
            };

        }

        [TestMethod]
        public void CreateNewSolicitudeOk()
        {
            SolicitudeItem solicitudeItem = new SolicitudeItem() 
            {
                DrugCode = "ABC123",
                DrugQuantity = 6
            };
            List <SolicitudeItem> solicitudeItems = new List<SolicitudeItem>() 
            { 
                solicitudeItem 
            };
            Solicitude solicitudeRepository = new Solicitude() 
            {
                Id = 1,
                State = State.PENDING,
                Date = DateTime.Now,
                Employee = _userEmployeeForTest,
                Pharmacy = _userEmployeeForTest.Pharmacy,
                Items = solicitudeItems,
            };
            Solicitude solicitudeToCreate = new Solicitude()
            {
                State = State.PENDING,
                Date = DateTime.Now,
                Employee = _userEmployeeForTest,
                Pharmacy = _userEmployeeForTest.Pharmacy,
                Items = solicitudeItems,
            };


            _solicitudeRepositoryMock.Setup(s => s.Create(solicitudeToCreate)).Returns(solicitudeRepository);

            Solicitude createdSolicitude = _solicitudeLogic.Create(solicitudeToCreate);


            Assert.AreEqual(solicitudeRepository.Id, createdSolicitude.Id);
            Assert.AreEqual(solicitudeRepository.State, createdSolicitude.State);
            Assert.AreEqual(solicitudeRepository.Date, createdSolicitude.Date);
            Assert.AreEqual(solicitudeRepository.Items, createdSolicitude.Items);
            Assert.AreEqual(solicitudeRepository.Employee.UserName, createdSolicitude.Employee.UserName);
            Assert.AreEqual(solicitudeRepository.Pharmacy, createdSolicitude.Pharmacy);
            CollectionAssert.AreEqual(solicitudeRepository.Items, createdSolicitude.Items);
            _solicitudeRepositoryMock.VerifyAll();
            
        }

        [TestMethod]
        public void TestGetSolicitudesForEmployee()
        {
            this._solicitudeLogic.SetContext(_userEmployeeForTest);

            QuerySolicitudeDto querysolicitudeDto = new QuerySolicitudeDto()
            {

            };

            SolicitudeItem solicitudeItem = new SolicitudeItem()
            {
                DrugCode = "ABC123",
                DrugQuantity = 6
            };
            List<SolicitudeItem> solicitudeItems = new List<SolicitudeItem>()
            {
                solicitudeItem
            };
            Solicitude solicitudeRepository = new Solicitude()
            {
                Id = 1,
                State = State.PENDING,
                Date = DateTime.Now,
                Employee = _userEmployeeForTest,
                Pharmacy = _userEmployeeForTest.Pharmacy,
                Items = solicitudeItems
            };

            List<Solicitude> solicitudesRepository = new List<Solicitude>()
            {
                solicitudeRepository, _solicitudeForTest
            };

            _solicitudeRepositoryMock.Setup(s => s.GetAll(It.IsAny<Func<Solicitude, bool>>())).Returns(solicitudesRepository);

           List <Solicitude> solicitudesReturned = _solicitudeLogic.GetSolicitudes(querysolicitudeDto).ToList();

            CollectionAssert.AreEqual(solicitudesRepository, solicitudesReturned);
            Assert.AreEqual(solicitudesRepository.Count, solicitudesReturned.Count);
            Assert.AreEqual(solicitudesRepository[0].Employee, solicitudesReturned[0].Employee);
            _solicitudeRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void TestGetSolicitudesForOwner()
        {
            User userOwnerForTest = new User()
            {
                Id = 3,
                UserName = "Usuario1",
                Email = "ususario@user.com",
                Address = "Cuareim 123",
                Password = "Usuario+1",
                Pharmacy = _userEmployeeForTest.Pharmacy,
                Role = new Role()
                {
                    Name = "Owner"
                }
            };

             this._solicitudeLogic.SetContext(userOwnerForTest);

            QuerySolicitudeDto querysolicitudeDto = new QuerySolicitudeDto()
            {

            };

            SolicitudeItem solicitudeItem = new SolicitudeItem()
            {
                DrugCode = "ABC123",
                DrugQuantity = 6
            };
            List<SolicitudeItem> solicitudeItems = new List<SolicitudeItem>()
            {
                solicitudeItem
            };
            Solicitude solicitudeRepository = new Solicitude()
            {
                Id = 1,
                State = State.PENDING,
                Date = DateTime.Now,
                Employee = _userEmployeeForTest,
                Pharmacy = _userEmployeeForTest.Pharmacy,
                Items = solicitudeItems
            };

            List<Solicitude> solicitudesRepository = new List<Solicitude>()
            {
                solicitudeRepository, _solicitudeForTest
            };

            _solicitudeRepositoryMock.Setup(s => s.GetAll(It.IsAny<Func<Solicitude, bool>>())).Returns(solicitudesRepository);

            List<Solicitude> solicitudesReturned = _solicitudeLogic.GetSolicitudes(querysolicitudeDto).ToList();

            CollectionAssert.AreEqual(solicitudesRepository, solicitudesReturned);
            _solicitudeRepositoryMock.VerifyAll();
        }
        [TestMethod]
        public void TestGetSolicitudesByState()
        {
            this._solicitudeLogic.SetContext(_userEmployeeForTest);

            QuerySolicitudeDto querysolicitudeDto = new QuerySolicitudeDto()
            {
               State = "PENDING",
            };

            SolicitudeItem solicitudeItem = new SolicitudeItem()
            {
                DrugCode = "ABC123",
                DrugQuantity = 6
            };
            List<SolicitudeItem> solicitudeItems = new List<SolicitudeItem>()
            {
                solicitudeItem
            };
            Solicitude solicitudeRepository = new Solicitude()
            {
                Id = 1,
                State = State.PENDING,
                Date = DateTime.Now,
                Employee = _userEmployeeForTest,
                Pharmacy = _userEmployeeForTest.Pharmacy,
                Items = solicitudeItems
            };

            List<Solicitude> solicitudesRepository = new List<Solicitude>()
            {
                solicitudeRepository, _solicitudeForTest
            };

            _solicitudeRepositoryMock.Setup(s => s.GetAll(It.IsAny<Func<Solicitude, bool>>())).Returns(solicitudesRepository);

            List<Solicitude> solicitudesReturned = _solicitudeLogic.GetSolicitudes(querysolicitudeDto).ToList();

            CollectionAssert.AreEqual(solicitudesRepository, solicitudesReturned);
            _solicitudeRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void TestGetSolicitudesByDrugCode()
        {
            this._solicitudeLogic.SetContext(_userEmployeeForTest);

            QuerySolicitudeDto querysolicitudeDto = new QuerySolicitudeDto()
            {
                DrugCode = "ABC123"
            };

            SolicitudeItem solicitudeItem = new SolicitudeItem()
            {
                DrugCode = "ABC123",
                DrugQuantity = 6
            };
            List<SolicitudeItem> solicitudeItems = new List<SolicitudeItem>()
            {
                solicitudeItem
            };
            Solicitude solicitudeRepository = new Solicitude()
            {
                Id = 1,
                State = State.PENDING,
                Date = DateTime.Now,
                Employee = _userEmployeeForTest,
                Pharmacy = _userEmployeeForTest.Pharmacy,
                Items = solicitudeItems
            };

            List<Solicitude> solicitudesRepository = new List<Solicitude>()
            {
                solicitudeRepository
            };

            _solicitudeRepositoryMock.Setup(s => s.GetAll(It.IsAny<Func<Solicitude, bool>>())).Returns(solicitudesRepository);

            List<Solicitude> solicitudesReturned = _solicitudeLogic.GetSolicitudes(querysolicitudeDto).ToList();
            CollectionAssert.AreEqual(solicitudesRepository, solicitudesReturned);
            _solicitudeRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void TestGetSolicitudesByDates()
        {
            this._solicitudeLogic.SetContext(_userEmployeeForTest);

            QuerySolicitudeDto querysolicitudeDto = new QuerySolicitudeDto()
            {
                DateFrom = "2020-05-08",
                DateTo = "2022-10-08"
            };

            SolicitudeItem solicitudeItem = new SolicitudeItem()
            {
                DrugCode = "ABC123",
                DrugQuantity = 6
            };
            List<SolicitudeItem> solicitudeItems = new List<SolicitudeItem>()
            {
                solicitudeItem
            };
            Solicitude solicitudeRepository = new Solicitude()
            {
                Id = 1,
                State = State.PENDING,
                Date = DateTime.Now,
                Employee = _userEmployeeForTest,
                Pharmacy = _userEmployeeForTest.Pharmacy,
                Items = solicitudeItems
            };

            List<Solicitude> solicitudesRepository = new List<Solicitude>()
            {
                solicitudeRepository
            };

            _solicitudeRepositoryMock.Setup(s => s.GetAll(It.IsAny<Func<Solicitude, bool>>())).Returns(solicitudesRepository);

            List<Solicitude> solicitudesReturned = _solicitudeLogic.GetSolicitudes(querysolicitudeDto).ToList();
            CollectionAssert.AreEqual(solicitudesRepository, solicitudesReturned);
            _solicitudeRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void TestUpdateSolicitude()
        {
            User userOwnerForTest = new User()
            {
                Id = 3,
                UserName = "Usuario1",
                Email = "ususario@user.com",
                Address = "Cuareim 123",
                Password = "Usuario+1",
                Pharmacy = _userEmployeeForTest.Pharmacy,
                Role = new Role()
                {
                    Name = "Owner"
                }
            };

            this._solicitudeLogic.SetContext(userOwnerForTest);

            SolicitudeItem solicitudeItem = new SolicitudeItem()
            {
                DrugCode = "ABC123",
                DrugQuantity = 6
            };
            List<SolicitudeItem> solicitudeItems = new List<SolicitudeItem>()
            {
                solicitudeItem
            };
            const int solicitudeId = 1;
            Solicitude solicitudeRepository = new Solicitude()
            {
                Id = solicitudeId,
                State = State.PENDING,
                Date = DateTime.Now,
                Employee = _userEmployeeForTest,
                Pharmacy = _userEmployeeForTest.Pharmacy,
                Items = solicitudeItems
            };

            List<Solicitude> solicitudesRepository = new List<Solicitude>()
            {
                solicitudeRepository, _solicitudeForTest
            };
            Solicitude solicitudeToUpdate = new Solicitude()
            {
                State = State.REJECTED
            };

            _solicitudeRepositoryMock.Setup(s=>s.GetFirst(It.IsAny<Func<Solicitude, bool>>())).Returns(solicitudeRepository);

            Solicitude solicitudeReturned = _solicitudeLogic.Update(solicitudeId, solicitudeToUpdate);

            Assert.AreEqual(solicitudeRepository, solicitudeReturned);
            _solicitudeRepositoryMock.VerifyAll();

        }
    }
}

