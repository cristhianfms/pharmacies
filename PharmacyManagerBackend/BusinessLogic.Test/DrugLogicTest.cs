using System;
using Domain;
using Exceptions;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

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

            _drugRepository.Setup(m => m.Create(It.IsAny<Drug>())).Returns(drug);
            _drugRepository.Setup(m => m.GetFirst(It.IsAny<Func<Drug, bool>>())).Throws(new ResourceNotFoundException(""));

            Drug createdDrug = _drugLogic.Create(drug);

            _drugRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateRepetatedDrugFail()
        {
            Drug drug = new Drug()
            {
                Id = 1,
                DrugCode = "2a5678bx1",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false
            };

            _drugRepository.Setup(m => m.Create(It.IsAny<Drug>())).Returns(drug);
            _drugRepository.Setup(m=>m.GetFirst(It.IsAny<Func<Drug, bool>>())).Throws(new ValidationException(""));
            Drug createdDrug = _drugLogic.Create(drug);

            _drugRepository.VerifyAll();
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

            _drugRepository.Setup(m => m.Delete(drug.Id));

            _drugLogic.Delete(drug.Id);

            _drugRepository.VerifyAll();
        }

        [TestMethod]
        public void GetAllDrugsOk()
        {
            _drugRepository.Setup(m => m.GetAll(null)).Returns(new List<Drug>());

            _drugLogic.GetAllDrugs();

            _drugRepository.VerifyAll();
        }

        [TestMethod]
        public void GetDrugOk()
        {
            Drug drug = new Drug()
            {
                Id = 1,
                DrugCode = "2a5678bx",
                Price = 25.99,
                Stock = 15,
                NeedsPrescription = false
            };

            _drugRepository.Setup(m => m.GetFirst(It.IsAny<Func<Drug, bool>>())).Returns(drug);

            _drugLogic.GetDrug(drug);

            _drugRepository.VerifyAll();
        }
    }
}
