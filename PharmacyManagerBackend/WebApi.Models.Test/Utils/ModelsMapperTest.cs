using System;
using System.Collections.Generic;
using Domain;
using Domain.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models;
using WebApi.Models.Utils;

namespace WebApi.Models.Test.Utils;

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

    [TestMethod]
    public void DrugToModelOK()
    {
        DrugInfo drugInfo = new DrugInfo
        {
            Id = 1,
            Name = "Perifar Flex",
            Symptoms = "Dolor de cabeza",
            Presentation = "Blister",
            QuantityPerPresentation = 8,
            UnitOfMeasurement = "Gramos"
        };

        Drug drug = new Drug
        {
            Id = 1,
            DrugCode = "2A5678BX",
            DrugInfo = drugInfo,
            NeedsPrescription = false,
            Price = 25.5
        };

        DrugModel drugModel = ModelsMapper.ToModel(drug);

        Assert.AreEqual(drugModel.Id, drug.Id);
        Assert.AreEqual(drugModel.DrugCode, drug.DrugCode);
        Assert.AreEqual(drugModel.Name, drugInfo.Name);
        Assert.AreEqual(drugModel.Price, drug.Price);
        Assert.AreEqual(drugModel.NeedsPrescription, drug.NeedsPrescription);
        Assert.AreEqual(drugModel.Presentation, drugInfo.Presentation);
        Assert.AreEqual(drugModel.QuantityPerPresentation, drugInfo.QuantityPerPresentation);
        Assert.AreEqual(drugModel.UnitOfMeasurement, drugInfo.UnitOfMeasurement);
        Assert.AreEqual(drugModel.Symptoms, drugInfo.Symptoms);
    }

    [TestMethod]
    public void DrugModelToEntityOK()
    {
        DrugModel drugModel = new DrugModel
        {
            Id = 1,
            Name = "Perifar Flex",
            Symptoms = "Dolor de cabeza",
            Presentation = "Blister",
            QuantityPerPresentation = 8,
            UnitOfMeasurement = "Gramos",        
            DrugCode = "2A5678BX",
            NeedsPrescription = false,
            Price = 25.5
        };

        Drug drug = ModelsMapper.ToEntity(drugModel);

        Assert.AreEqual(drugModel.Id, drug.Id);
        Assert.AreEqual(drugModel.DrugCode, drug.DrugCode);
        Assert.AreEqual(drugModel.Name, drug.DrugInfo.Name);
        Assert.AreEqual(drugModel.Price, drug.Price);
        Assert.AreEqual(drugModel.NeedsPrescription, drug.NeedsPrescription);
        Assert.AreEqual(drugModel.Presentation, drug.DrugInfo.Presentation);
        Assert.AreEqual(drugModel.QuantityPerPresentation, drug.DrugInfo.QuantityPerPresentation);
        Assert.AreEqual(drugModel.UnitOfMeasurement, drug.DrugInfo.UnitOfMeasurement);
        Assert.AreEqual(drugModel.Symptoms, drug.DrugInfo.Symptoms);
    }
}