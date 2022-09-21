using Domain;
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
        private Mock<IBaseRepository<Solicitude>> _solicitudeRepositoryMock;
        private User _userForTest;

        [TestInitialize]
        public void Initialize()
        {
            this._solicitudeRepositoryMock = new Mock<IBaseRepository<Solicitude>>(MockBehavior.Strict);
            this._solicitudeLogic = new SolicitudeLogic(this._solicitudeRepositoryMock.Object);

            _userForTest = new User()
            {
                Id = 1,
                UserName = "Usuario1",
                Email = "ususario@user.com",
                Address = "Cuareim 123",
                Password = "Usuario+1",
                Pharmacy = new Pharmacy()
                {
                    Name = "Pharmashop"
                },
                Role = new Role()
                {
                    Name = "Employee"
                }
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
                Items = solicitudeItems,
                Employee = _userForTest
            };
            Solicitude solicitudeToCreate = new Solicitude()
            {
                State = State.PENDING,
                Date = DateTime.Now,
                Items = solicitudeItems,
                Employee = _userForTest
            };


            _solicitudeRepositoryMock.Setup(s => s.Create(solicitudeToCreate)).Returns(solicitudeRepository);

            Solicitude createdSolicitude = _solicitudeLogic.Create(solicitudeToCreate);


            Assert.AreEqual(solicitudeRepository.Id, createdSolicitude.Id);
            Assert.AreEqual(solicitudeRepository.State, createdSolicitude.State);
            Assert.AreEqual(solicitudeRepository.Date, createdSolicitude.Date);
            Assert.AreEqual(solicitudeRepository.Items, createdSolicitude.Items);
            Assert.AreEqual(solicitudeRepository.Employee.UserName, createdSolicitude.Employee.UserName);
            CollectionAssert.AreEqual(solicitudeRepository.Items, createdSolicitude.Items);
            _solicitudeRepositoryMock.VerifyAll();
            
        }

    }
}
