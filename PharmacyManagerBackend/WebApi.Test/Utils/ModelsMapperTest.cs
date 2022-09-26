using System;
using System.Collections.Generic;
using Domain;
using Domain.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Test.Utils;

[TestClass]
public class ModelsMapperTest
{

    [TestMethod]
    public void CredentialsDtoToCredentialsModelOk()
    {
        TokenDto token = new TokenDto
        {
            Token = Guid.NewGuid()
        };

        TokenModel tokenModel = ModelsMapper.ToModel(token);

        Assert.AreEqual(token.Token, tokenModel.Token);
    }

    [TestMethod]
    public void SessionRequestModelToSessionOK()
    {
        CredentialsModel credentialsModel = new CredentialsModel
        {
            UserName = "Cris",
            Password = "Cris.2022"
        };

        CredentialsDto credentialsDto = ModelsMapper.ToEntity(credentialsModel);

        Assert.AreEqual(credentialsModel.UserName, credentialsDto.UserName);
        Assert.AreEqual(credentialsModel.Password, credentialsDto.Password);
    }

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
            Items = purchaseItemModels
        };

        PurchaseDto purchaseDto = PurchaseModelsMapper.ToEntity(purchaseRequestModel);

        Assert.AreEqual(purchaseRequestModel.UserEmail, purchaseDto.UserEmail);
        Assert.AreEqual(purchaseRequestModel.Items[0].DrugCode, purchaseDto.Items[0].DrugCode);
        Assert.AreEqual(purchaseRequestModel.Items[0].Quantity, purchaseDto.Items[0].Quantity);
        Assert.AreEqual(purchaseRequestModel.Items[0].PharmacyName, purchaseDto.Items[0].PharmacyName);
    }


    [TestMethod]
    public void PurchaseDtoToModelOK()
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

        PurchaseResponseModel purchaseResponse = PurchaseModelsMapper.ToModel(purchaseDto);

        Assert.AreEqual(purchaseDto.Id, purchaseResponse.Id);
        Assert.AreEqual(purchaseDto.UserEmail, purchaseResponse.UserEmail);
        Assert.AreEqual(purchaseDto.CreatedDate, purchaseResponse.CreatedDate);
        Assert.AreEqual(purchaseDto.Items[0].DrugCode, purchaseResponse.Items[0].DrugCode);
        Assert.AreEqual(purchaseDto.Items[0].Quantity, purchaseResponse.Items[0].Quantity);
        Assert.AreEqual(purchaseDto.Items[0].PharmacyName, purchaseResponse.Items[0].PharmacyName);
    }

    [TestMethod]
    public void SolicitudeRequestModelToEntityOK()
    {

        SolicitudeItemModel solicitudeItemModel = new SolicitudeItemModel()
        {
            DrugQuantity = 3,
            DrugCode = "AG123",
        };
        List<SolicitudeItemModel> solicitudeItemModels = new List<SolicitudeItemModel>()
            {
               solicitudeItemModel,
            };
        SolicitudeRequestModel solicitudeRequestModel = new SolicitudeRequestModel()
        {
            SolicitudeItems = solicitudeItemModels
        };

        Solicitude solicitude = ModelsMapper.ToEntity(solicitudeRequestModel);

        Assert.AreEqual(solicitudeRequestModel.SolicitudeItems.Count, solicitude.Items.Count);
        Assert.AreEqual(solicitudeRequestModel.SolicitudeItems[0].DrugCode, solicitude.Items[0].DrugCode);
        Assert.AreEqual(solicitudeRequestModel.SolicitudeItems[0].DrugQuantity, solicitude.Items[0].DrugQuantity);
    }

    [TestMethod]
    public void SolicitudeToModelOK()
    {
        User userForTest = new User()
        {
            Id = 1,
            UserName = "Usuario1",
            Email = "ususario@user.com",
            Address = "Cuareim 123",
            Password = "Usuario+1",
            Role = new Role()
            {
                Name = "Employee"
            }
        };
        SolicitudeItem solicitudeItem = new SolicitudeItem()
        {
            DrugQuantity = 3,
            DrugCode = "AG123",
        };
        List<SolicitudeItem> solicitudeItems = new List<SolicitudeItem>()
            {
               solicitudeItem,
            };
        Solicitude solicitude = new Solicitude()
        {
            Id = 1,
            Date = DateTime.Now,
            State = State.PENDING,
            Employee = userForTest,
            Items = solicitudeItems,
        };

        SolicitudeResponseModel solicitudeResponseModel = ModelsMapper.ToModel(solicitude);

        Assert.AreEqual(solicitude.Id, solicitudeResponseModel.Id);
        Assert.AreEqual(solicitude.State, solicitudeResponseModel.State);
        Assert.AreEqual(solicitude.Date, solicitudeResponseModel.Date);
        Assert.AreEqual(solicitude.Employee.UserName, solicitudeResponseModel.EmployeeUserName);
    }

    [TestMethod]
    public void SolicitudesToModelListOK()
    {

        User userForTest = new User()
        {
            Id = 1,
            UserName = "Usuario1",
            Email = "ususario@user.com",
            Address = "Cuareim 123",
            Password = "Usuario+1",
            Role = new Role()
            {
                Name = "Employee"
            }
        };
        SolicitudeItem solicitudeItem1 = new SolicitudeItem()
        {
            DrugQuantity = 20,
            DrugCode = "XF324",
        };
        SolicitudeItem solicitudeItem2 = new SolicitudeItem()
        {
            DrugQuantity = 3,
            DrugCode = "AG123",
        };

        List<SolicitudeItem> solicitudeItems1and2 = new List<SolicitudeItem>()
            {
                solicitudeItem1, solicitudeItem2,
            };

        Solicitude solicitudeForList = new Solicitude()
        {
            Id = 1,
            State = State.PENDING,
            Date = DateTime.Now,
            Employee = userForTest,
            Items = solicitudeItems1and2

        };

        List<Solicitude> solicitudesToConvert = new List<Solicitude>()
            {
                solicitudeForList,
            };

        List<SolicitudeResponseModel> solicitudeResponseModels = ModelsMapper.ToModelList(solicitudesToConvert);

        Assert.AreEqual(solicitudesToConvert.Count, solicitudeResponseModels.Count);
        Assert.AreEqual(solicitudesToConvert[0].Id, solicitudeResponseModels[0].Id);
        Assert.AreEqual(solicitudesToConvert[0].State, solicitudeResponseModels[0].State);
        Assert.AreEqual(solicitudesToConvert[0].Date, solicitudeResponseModels[0].Date);
        Assert.AreEqual(solicitudesToConvert[0].Employee.UserName, solicitudeResponseModels[0].EmployeeUserName);

    }
}