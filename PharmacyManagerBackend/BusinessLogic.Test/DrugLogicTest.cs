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
        private Mock<UserLogic> _userLogic;
        private Mock<RoleLogic> _roleLogic;
        private Mock<PharmacyLogic> _pharmacyLogic;

        [TestInitialize]
        public void Initialize()
        {
            this._drugRepository = new Mock<IDrugRepository>(MockBehavior.Strict);
            this._drugLogic = new DrugLogic(_drugRepository.Object);
        }

        [TestMethod]
        public void CreateNewDrugOk()
        {
            Drug drugRepository = new Drug()
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

            _drugRepository.Setup(m => m.Create(It.IsAny<Drug>())).Returns(drugRepository);
            
            Drug createdDrug = _drugLogic.Create(drugToCreate);

            Assert.AreEqual(drugRepository.Id, createdDrug.Id);
            Assert.AreEqual(drugRepository.DrugCode, createdDrug.DrugCode);
            Assert.AreEqual(drugRepository.Stock, createdDrug.Stock);
            Assert.AreEqual(drugRepository.Price, createdDrug.Price);
            Assert.AreEqual(drugRepository.NeedsPrescription, createdDrug.NeedsPrescription);
            _drugRepository.VerifyAll();
        }
    }
}
