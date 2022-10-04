using System;
using AuthLogic;
using Domain;
using Domain.AuthDomain;
using Domain.Dtos;
using Exceptions;
using IBusinessLogic;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace AuthLogic.Test;

[TestClass]
public class SessionLogicTest
{
    private SessionLogic _sessionLogic;
    private Mock<ISessionRepository> _sessionRepository;
    private Mock<IUserLogic> _userLogic;

    [TestInitialize]
    public void Initialize()
    {
        this._sessionRepository = new Mock<ISessionRepository>(MockBehavior.Strict);
        this._userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
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
            Password = "Contraseña-"
        };
        CredentialsDto credentialsDto = new CredentialsDto()
        {
            UserName = user.UserName,
            Password = user.Password
        };

        _sessionRepository.Setup(m => m.GetFirst(It.IsAny<Func<Session, bool>>())).Throws(new ResourceNotFoundException(""));
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
            Password = "Contraseña-"
        };
        CredentialsDto credentialsDto = new CredentialsDto()
        {
            UserName = user.UserName,
            Password = user.Password
        };

        _sessionRepository.Setup(m => m.GetFirst(It.IsAny<Func<Session, bool>>())).Returns(session);
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
            Password = "Contraseña-"
        };
        CredentialsDto credentialsDto = new CredentialsDto()
        {
            UserName = user.UserName,
            Password = "Contraseña+"
        };
        _sessionRepository.Setup(m => m.GetFirst(It.IsAny<Func<Session, bool>>())).Returns(session);
        _userLogic.Setup(m => m.GetUserByUserName(It.IsAny<string>())).Returns(user);

        _sessionLogic.Create(credentialsDto);
    }

    [TestMethod]
    public void GetSessionOk()
    {
        Guid token = Guid.NewGuid();
        User userRepository = new User()
        {
            Id = 1,
            UserName = "username",
            Role = new Role()
            {
                Name = "Admin"
            }
        };
        Session sessionRepository = new Session()
        {
            Id = 1,
            Token = token,
            User = userRepository
        };
        
        _sessionRepository.Setup(m => m.GetFirst(It.IsAny<Func<Session, bool>>())).Returns(sessionRepository);

        Session sessionReturned = _sessionLogic.Get(token);

        Assert.AreEqual(sessionRepository, sessionReturned);
        _sessionRepository.VerifyAll();
    }
}

