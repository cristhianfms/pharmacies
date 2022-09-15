using BusinessLogic;
using Domain;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Test.Utils;

namespace WebApi.Test
{
    [TestClass]
    public class DrugControllerTest
    {
        private Mock<DrugLogic> _drugLogicMock;
        private DrugController _drugApiController;
        private Drug _drug;

        [TestInitialize]
        public void InitTest()
        {
            _drugLogicMock = new Mock<DrugLogic>(MockBehavior.Strict);
            _drugApiController = new DrugController(_drugLogicMock.Object);
            _drug = new Drug()
            {
                Id = 1
            };
        }


        [TestMethod]
        public void CreateDrugOk()
        {
            _drugLogicMock.Setup(m => m.Create(It.IsAny<Drug>())).Returns(_drug);
            var drugModel = new DrugModel()
            {
               
            };


            var result = _drugApiController.Create(drugModel);
            var okResult = result as OkObjectResult;
            var createdDrug = okResult.Value as DrugModel;

            Assert.IsTrue(ModelsComparer.DrugCompare(drugModel, createdDrug));
            _drugLogicMock.VerifyAll();
        }
    }
}
