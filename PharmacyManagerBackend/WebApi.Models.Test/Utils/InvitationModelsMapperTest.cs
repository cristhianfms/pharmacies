using System;
using System.Collections.Generic;
using Domain;
using Domain.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models;
using WebApi.Models.Utils;

namespace WebApi.Models.Test.Utils;

[TestClass]
public class InvitationModelsMapperTest
{
    [TestMethod]
    public void InvitationModelToEntityOK()
    {
        var invitationModel = new InvitationModel()
        {
            Id = 1,
            UserName = "JuanPerez",
            RoleName = "Employee",
            InvitationCode = "2A5678BX",
            PharmacyName = "FarmaciaB"
        };

        InvitationDto invitationDto = InvitationModelsMapper.ToEntity(invitationModel);

        Assert.AreEqual(invitationModel.UserName, invitationDto.UserName);
        Assert.AreEqual(invitationModel.RoleName, invitationDto.RoleName);
        Assert.AreEqual(invitationModel.InvitationCode, invitationDto.Code);
        Assert.AreEqual(invitationModel.PharmacyName, invitationDto.PharmacyName);
    }


    [TestMethod]
    public void InvitationToModelOK()
    {
        var invitation = new Invitation()
        {
            Id = 1,
            UserName = "JuanPerez",
            Role = new Role()
            {
                Name = "Employee"
            },
            Pharmacy = new Pharmacy()
            {
                Name = "FarmaciaB"
            },
            Code = "2A5678BX"
        };

        InvitationModel invitationModel = InvitationModelsMapper.ToModel(invitation);

        Assert.AreEqual(invitation.Id, invitationModel.Id);
        Assert.AreEqual(invitation.UserName, invitationModel.UserName);
        Assert.AreEqual(invitation.Role.Name, invitationModel.RoleName);
        Assert.AreEqual(invitation.Pharmacy.Name, invitationModel.PharmacyName);
        Assert.AreEqual(invitation.Code, invitationModel.InvitationCode);
    }

    [TestMethod]
    public void InvitationPutModelToEntityOK()
    {
        var invitationPutModel = new InvitationPutModel()
        {
            UserName = "JuanPerez",
            InvitationCode = "2A5678BX",
            Email = "Juan@email.com",
            Address = "Road A 1234",
            Password = "12345678"
        };

        InvitationDto invitationDto = InvitationModelsMapper.ToEntity(invitationPutModel);

        Assert.AreEqual(invitationDto.UserName, invitationPutModel.UserName);
        Assert.AreEqual(invitationDto.Code, invitationPutModel.InvitationCode);
        Assert.AreEqual(invitationDto.Email, invitationPutModel.Email);
        Assert.AreEqual(invitationDto.Address, invitationPutModel.Address);
        Assert.AreEqual(invitationDto.Password, invitationPutModel.Password);
    }

    [TestMethod]
    public void InvitationDtoToInvitationConfirmedModelOK()
    {
        InvitationDto invitation = new InvitationDto()
        {
            UserId = 1,
            UserName = "JuanPerez",
            Code = "2A5678BX",
            Email = "Juan@email.com",
            Address = "Road A 1234",
            RoleName = "Empployee",
            PharmacyName = "PharmacyName"
        };

        InvitationConfirmedModel invitationConfirmedModel = InvitationModelsMapper.ToModel(invitation);

        Assert.AreEqual(invitation.UserId, invitationConfirmedModel.UserId);
        Assert.AreEqual(invitation.UserName, invitationConfirmedModel.UserName);
        Assert.AreEqual(invitation.Email, invitationConfirmedModel.Email);
        Assert.AreEqual(invitation.Address, invitationConfirmedModel.Address);
        Assert.AreEqual(invitation.RoleName, invitationConfirmedModel.RoleName);
        Assert.AreEqual(invitation.PharmacyName, invitationConfirmedModel.PharmacyName);
    }

    [TestMethod]
    public void InvitationsToModelListOK()
    {
        Invitation invitationForList = new Invitation()
        {
            Id = 1,
            UserName = "JuanPerez",
            Role = new Role()
            {
                Name = "Employee"
            },
            Pharmacy = new Pharmacy()
            {
                Name = "FarmaciaB"
            },
            Code = "2A5678BX"

        };

        List<Invitation> invitationsToConvert = new List<Invitation>()
            {
                invitationForList,
            };

        List<InvitationResponseModel> invitationResponseModels = InvitationModelsMapper.ToModelList(invitationsToConvert);

        Assert.AreEqual(invitationsToConvert.Count, solicitudeResponseModels.Count);
        Assert.AreEqual(invitationsToConvert[0].Id, solicitudeResponseModels[0].Id);
        

    }
}
