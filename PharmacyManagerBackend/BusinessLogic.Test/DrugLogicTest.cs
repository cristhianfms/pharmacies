using System;
using Domain;
using BusinessLogic;
using Exceptions;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogic.Test
{
    [TestClass]
    public class DrugLogicTest
    {
        private DrugLogic _drugLogic;
        private Mock<IDrugRepository> _drugRepository;

        [TestInitialize]
        public void Initialize()
        {
            this._drugRepository = new Mock<IDrugRepository>(MockBehavior.Strict);
            this._drugLogic = new DrugLogic(_drugRepository.Object);
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
                NeedsPrescription = false
            };
            Drug drugToCreate = new Drug()
            {
                DrugCode = "2a5678bx1",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false
            };

            _drugRepository.Setup(m => m.Create(It.IsAny<Drug>())).Returns(drug);
            
            Drug createdDrug = _drugLogic.Create(drugToCreate);

            Assert.AreEqual(drug.Id, createdDrug.Id);
            Assert.AreEqual(drug.DrugCode, createdDrug.DrugCode);
            Assert.AreEqual(drug.Stock, createdDrug.Stock);
            Assert.AreEqual(drug.Price, createdDrug.Price);
            Assert.AreEqual(drug.NeedsPrescription, createdDrug.NeedsPrescription);
            _drugRepository.VerifyAll();
        }

        [TestMethod]
        public void DeleteDrugOk()
        {
            Drug drug = new Drug()
            {
                Id = 1,
                DrugCode = "2a5678bx1",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false
            };

            _drugRepository.Setup(m => m.Delete(drug.Id));

            _drugLogic.Delete(drug.Id);

            _drugRepository.VerifyAll();
        }
    }
}
