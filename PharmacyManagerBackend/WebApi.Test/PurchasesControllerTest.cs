using Domain;
using Domain.Dtos;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Models.Utils;

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
            Id = 1,
            Drug = new Drug()
            {
                DrugCode = "A01",
                Id = 1,
                DrugInfo = new DrugInfo { Name = "Perifar" }

            },
            Quantity = 1,
            Pharmacy = new Pharmacy()
            {
                Id = 1,
                Name = "Pharamacy Name"
            },
            State =PurchaseState.PENDING,
        };
        List<PurchaseItem> purchaseItems = new List<PurchaseItem>() { 
            purchaseItem };
        Purchase purchase = new Purchase()
        {
            Id = 1,
            UserEmail = "email@email.com",
            Date = new DateTime(2022-10-14),
            TotalPrice = 100.99,
            Items = purchaseItems,
        };
        PurchaseItemReportDto purchaseReportItem = new PurchaseItemReportDto
        {
            Name = "A01 - Perifar",
            Quantity = 1,
            Amount = 100.99
        };
        List<PurchaseItemReportDto> purchases = new List<PurchaseItemReportDto>() { purchaseReportItem };
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
        Assert.AreEqual(purchase.Items[0].Drug.DrugCode + " - " + purchase.Items[0].Drug.DrugInfo.Name, responsePurchaseReports.Purchases[0].Name);
        Assert.AreEqual(purchase.TotalPrice, responsePurchaseReports.Purchases[0].Amount);
        Assert.AreEqual(purchase.Items[0].Quantity, responsePurchaseReports.Purchases[0].Quantity);
        _purchaseLogicMock.VerifyAll();
    }

    [TestMethod]
    public void UpdatePurchaseOk()
    {
        int purchaseId = 1;
        PurchaseItem purchaseItem = new PurchaseItem
        {   
            Id = 1,
            Drug = new Drug()
            {
                Id = 1,
                DrugCode = "A01"
            },
            Quantity = 1,
            Pharmacy = new Pharmacy()
            { 
                Id = 1,
                Name = "Pharamacy Name"
            },
            State = PurchaseState.ACCEPTED
        };
        List<PurchaseItem> purchaseItems = new List<PurchaseItem>() { purchaseItem };
        Purchase purchase = new Purchase()
        {
            Id = 1,
            UserEmail = "email@email.com",
            Date = DateTime.Now,
            TotalPrice = 100.99,
            Items = purchaseItems,
            Code = "1234"
        };
        _purchaseLogicMock.Setup(m => m.Update(It.IsAny<int>(), It.IsAny<Purchase>())).Returns(purchase);
        var purchasePutModel = new PurchasePutModel()
        {
            Items = new List<PurchaseItemPutModel>()
            {
                new PurchaseItemPutModel()
                {
                    State = PurchaseState.ACCEPTED,
                    DrugCode = "A01"
                }
            }
        };
        var purchaseModelExpected = PurchaseModelsMapper.ToModel(purchase);

        var result = _purchasesApiController.Update(purchaseId, purchasePutModel);
        var okResult = result as OkObjectResult;
        var purchaseUpdatedModel = okResult.Value as PurchaseResponseModel;

        Assert.AreEqual(purchaseModelExpected, purchaseUpdatedModel);
        CollectionAssert.AreEqual(purchaseModelExpected.Items, purchaseUpdatedModel.Items);
        _purchaseLogicMock.VerifyAll();
    }
    
    
    [TestMethod]
    public void GetPurchaseOk()
    {
        int purchaseId = 1;
        PurchaseItem purchaseItem = new PurchaseItem
        {   
            Id = 1,
            Drug = new Drug()
            {
                Id = 1,
                DrugCode = "A01"
            },
            Quantity = 1,
            Pharmacy = new Pharmacy()
            { 
                Id = 1,
                Name = "Pharamacy Name"
            },
            State = PurchaseState.ACCEPTED
        };
        List<PurchaseItem> purchaseItems = new List<PurchaseItem>() { purchaseItem };
        Purchase purchase = new Purchase()
        {
            Id = 1,
            UserEmail = "email@email.com",
            Date = DateTime.Now,
            TotalPrice = 100.99,
            Items = purchaseItems,
            Code = "1234"
        };
        _purchaseLogicMock.Setup(m => m.Get(It.IsAny<string>())).Returns(purchase);
        var purchaseModelExpected = PurchaseModelsMapper.ToModel(purchase);

        var result = _purchasesApiController.GetPurchase("1234");
        var okResult = result as OkObjectResult;
        var purchaseUpdatedModel = okResult.Value as PurchaseResponseModel;

        Assert.AreEqual(purchaseModelExpected, purchaseUpdatedModel);
        CollectionAssert.AreEqual(purchaseModelExpected.Items, purchaseUpdatedModel.Items);
        _purchaseLogicMock.VerifyAll();
    }
    
    [TestMethod]
    public void GetAllPurchasesOk()
    {
        int purchaseId = 1;
        PurchaseItem purchaseItem = new PurchaseItem
        {   
            Id = 1,
            Drug = new Drug()
            {
                Id = 1,
                DrugCode = "A01"
            },
            Quantity = 1,
            Pharmacy = new Pharmacy()
            { 
                Id = 1,
                Name = "Pharamacy Name"
            },
            State = PurchaseState.ACCEPTED
        };
        List<PurchaseItem> purchaseItems = new List<PurchaseItem>() { purchaseItem };
        Purchase purchase = new Purchase()
        {
            Id = 1,
            UserEmail = "email@email.com",
            Date = DateTime.Now,
            TotalPrice = 100.99,
            Items = purchaseItems,
            Code = "1234"
        };
        List<Purchase> purchases = new List<Purchase>()
        {
            purchase
        };
        _purchaseLogicMock.Setup(m => m.GetAll()).Returns(purchases);
        var purchaseModelExpected = PurchaseModelsMapper.ToModelList(purchases).ToList();

        var result = _purchasesApiController.GetAllPurchases();
        var okResult = result as OkObjectResult;
        var purchaseUpdatedModel = okResult.Value as List<PurchaseResponseModel>;
            
        CollectionAssert.AreEqual(purchaseModelExpected, purchaseUpdatedModel);
        _purchaseLogicMock.VerifyAll();
    }
}

