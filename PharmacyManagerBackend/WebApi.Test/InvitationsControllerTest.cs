using System.Collections.Generic;
using Domain;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Test.Utils;
using IBusinessLogic;
using Domain.Dtos;

namespace WebApi.Test;

[TestClass]
public class InvitationsControllerTest
{
    private Mock<IInvitationLogic> _invitationLogicMock;
    private InvitationController _invitationApiController;
    private Invitation _invitation;

    [TestInitialize]
    public void InitTest()
    {
        _invitationLogicMock = new Mock<IInvitationLogic>(MockBehavior.Strict);
        _invitationApiController = new InvitationController(_invitationLogicMock.Object);
        _invitation = new Invitation()
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
    }


    [TestMethod]
    public void CreateInvitationOk()
    {
        _invitationLogicMock.Setup(m => m.Create(It.IsAny<InvitationDto>())).Returns(_invitation);
        var invitationModel = new InvitationModel()
        {
            Id = 1,
            UserName = "JuanPerez",
            RoleName = "Employee",
            InvitationCode = "2A5678BX",
            PharmacyName = "FarmaciaB"
        };


        var result = _invitationApiController.Create(invitationModel);
        var okResult = result as OkObjectResult;
        var createdInvitation = okResult.Value as InvitationModel;

        Assert.IsTrue(ModelsComparer.InvitationCompare(invitationModel, createdInvitation));
        _invitationLogicMock.VerifyAll();
    }


    [TestMethod]
    public void UpdateInvitationOk()
    {
        InvitationDto invitationUpdated = new InvitationDto()
        {
            UserId = 1,
            UserName = "JuanPerez",
            Code = "2A5678BX",
            Email = "Juan@email.com",
            Address = "Road A 1234",
            RoleName = "Empployee",
            PharmacyName = "PharmacyName"
        };
        _invitationLogicMock.Setup(m => m.Update(It.IsAny<int>(), It.IsAny<InvitationDto>())).Returns(invitationUpdated);
        var invitationPutModel = new InvitationPutModel()
        {
            UserName = "JuanPerez",
            InvitationCode = "2A5678BX",
            Email = "Juan@email.com",
            Address = "Road A 1234",
            Password = "12345678"
        };


        var result = _invitationApiController.Update(1, invitationPutModel);
        var okResult = result as OkObjectResult;
        var confirmedInvitation = okResult.Value as InvitationConfirmedModel;

        Assert.AreEqual(invitationUpdated.UserId, confirmedInvitation.UserId);
        Assert.AreEqual(invitationUpdated.UserName, confirmedInvitation.UserName);
        Assert.AreEqual(invitationUpdated.RoleName, confirmedInvitation.RoleName);
        Assert.AreEqual(invitationUpdated.PharmacyName, confirmedInvitation.PharmacyName);
        Assert.AreEqual(invitationUpdated.Email, confirmedInvitation.Email);
        Assert.AreEqual(invitationUpdated.Address, confirmedInvitation.Address);


        _invitationLogicMock.VerifyAll();
    }
}

