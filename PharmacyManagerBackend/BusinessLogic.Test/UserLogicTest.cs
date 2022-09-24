using System;
using System.Collections.Generic;
using Domain;
using Domain.Dtos;
using Exceptions;
using IDataAccess;
using Moq;

namespace BusinessLogic.Test;

[TestClass]
public class UserLogicTest
{
    private UserLogic _userLogic;
    private Mock<IUserRepository> _userRepository;
    private Mock<InvitationLogic> _invitationLogic;

    [TestInitialize]
    public void Initialize()
    {
        this._userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
        this._invitationLogic = new Mock<InvitationLogic>(MockBehavior.Strict, null, null, null, null);
        _userLogic = new UserLogic(this._userRepository.Object);
    }

    [TestMethod]
    public void CreateNewUserOk()
    {
        DateTime registrationDate = DateTime.Now;
        User userRepository = new User()
        {
            Id = 1,
            UserName = "Cris01",
            Role = new Role()
            {
                Name = "Admin"
            },
            Email = "cris@gmail.com",
            Address = "calle a 123",
            Password = "pass.1234",
            RegistrationDate = registrationDate
        };
        User userToCreate = new User()
        {
            UserName = "Cris01",
            Role = new Role()
            {
                Name = "Admin"
            },
            Email = "cris@gmail.com",
            Address = "calle a 123",
            Password = "pass.1234",
            RegistrationDate = registrationDate
        };
        _userRepository.Setup(m => m.Create(It.IsAny<User>())).Returns(userRepository);

        User userCreated = _userLogic.Create(userToCreate);

        Assert.AreEqual(userCreated, userRepository);
        _invitationLogic.VerifyAll();
        _userRepository.VerifyAll();
    }
    
    [TestMethod]
    public void GetUserByUserNameOk()
    {
        DateTime registrationDate = DateTime.Now;
        string userName = "Cris01";
        User userRepository = new User()
        {
            Id = 1,
            UserName = userName,
            Role = new Role()
            {
                Name = "Admin"
            },
            Email = "cris@gmail.com",
            Address = "calle a 123",
            Password = "pass.1234",
            RegistrationDate = registrationDate
        };

        _userRepository.Setup(m => m.GetFirst(It.IsAny<Func<User, bool>>())).Returns(userRepository);

        User userReturned = _userLogic.GetUserByUserName(userName);

        Assert.AreEqual(userRepository, userReturned);
        _userRepository.VerifyAll();
    }
}

