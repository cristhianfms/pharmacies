using System;
using System.Collections.Generic;
using Domain;
using Domain.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Test.Utils;

[TestClass]
public class PurchaseModelsMapperTest
{
    [TestMethod]
    public void PurchaseRequestModelToEntityOK()
    {
        List<PurchaseItemModel> purchaseItemModels = new List<PurchaseItemModel>(){
                new PurchaseItemModel(){
                    DrugCode = "A01",
                    Quantity = 1
                }
            };
        PurchaseRequestModel purchaseRequestModel = new PurchaseRequestModel()
        {
            UserEmail = "email@email.com",
            Items = purchaseItemModels,
            PharmacyName = "Pharamacy Name"
        };

        PurchaseDto purchaseDto = PurchaseModelsMapper.ToEntity(purchaseRequestModel);

        Assert.AreEqual(purchaseRequestModel.UserEmail, purchaseDto.UserEmail);
        Assert.AreEqual(purchaseRequestModel.PharmacyName, purchaseDto.PharmacyName);
        Assert.AreEqual(purchaseRequestModel.Items[0].DrugCode, purchaseDto.Items[0].DrugCode);
        Assert.AreEqual(purchaseRequestModel.Items[0].Quantity, purchaseDto.Items[0].Quantity);
    }


    [TestMethod]
    public void PurchaseDtoToModelOK()
    {
        PurchaseItemDto purchaseItemDto = new PurchaseItemDto
        {
            DrugCode = "A01",
            Quantity = 1
        };
        List<PurchaseItemDto> purchaseItems = new List<PurchaseItemDto>() { purchaseItemDto };
        PurchaseDto purchaseDto = new PurchaseDto()
        {
            Id = 1,
            UserEmail = "email@email.com",
            CreatedDate = DateTime.Now,
            Items = purchaseItems,
            PharmacyName = "Pharamacy Name"
        };

        PurchaseResponseModel purchaseResponse = PurchaseModelsMapper.ToModel(purchaseDto);

        Assert.AreEqual(purchaseDto.Id, purchaseResponse.Id);
        Assert.AreEqual(purchaseDto.UserEmail, purchaseResponse.UserEmail);
        Assert.AreEqual(purchaseDto.PharmacyName, purchaseResponse.PharmacyName);
        Assert.AreEqual(purchaseDto.CreatedDate, purchaseResponse.CreatedDate);
        Assert.AreEqual(purchaseDto.Items[0].DrugCode, purchaseResponse.Items[0].DrugCode);
        Assert.AreEqual(purchaseDto.Items[0].Quantity, purchaseResponse.Items[0].Quantity);
    }

    [TestMethod]
    public void PurchaseReportDtoToModelOK()
    {
        PurchaseItemDto purchaseItemDto = new PurchaseItemDto
        {
            DrugCode = "A01",
            Quantity = 1
        };
        List<PurchaseItemDto> purchaseItems = new List<PurchaseItemDto>() { purchaseItemDto };
        PurchaseDto purchaseDto = new PurchaseDto()
        {
            Id = 1,
            UserEmail = "email@email.com",
            CreatedDate = DateTime.Now,
            Price = 100.99,
            Items = purchaseItems,
            PharmacyName = "Pharamacy Name"
        };
        List<PurchaseDto> purchases = new List<PurchaseDto>() { purchaseDto };
        PurchaseReportDto purchaseReportDto = new PurchaseReportDto
        {
            TotalPrice = 100.99,
            Purchases = purchases
        };

        PurchaseReportModel purchaseReportModel = PurchaseModelsMapper.ToModel(purchaseReportDto);

        Assert.AreEqual(purchaseReportDto.TotalPrice, purchaseReportModel.TotalPrice);
        Assert.AreEqual(purchaseDto.Id, purchaseReportModel.Purchases[0].Id);
        Assert.AreEqual(purchaseDto.UserEmail, purchaseReportModel.Purchases[0].UserEmail);
        Assert.AreEqual(purchaseDto.Price, purchaseReportModel.Purchases[0].Price);
        Assert.AreEqual(purchaseDto.CreatedDate, purchaseReportModel.Purchases[0].CreatedDate);
        Assert.AreEqual(purchaseDto.Items[0].DrugCode, purchaseReportModel.Purchases[0].Items[0].DrugCode);
        Assert.AreEqual(purchaseDto.Items[0].Quantity, purchaseReportModel.Purchases[0].Items[0].Quantity);
    }
}
