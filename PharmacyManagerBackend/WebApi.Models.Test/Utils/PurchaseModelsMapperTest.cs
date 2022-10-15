using System;
using System.Collections.Generic;
using Domain;
using Domain.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models;
using WebApi.Models.Utils;

namespace WebApi.Models.Test.Utils;

[TestClass]
public class PurchaseModelsMapperTest
{
    [TestMethod]
    public void PurchaseRequestModelToEntityOK()
    {
        List<PurchaseItemModel> purchaseItemModels = new List<PurchaseItemModel>(){
                new PurchaseItemModel(){
                    DrugCode = "A01",
                    Quantity = 1,
                    PharmacyName = "Pharamacy Name"
                }
            };
        PurchaseRequestModel purchaseRequestModel = new PurchaseRequestModel()
        {
            UserEmail = "email@email.com",
            Items = purchaseItemModels,
        };

        Purchase purchase = PurchaseModelsMapper.ToEntity(purchaseRequestModel);

        Assert.AreEqual(purchaseRequestModel.UserEmail, purchase.UserEmail);
        Assert.AreEqual(purchaseRequestModel.Items[0].PharmacyName, purchase.Items[0].Pharmacy.Name);
        Assert.AreEqual(purchaseRequestModel.Items[0].DrugCode, purchase.Items[0].Drug.DrugCode);
        Assert.AreEqual(purchaseRequestModel.Items[0].Quantity, purchase.Items[0].Quantity);
    }


    [TestMethod]
    public void PurchaseDtoToModelOK()
    {
        PurchaseItem purchaseItem = new PurchaseItem
        {
            Drug = new Drug()
            {
                DrugCode = "A01",
            },
            Quantity = 1,
            Pharmacy = new Pharmacy()
            {
                Name = "Pharamacy Name",
            }
        };
        List<PurchaseItem> purchaseItems = new List<PurchaseItem>() { purchaseItem };
        Purchase purchaseDto = new Purchase()
        {
            Id = 1,
            UserEmail = "email@email.com",
            Date = DateTime.Now,
            Items = purchaseItems,
        };

        PurchaseResponseModel purchaseResponse = PurchaseModelsMapper.ToModel(purchaseDto);

        Assert.AreEqual(purchaseDto.Id, purchaseResponse.Id);
        Assert.AreEqual(purchaseDto.UserEmail, purchaseResponse.UserEmail);
        Assert.AreEqual(purchaseDto.Items[0].Pharmacy.Name, purchaseResponse.Items[0].PharmacyName);
        Assert.AreEqual(purchaseDto.Date, purchaseResponse.CreatedDate);
        Assert.AreEqual(purchaseDto.Items[0].Drug.DrugCode, purchaseResponse.Items[0].DrugCode);
        Assert.AreEqual(purchaseDto.Items[0].Quantity, purchaseResponse.Items[0].Quantity);
    }

    [TestMethod]
    public void PurchaseReportDtoToModelOK()
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
        Purchase purchaseDto = new Purchase()
        {
            Id = 1,
            UserEmail = "email@email.com",
            Date = DateTime.Now,
            TotalPrice = 100.99,
            Items = purchaseItems,
        };
        List<Purchase> purchases = new List<Purchase>() { purchaseDto };
        PurchaseReportDto purchaseReportDto = new PurchaseReportDto
        {
            TotalPrice = 100.99,
            Purchases = purchases
        };

        PurchaseReportModel purchaseReportModel = PurchaseModelsMapper.ToModel(purchaseReportDto);

        Assert.AreEqual(purchaseReportDto.TotalPrice, purchaseReportModel.TotalPrice);
        Assert.AreEqual(purchaseDto.Id, purchaseReportModel.Purchases[0].Id);
        Assert.AreEqual(purchaseDto.UserEmail, purchaseReportModel.Purchases[0].UserEmail);
        Assert.AreEqual(purchaseDto.TotalPrice, purchaseReportModel.Purchases[0].Price);
        Assert.AreEqual(purchaseDto.Date, purchaseReportModel.Purchases[0].CreatedDate);
        Assert.AreEqual(purchaseDto.Items[0].Drug.DrugCode, purchaseReportModel.Purchases[0].Items[0].DrugCode);
        Assert.AreEqual(purchaseDto.Items[0].Quantity, purchaseReportModel.Purchases[0].Items[0].Quantity);
    }
}
