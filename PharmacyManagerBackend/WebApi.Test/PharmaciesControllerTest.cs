using System.Collections.Generic;
using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Test.Utils;

namespace WebApi.Test
{
    [TestClass]
    public class PharmaciesControllerTest
    {
        private Mock<IPharmacyLogic> _pharmacyLogicMock;
        private PharmaciesController _pharmacyApiController;
        private Pharmacy _pharmacy;

        [TestInitialize]
        public void InitTest()
        {
            _pharmacyLogicMock = new Mock<IPharmacyLogic>(MockBehavior.Strict);
            _pharmacyApiController = new PharmaciesController(_pharmacyLogicMock.Object);
            _pharmacy = new Pharmacy()
            {
                Name = "Farmashop",
                Address = "Rivera 2030",
                Drugs = new List<Drug>()
            };
        }


        [TestMethod]
        public void CreatePharmacyOk()
        {
            _pharmacyLogicMock.Setup(m => m.Create(It.IsAny<Pharmacy>())).Returns(_pharmacy);
            var pharmacyModel = new PharmacyModel()
            {
                Name = "Farmashop",
                Address = "Rivera 2030",
            };


            var result = _pharmacyApiController.Create(pharmacyModel);
            var okResult = result as OkObjectResult;
            var createdPharmacy = okResult.Value as PharmacyModel;

            Assert.IsTrue(ModelsComparer.PharmacyCompare(pharmacyModel, createdPharmacy));
            _pharmacyLogicMock.VerifyAll();
        }


    }
}
