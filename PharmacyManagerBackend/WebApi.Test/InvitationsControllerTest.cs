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
using IAuthLogic;
using WebApi.Models.Utils;
using Domain.Dto;

namespace WebApi.Test;

[TestClass]
public class InvitationsControllerTest
{
    private Mock<IInvitationLogic> _invitationLogicMock;
    private InvitationsController _invitationApiController;
    private Invitation _invitation;

    [TestInitialize]
    public void InitTest()
    {
        _invitationLogicMock = new Mock<IInvitationLogic>(MockBehavior.Strict);
        _invitationApiController = new InvitationsController(_invitationLogicMock.Object);
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
        var invitationModel = new InvitationRequestModel()
        {
            UserName = "JuanPerez",
            RoleName = "Employee",
            PharmacyName = "FarmaciaB"
        };
        var invitationModelExpected = new InvitationResponseModel()
        {
            UserName = "JuanPerez",
            RoleName = "Employee",
            PharmacyName = "FarmaciaB",
            InvitationCode = "2A5678BX"
        };

        var result = _invitationApiController.Create(invitationModel);
        var okResult = result as OkObjectResult;
        var createdInvitation = okResult.Value as InvitationResponseModel;

        Assert.AreEqual(invitationModelExpected, createdInvitation);
        _invitationLogicMock.VerifyAll();
    }


    [TestMethod]
    public void UpdateInvitationOk()
    {
        InvitationDto invitationUpdated = new InvitationDto()
        {
            UserName = "JuanPerez",
            Code = "2A5678BX",
            Email = "Juan@email.com",
            Address = "Road A 1234",
            RoleName = "Empployee",
            PharmacyName = "PharmacyName"
        };
        _invitationLogicMock.Setup(m => m.Update(It.IsAny<string>(), It.IsAny<InvitationDto>())).Returns(invitationUpdated);
        var invitationPutModel = new InvitationPutModel()
        {
            UserName = "JuanPerez",
            Email = "Juan@email.com",
            Address = "Road A 1234",
            Password = "12345678"
        };


        var result = _invitationApiController.Update("code", invitationPutModel);
        var okResult = result as OkObjectResult;
        var confirmedInvitation = okResult.Value as InvitationConfirmedModel;

        Assert.AreEqual(invitationUpdated.UserName, confirmedInvitation.UserName);
        Assert.AreEqual(invitationUpdated.RoleName, confirmedInvitation.RoleName);
        Assert.AreEqual(invitationUpdated.PharmacyName, confirmedInvitation.PharmacyName);
        Assert.AreEqual(invitationUpdated.Email, confirmedInvitation.Email);
        Assert.AreEqual(invitationUpdated.Address, confirmedInvitation.Address);

        _invitationLogicMock.VerifyAll();
    }
    
    [TestMethod]
    public void GetAllInvitationsOk()
    {
        List<Invitation> invitations = new List<Invitation>()
        {
            _invitation
        };
        QueryInvitationDto queryInvitationDto = new QueryInvitationDto()
        {

        };
        _invitationLogicMock.Setup(s => s.GetInvitations(queryInvitationDto)).Returns(invitations);


        var result = _invitationApiController.GetInvitations(queryInvitationDto);
        var okResult = result as OkObjectResult;
        var invitationsResult = okResult.Value as List<InvitationResponseModel>;
        var invitationsToReturnModels = InvitationModelsMapper.ToModelList(invitations);


        CollectionAssert.AreEqual(invitationsToReturnModels, invitationsResult);

        _invitationLogicMock.VerifyAll();
    }
}

