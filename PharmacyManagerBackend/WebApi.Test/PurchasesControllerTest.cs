
using System;
using System.Collections.Generic;
using Domain;
using Domain.Dtos;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Test.Utils;
using WebApi.Utils;

namespace WebApi.Test
{
    [TestClass]
    public class PurchasesControllerTest
    {
        private Mock<IPurchaseLogic> _purchaseLogicMock;
        private PurchasesController _purchasesApiController;

        [TestInitialize]
        public void InitTest()
        {
            _purchaseLogicMock = new Mock<IPurchaseLogic>(MockBehavior.Strict);
            _purchasesApiController = new PurchasesController(_purchaseLogicMock.Object);
        }

        [TestMethod]
        public void CreateUserOk()
        {
            PurchaseItemDto purchaseItemDto = new PurchaseItemDto
            {
                DrugCode = "A01",
                Quantity = 1,
                PharmacyName = "Pharamacy Name"
            };
            List<PurchaseItemDto> purchaseItems = new List<PurchaseItemDto>() { purchaseItemDto };
            PurchaseDto purchaseDto = new PurchaseDto()
            {
                Id = 1,
                UserEmail = "email@email.com",
                CreatedDate = DateTime.Now,
                Items = purchaseItems
            };
            List<PurchaseItemModel> purchaseItemModels = new List<PurchaseItemModel>(){
                new PurchaseItemModel(){
                    DrugCode = purchaseItemDto.DrugCode,
                    Quantity = purchaseItemDto.Quantity,
                    PharmacyName = purchaseItemDto.PharmacyName,
                }
            };
            PurchaseRequestModel purchaseRequestModel = new PurchaseRequestModel()
            {
                UserEmail = purchaseDto.UserEmail,
                Items = purchaseItemModels
            };

            _purchaseLogicMock.Setup(m => m.Create(It.IsAny<PurchaseDto>())).Returns(purchaseDto);

            var result = _purchasesApiController.Create(purchaseRequestModel);
            var okResult = result as OkObjectResult;
            var createdPurchase = okResult.Value as PurchaseResponseModel;

            Assert.AreEqual(purchaseDto.Id, createdPurchase.Id);
            Assert.AreEqual(purchaseDto.UserEmail, createdPurchase.UserEmail);
            Assert.AreEqual(purchaseDto.CreatedDate, createdPurchase.CreatedDate);
            Assert.AreEqual(purchaseDto.Items[0].DrugCode, createdPurchase.Items[0].DrugCode);
            Assert.AreEqual(purchaseDto.Items[0].Quantity, createdPurchase.Items[0].Quantity);
            Assert.AreEqual(purchaseDto.Items[0].PharmacyName, createdPurchase.Items[0].PharmacyName);
            _purchaseLogicMock.VerifyAll();
        }

    }
}
