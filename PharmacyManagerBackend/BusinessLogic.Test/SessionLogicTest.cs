using System;
using Domain;
using Domain.Dtos;
using Exceptions;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogic.Test;

[TestClass]
public class SessionLogicTest
{
    private SessionLogic _sessionLogic;
    private Mock<ISessionRepository> _sessionRepository;
    private Mock<UserLogic> _userLogic;

    [TestInitialize]
    public void Initialize()
    {
        this._sessionRepository = new Mock<ISessionRepository>(MockBehavior.Strict);
        this._userLogic = new Mock<UserLogic>(MockBehavior.Strict);
        _sessionLogic = new SessionLogic(this._sessionRepository.Object, this._userLogic.Object);
    }

    [TestMethod]
    public void CreateNewSessionOk()
    {
        Guid token = Guid.NewGuid();
        var session = new Session()
        {
            Token = token,
            UserId = 1,
            User = new User
            {
                Id = 1
            }
        };
        User user = new User()
        {
            Id = 1,
            UserName = "ricardofort",
            Password = "1234"
        };
        CredentialsDto credentialsDto = new CredentialsDto()
        {
            UserName = user.UserName,
            Password = user.Password
        };

        _sessionRepository.Setup(m => m.FindSessionByUserId(It.IsAny<int>())).Returns((Session)null);
        _sessionRepository.Setup(m => m.Create(It.IsAny<Session>())).Returns(session);
        _userLogic.Setup(m => m.GetUserByUserName(It.IsAny<string>())).Returns(user);

        TokenDto tokenReturned = _sessionLogic.Create(credentialsDto);

        Assert.AreEqual(token, tokenReturned.Token);
        _sessionRepository.VerifyAll();
        _userLogic.VerifyAll();
    }

    [TestMethod]
    public void CreateAlreadyExistantSessionOk()
    {
        Guid token = Guid.NewGuid();
        var session = new Session()
        {
            Token = token,
            UserId = 1,
            User = new User
            {
                Id = 1
            }
        };
        User user = new User()
        {
            Id = 1,
            UserName = "ricardofort",
            Password = "1234"
        };
        CredentialsDto credentialsDto = new CredentialsDto()
        {
            UserName = user.UserName,
            Password = user.Password
        };

        _sessionRepository.Setup(m => m.FindSessionByUserId(It.IsAny<int>())).Returns(session);
        _userLogic.Setup(m => m.GetUserByUserName(It.IsAny<string>())).Returns(user);

        TokenDto tokenReturned = _sessionLogic.Create(credentialsDto);

        Assert.AreEqual(token, tokenReturned.Token);
        _sessionRepository.VerifyAll();
        _userLogic.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void CreateSessionWithNullUserNameThrowsException()
    {
        CredentialsDto credentialsDto = new CredentialsDto()
        {
            UserName = null,
            Password = "1234"
        };

        _sessionLogic.Create(credentialsDto);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void CreateSessionWithNullPasswordThrowsException()
    {
        CredentialsDto credentialsDto = new CredentialsDto()
        {
            UserName = "ricardofort",
            Password = null
        };

        _sessionLogic.Create(credentialsDto);
    }

    [TestMethod]
    [ExpectedException(typeof(AuthenticationException))]
    public void CreateSessionForNotExistantUserThrowsException()
    {
        CredentialsDto credentialsDto = new CredentialsDto()
        {
            UserName = "ricardofort",
            Password = "1234"
        };
        _userLogic.Setup(m => m.GetUserByUserName(It.IsAny<string>())).Returns((User)null);

        _sessionLogic.Create(credentialsDto);
    }

    [TestMethod]
    [ExpectedException(typeof(AuthenticationException))]
    public void CreateSessionBadPasswordThrowsExcpetion()
    {
        Guid token = Guid.NewGuid();
        var session = new Session()
        {
            Token = token,
            UserId = 1,
            User = new User
            {
                Id = 1
            }
        };
        User user = new User()
        {
            Id = 1,
            UserName = "ricardofort",
            Password = "12345"
        };
        CredentialsDto credentialsDto = new CredentialsDto()
        {
            UserName = user.UserName,
            Password = "1234"
        };
        _sessionRepository.Setup(m => m.FindSessionByUserId(It.IsAny<int>())).Returns(session);
        _userLogic.Setup(m => m.GetUserByUserName(It.IsAny<string>())).Returns(user);

        _sessionLogic.Create(credentialsDto);
    }

}

