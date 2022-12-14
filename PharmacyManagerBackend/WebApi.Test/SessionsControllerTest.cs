using System;
using Domain;
using Domain.AuthDomain;
using Domain.Dtos;
using IAuthLogic;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApi.Test;

[TestClass]
public class SessionsControllerTest
{
    private Mock<ISessionLogic> _sessionLogicMock;
    private SessionsController _sessionsApiController;

    [TestInitialize]
    public void InitTest()
    {
        _sessionLogicMock = new Mock<ISessionLogic>(MockBehavior.Strict);
        _sessionsApiController = new SessionsController(_sessionLogicMock.Object);
    }

    [TestMethod]
    public void CreateSessionOk()
    {
        var token = new TokenDto()
        {
            Token = Guid.NewGuid()
        };
        var credentialsModel = new CredentialsModel()
        {
            UserName = "Cris",
            Password = "Cris.2022"
        };
        _sessionLogicMock.Setup(m => m.Create(It.IsAny<CredentialsDto>())).Returns(token);


        var result = _sessionsApiController.CreateSession(credentialsModel);
        var okResult = result as OkObjectResult;
        var tokenModel = okResult.Value as TokenModel;

        Assert.AreEqual(token.Token, tokenModel.Token);
        _sessionLogicMock.VerifyAll();
    }
    
    [TestMethod]
    public void GetProfileOk()
    {
        var session = new Session()
        {
            User = new User()
            {
                UserName = "UserName",
                Role = new Role()
                {
                    Name = "Employee"
                }
            }
        };
        _sessionLogicMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(session);


        var result = _sessionsApiController.GetSessionProfile(Guid.NewGuid().ToString());
        var okResult = result as OkObjectResult;
        var profile = okResult.Value as SessionProfileModel;

        Assert.AreEqual(session.User.UserName, profile.UserName);
        Assert.AreEqual(session.User.Role.Name, profile.RoleName);
        _sessionLogicMock.VerifyAll();
    }

}

