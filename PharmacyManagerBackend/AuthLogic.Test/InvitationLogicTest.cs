using System;
using AuthLogic;
using BusinessLogic;
using Domain;
using Domain.Dtos;
using Exceptions;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Assert = NUnit.Framework.Assert;

namespace AuthLogic.Test;

public class InvitationLogicTest
{
    private InvitationLogic _invitationLogic;
    private Mock<IInvitationRepository> _invitationRepository;
    private Mock<UserLogic> _userLogic;
    private Mock<RoleLogic> _roleLogic;
    private Mock<PharmacyLogic> _pharmacyLogic;

    [TestInitialize]
    public void Initialize()
    {
        this._userLogic = new Mock<UserLogic>(MockBehavior.Strict);
        this._roleLogic = new Mock<RoleLogic>(MockBehavior.Strict);
        this._pharmacyLogic = new Mock<PharmacyLogic>(MockBehavior.Strict);
        this._invitationRepository = new Mock<IInvitationRepository>(MockBehavior.Strict);
        this._invitationLogic = new InvitationLogic(this._invitationRepository.Object, this._userLogic.Object, this._roleLogic.Object, this._pharmacyLogic.Object);
    }

    [TestMethod]
    public void CreateNewInvitationOk()
    {
        Invitation invitationRepository = new Invitation()
        {
            Id = 1,
            UserName = "cris01",
            Role = new Role()
            {
                Name = "Employee"
            },
            Code = "123456",
            Pharmacy = new Pharmacy()
            {
                Name = "PharmacyB"
            }
        };
        InvitationDto invitationToCreate = new InvitationDto()
        {
            UserName = "cris01",
            RoleName = "Employee",
            PharmacyName = "PharmacyB"
        };

        _userLogic.Setup(m => m.GetUserByUserName(invitationToCreate.UserName)).Throws(new ResourceNotFoundException(""));
        _invitationRepository.Setup(m => m.Create(It.IsAny<Invitation>())).Returns(invitationRepository);
        _invitationRepository.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>())).Throws(new ResourceNotFoundException(""));
        _pharmacyLogic.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(new Pharmacy()
        {
            Name = invitationToCreate.PharmacyName
        });
        _roleLogic.Setup(m => m.GetRoleByName(It.IsAny<string>())).Returns(new Role()
        {
            Name = invitationToCreate.RoleName
        });

        Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);

        Assert.AreEqual(invitationRepository.Id, createdInvitation.Id);
        Assert.AreEqual(invitationRepository.UserName, createdInvitation.UserName);
        Assert.AreEqual(invitationRepository.Role.Name, createdInvitation.Role.Name);
        Assert.AreEqual(invitationRepository.Pharmacy.Name, createdInvitation.Pharmacy.Name);
        Assert.IsTrue(createdInvitation.Code.Length == 6);
        _userLogic.VerifyAll();
        _invitationRepository.VerifyAll();
        _pharmacyLogic.VerifyAll();
        _roleLogic.VerifyAll();
    }

    [TestMethod]
    public void CreateNewInvitationRepeatedCodeOnceOK()
    {
        Invitation invitationRepository = new Invitation()
        {
            Id = 1,
            UserName = "cris01",
            Role = new Role()
            {
                Name = "Employee"
            },
            Code = "123456"
        };
        InvitationDto invitationToCreate = new InvitationDto()
        {
            UserName = "cris01",
            RoleName = "Employee"
        };

        _userLogic.Setup(m => m.GetUserByUserName(invitationToCreate.UserName)).Throws(new ResourceNotFoundException(""));
        _invitationRepository.Setup(m => m.Create(It.IsAny<Invitation>())).Returns(invitationRepository);
        _invitationRepository.SetupSequence(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Returns(new Invitation() { })
            .Throws(new ResourceNotFoundException(""));
        _pharmacyLogic.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(new Pharmacy()
        {
            Name = invitationToCreate.PharmacyName
        });
        _roleLogic.Setup(m => m.GetRoleByName(It.IsAny<string>())).Returns(new Role()
        {
            Name = invitationToCreate.RoleName
        });

        Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);

        Assert.AreEqual(invitationRepository.Id, createdInvitation.Id);
        Assert.AreEqual(invitationRepository.UserName, createdInvitation.UserName);
        Assert.AreEqual(invitationRepository.Role.Name, createdInvitation.Role.Name);
        const int invitationCodeRequiredLength = 6;
        Assert.IsTrue(createdInvitation.Code.Length == invitationCodeRequiredLength);
        _userLogic.VerifyAll();
        _invitationRepository.VerifyAll();
        _pharmacyLogic.VerifyAll();
        _roleLogic.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void CreateInvitationRepeatedUserNameShouldThrowError()
    {
        Invitation invitationRepository = new Invitation()
        {
            Id = 1,
            UserName = "cris01",
            Role = new Role()
            {
                Name = "Employee"
            }
        };
        InvitationDto invitationToCreate = new InvitationDto()
        {
            UserName = "cris01",
            RoleName = "Employee"
        };

        _userLogic.Setup(m => m.GetUserByUserName(invitationToCreate.UserName)).Returns(new User());
        _invitationRepository.Setup(m => m.Create(It.IsAny<Invitation>())).Returns(invitationRepository);

        Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void CreateInvitationInvalidRoleNameShouldThrowError()
    {
        InvitationDto invitationToCreate = new InvitationDto()
        {
            UserName = "cris01",
            RoleName = "x",
            PharmacyName = "Farmashop"
        };

        _userLogic.Setup(m => m.GetUserByUserName(invitationToCreate.UserName)).Throws(new ResourceNotFoundException(""));
        _pharmacyLogic.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(new Pharmacy()
        {
            Name = invitationToCreate.PharmacyName
        });
        _roleLogic.Setup(m => m.GetRoleByName(It.IsAny<string>())).Throws(new ResourceNotFoundException(""));

        Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void CreateInvitationInvalidPharmacyNameShouldThrowError()
    {
        InvitationDto invitationToCreate = new InvitationDto()
        {
            UserName = "cris01",
            RoleName = "x",
            PharmacyName = "Farmashop"
        };

        _userLogic.Setup(m => m.GetUserByUserName(invitationToCreate.UserName)).Throws(new ResourceNotFoundException(""));
        _roleLogic.Setup(m => m.GetRoleByName(It.IsAny<string>())).Returns(new Role()
        {
            Name = invitationToCreate.RoleName
        });
        _pharmacyLogic.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Throws(new ResourceNotFoundException(""));


        Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);
    }


    [TestMethod]
    public void GetInvitationByCodeOk()
    {
        string invitationCode = "123456";
        Invitation invitationRepository = new Invitation()
        {
            Id = 1,
            UserName = "cris01",
            Role = new Role()
            {
                Name = "Employee"
            },
            Code = invitationCode
        };

        _invitationRepository.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>())).Returns(invitationRepository);


        Invitation invitationReturned = _invitationLogic.GetInvitationByCode(invitationCode);

        Assert.AreEqual(invitationRepository, invitationReturned);
    }


    [TestMethod]
    public void UpdateInvitationOk()
    {
        string invitationCode = "code";
        InvitationDto invitationToUpdate= new InvitationDto()
        {
            UserName = "JuanPerez",
            Code = "2A5678BX",
            Email = "Juan@email.com",
            Address = "Road A 1234",
            RoleName = "Empployee",
            PharmacyName = "PharmacyName"
        };
        User userRepository = new User()
        {
            Id = 1,
            UserName = "Cris01",
            Role = new Role()
            {
                Name = "Employee"
            },
            Email = "cris@gmail.com",
            Address = "calle a 123",
            Password = "pass.1234",
            RegistrationDate = DateTime.Now
        };
        Invitation userInvitation = new Invitation
        {
            Id = 1,
            UserName = "Cris01",
            Role = new Role()
            {
                Name = "Employee"
            },
            Code = invitationToUpdate.Code
        };
        _userLogic.Setup(m => m.Create(It.IsAny<User>())).Returns(userRepository);
        _invitationRepository.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>())).Returns(userInvitation);
        _invitationRepository.Setup(m => m.Delete(It.IsAny<Invitation>())).Callback(() => { });
        
        InvitationDto invitationDtoUpdated = _invitationLogic.Update(invitationCode, invitationToUpdate);

        Assert.AreEqual(invitationDtoUpdated, invitationToUpdate);
        _userLogic.VerifyAll();
        _invitationRepository.VerifyAll();
    }


    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void UpdateNotExistantInvitationShouldFail()
    {
        string invitationCode = "code";
        InvitationDto invitationToUpdate= new InvitationDto()
        {
            UserName = "JuanPerez",
            Code = "2A5678BX",
            Email = "Juan@email.com",
            Address = "Road A 1234",
            RoleName = "Empployee",
            PharmacyName = "PharmacyName"
        };
        Invitation userInvitation = new Invitation
        {
            Id = 1,
            UserName = "Cris01",
            Role = new Role()
            {
                Name = "Employee"
            },
            Code = invitationToUpdate.Code
        };
        _invitationRepository.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>())).Throws(new ResourceNotFoundException(""));

        _invitationLogic.Update(invitationCode, invitationToUpdate);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void UpdateInvitationCodeForDiferentUserShouldFail()
    {
        string invitationCode = "code";
        InvitationDto invitationToUpdate= new InvitationDto()
        {
            UserName = "JuanPerez",
            Code = "2A5678BX",
            Email = "Juan@email.com",
            Address = "Road A 1234",
            RoleName = "Empployee",
            PharmacyName = "PharmacyName"
        };
        User userRepository = new User()
        {
            Id = 1,
            UserName = "Cris01",
            Role = new Role()
            {
                Name = "Employee"
            },
            Email = "cris@gmail.com",
            Address = "calle a 123",
            Password = "pass.1234",
            RegistrationDate = DateTime.Now
        };
        Invitation userInvitation = new Invitation
        {
            Id = 1,
            UserName = "Cris01",
            Role = new Role()
            {
                Name = "Employee"
            },
            Code = "OtherInvitationCode"
        };
        _invitationRepository.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>())).Returns(userInvitation);
        _invitationRepository.Setup(m => m.Delete(It.IsAny<Invitation>())).Callback(() => { });
        
        _invitationLogic.Update(invitationCode, invitationToUpdate);
    }

}
