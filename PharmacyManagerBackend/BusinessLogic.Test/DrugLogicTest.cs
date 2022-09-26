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

        [TestInitialize]
        public void Initialize()
        {
            this._drugRepositoryMock = new Mock<IDrugRepository>(MockBehavior.Strict);
            this._drugInfoRepositoryMock = new Mock<IDrugInfoRepository>(MockBehavior.Strict);
            this._drugLogic = new DrugLogic(_drugRepositoryMock.Object, _drugInfoRepositoryMock.Object);
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
                DrugInfo = new DrugInfo()
            };

            _drugRepositoryMock.Setup(m => m.Create(It.IsAny<Drug>())).Returns(drug);
            _drugInfoRepositoryMock.Setup(m => m.Create(It.IsAny<DrugInfo>())).Returns(new DrugInfo());

            Drug createdDrug = _drugLogic.Create(drug);

            _drugRepositoryMock.VerifyAll();
        }

        [TestMethod]
        public void DeleteDrugOk()
        {
            Drug drug = new Drug()
            {
                Id = 1,
                DrugCode = "2a5678bx",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false
            };

            _drugRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Drug, bool>>())).Returns(drug);
            _drugRepositoryMock.Setup(m => m.Delete(drug));

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
    }
}