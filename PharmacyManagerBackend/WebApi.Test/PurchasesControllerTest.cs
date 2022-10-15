using Domain;
using Domain.Dtos;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Models;
namespace WebApi.Test;

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
        PurchaseItem purchaseItem = new PurchaseItem
        {
            Drug = new Drug()
            {
                DrugCode = "A01"
            },
            Quantity = 1,
            Pharmacy = new Pharmacy()
            {
                Name = "PharamacyName"   
            }
        };
        List<PurchaseItem> purchaseItems = new List<PurchaseItem>() { purchaseItem };
        Purchase purchase = new Purchase()
        {
            Id = 1,
            UserEmail = "email@email.com",
            Date = DateTime.Now,
            Items = purchaseItems,
        };
        List<PurchaseItemModel> purchaseItemModels = new List<PurchaseItemModel>(){
                new PurchaseItemModel(){
                    DrugCode = purchaseItem.Drug.DrugCode,
                    Quantity = purchaseItem.Quantity,
                    PharmacyName = purchaseItem.Pharmacy.Name
                }
            };
        PurchaseRequestModel purchaseRequestModel = new PurchaseRequestModel()
        {
            UserEmail = purchase.UserEmail,
            Items = purchaseItemModels,
        };

        _purchaseLogicMock.Setup(m => m.Create(It.IsAny<Purchase>())).Returns(purchase);

        var result = _purchasesApiController.Create(purchaseRequestModel);
        var okResult = result as OkObjectResult;
        var createdPurchase = okResult.Value as PurchaseResponseModel;

        Assert.AreEqual(purchase.Id, createdPurchase.Id);
        Assert.AreEqual(purchase.UserEmail, createdPurchase.UserEmail);
        Assert.AreEqual(purchase.Items[0].Pharmacy.Name, createdPurchase.Items[0].PharmacyName);
        Assert.AreEqual(purchase.Date, createdPurchase.CreatedDate);
        Assert.AreEqual(purchase.Items[0].Drug.DrugCode, createdPurchase.Items[0].DrugCode);
        Assert.AreEqual(purchase.Items[0].Quantity, createdPurchase.Items[0].Quantity);
        _purchaseLogicMock.VerifyAll();
    }

    [TestMethod]
    public void TestGetPurchaseReportByDate()
    {
        PurchaseItem purchaseItem = new PurchaseItem
        {
            Drug = new Drug()
            {
                DrugCode = "A01"
            },
            Quantity = 1,
            Pharmacy = new Pharmacy()
            {
                Name = "Pharamacy Name"
            }
        };
        List<PurchaseItem> purchaseItems = new List<PurchaseItem>() { purchaseItem };
        Purchase purchase = new Purchase()
        {
            Id = 1,
            UserEmail = "email@email.com",
            Date = DateTime.Now,
            TotalPrice = 100.99,
            Items = purchaseItems,
        };
        List<Purchase> purchases = new List<Purchase>() { purchase };
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
        Assert.AreEqual(purchase.Id, responsePurchaseReports.Purchases[0].Id);
        Assert.AreEqual(purchase.UserEmail, responsePurchaseReports.Purchases[0].UserEmail);
        Assert.AreEqual(purchase.TotalPrice, responsePurchaseReports.Purchases[0].Price);
        Assert.AreEqual(purchase.Date, responsePurchaseReports.Purchases[0].CreatedDate);
        Assert.AreEqual(purchase.Items[0].Drug.DrugCode, responsePurchaseReports.Purchases[0].Items[0].DrugCode);
        Assert.AreEqual(purchase.Items[0].Quantity, responsePurchaseReports.Purchases[0].Items[0].Quantity);
        _purchaseLogicMock.VerifyAll();
    }

}

