using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApi.Test
{
        [TestClass]
    public class SolicitudesControllerTest
    {
        private Mock<ISolicitudeLogic> _solicitudeLogicMock;
        private SolicitudesController _solicitudeApiController;

        [TestInitialize]
        public void InitTest()
        {
            _solicitudeLogicMock = new Mock<ISolicitudeLogic>(MockBehavior.Strict);
            _solicitudeApiController = new SolicitudesController(_solicitudeLogicMock.Object);
        }


        [TestMethod]
        public void CreateSolicitudeOk()
        {

             SolicitudeItem solicitudeItem = new SolicitudeItem()
            {
                DrugQuantity = 20,
                DrugCode = "XF324",
            };

            List<SolicitudeItem> solicitudeItems = new List<SolicitudeItem>()
            {
                solicitudeItem,
            };
              
            Solicitude solicitude = new Solicitude()
            {
                Id = 1,
                State = State.PENDING,
                Date = DateTime.Now,
                Items = solicitudeItems

            };

            SolicitudeItemModel solicitudeItemModel = new SolicitudeItemModel()
            {
                DrugCode = solicitudeItem.DrugCode,
                DrugQuantity = solicitudeItem.DrugQuantity
            };



            List<SolicitudeItemModel> solicitudeItemModels = new List<SolicitudeItemModel>() 
            {
               solicitudeItemModel,
            };
                        
            SolicitudeRequestModel solicitudeRequestModel = new SolicitudeRequestModel()
            {
                SolicitudeItems = solicitudeItemModels
            };

            _solicitudeLogicMock.Setup(m => m.Create(It.IsAny<Solicitude>())).Returns(solicitude);


            var result = _solicitudeApiController.Create(solicitudeRequestModel);
            var okResult = result as OkObjectResult;
            var createdSolicitude = okResult.Value as SolicitudeResponseModel;


            Assert.AreEqual(solicitude.Id, createdSolicitude.Id);
            Assert.AreEqual(solicitude.State, createdSolicitude.State);
            Assert.AreEqual(solicitude.Date, createdSolicitude.Date);
            Assert.AreEqual(solicitude.Items[0].DrugCode, createdSolicitude.SolicitudeItems[0].DrugCode);
            Assert.AreEqual(solicitude.Items[0].DrugQuantity, createdSolicitude.SolicitudeItems[0].DrugQuantity);
            _solicitudeLogicMock.VerifyAll();
        }

    }
}
