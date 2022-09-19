
using System;
using System.Collections.Generic;
using Domain;
using Domain.Dtos;
using IBusinessLogic;
using Microsoft.AspNetCore.Http;
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
        public void CreatePurchaseOk()
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

        [TestMethod]
        public void TestGetPurchaseReportByDate()
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
                Price = 100.99,
                Items = purchaseItems
            };
            List<PurchaseDto> purchases = new List<PurchaseDto>() { purchaseDto };
            PurchaseReportDto purchaseReportDto = new PurchaseReportDto
            {
                TotalPrice = 100.99,
                Purchases = purchases
            };
            QueryPurchaseDto queryPurchaseDto = new QueryPurchaseDto()
            {
                DateFrom = "2022-09-16",
                DateTo = "2022-10-16"
            };
            _purchaseLogicMock.Setup(m => m.GetPurchasesReport(queryPurchaseDto)).Returns(purchaseReportDto);

            var result = _purchasesApiController.GetPurchasesReport(queryPurchaseDto);
            var okResult = result as OkObjectResult;
            var responsePurchaseReports = okResult.Value as PurchaseReportModel;

            Assert.AreEqual(purchaseReportDto.TotalPrice, responsePurchaseReports.TotalPrice);
            Assert.AreEqual(purchaseDto.Id, responsePurchaseReports.Purchases[0].Id);
            Assert.AreEqual(purchaseDto.UserEmail, responsePurchaseReports.Purchases[0].UserEmail);
            Assert.AreEqual(purchaseDto.Price, responsePurchaseReports.Purchases[0].Price);
            Assert.AreEqual(purchaseDto.CreatedDate, responsePurchaseReports.Purchases[0].CreatedDate);
            Assert.AreEqual(purchaseDto.Items[0].DrugCode, responsePurchaseReports.Purchases[0].Items[0].DrugCode);
            Assert.AreEqual(purchaseDto.Items[0].Quantity, responsePurchaseReports.Purchases[0].Items[0].Quantity);
            Assert.AreEqual(purchaseDto.Items[0].PharmacyName, responsePurchaseReports.Purchases[0].Items[0].PharmacyName);
            _purchaseLogicMock.VerifyAll();
        }

    }
}
