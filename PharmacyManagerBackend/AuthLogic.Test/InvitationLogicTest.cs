using Domain;
using Domain.Dtos;
using Exceptions;
using IBusinessLogic;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace AuthLogic.Test;

[TestClass]
public class InvitationLogicTest
{
    private InvitationLogic _invitationLogic;
    private Mock<IInvitationRepository> _invitationRepository;
    private Mock<IUserLogic> _userLogic;
    private Mock<IRoleLogic> _roleLogic;
    private Mock<IPharmacyLogic> _pharmacyLogic;
    private Mock<Context> _currentContext;
    
    [TestInitialize]
    public void Initialize()
    {
        this._userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
        this._roleLogic = new Mock<IRoleLogic>(MockBehavior.Strict);
        this._pharmacyLogic = new Mock<IPharmacyLogic>(MockBehavior.Strict);
        this._invitationRepository = new Mock<IInvitationRepository>(MockBehavior.Strict);
        this._currentContext = new Mock<Context>(MockBehavior.Strict);
        this._invitationLogic = new InvitationLogic(
            this._invitationRepository.Object, 
            this._userLogic.Object, 
            this._roleLogic.Object, 
            this._pharmacyLogic.Object, 
            this._currentContext.Object);
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

        _userLogic.Setup(m => m.GetFirst(It.IsAny<Func<User,bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _invitationRepository.Setup(m => m.Create(It.IsAny<Invitation>())).Returns(invitationRepository);
        _invitationRepository.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _pharmacyLogic.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(new Pharmacy()
        {
            Name = invitationToCreate.PharmacyName
        });
        _roleLogic.Setup(m => m.GetRoleByName(It.IsAny<string>())).Returns(new Role()
        {
            Name = invitationToCreate.RoleName
        });
        _currentContext.Setup(m => m.CurrentUser).Returns(new User()
        {
            Role = new Role()
            {
                Name = Role.ADMIN

        }});

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
        _currentContext.VerifyAll();
    }

    [TestMethod]
    public void CreateNewInvitationForAdminUserOk()
    {
        Invitation invitationRepository = new Invitation()
        {
            Id = 1,
            UserName = "cris01",
            Role = new Role()
            {
                Name = "Admin"
            },
            Code = "123456"
        };
        InvitationDto invitationToCreate = new InvitationDto()
        {
            UserName = "cris01",
            RoleName = "Admin"
        };

        _userLogic.Setup(m => m.GetFirst(It.IsAny<Func<User,bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _invitationRepository.Setup(m => m.Create(It.IsAny<Invitation>())).Returns(invitationRepository);
        _invitationRepository.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _roleLogic.Setup(m => m.GetRoleByName(It.IsAny<string>())).Returns(new Role()
        {
            Name = invitationToCreate.RoleName
        });
        _currentContext.Setup(m => m.CurrentUser).Returns(new User()
        {
            Role = new Role()
            {
                Name = Role.ADMIN

            }});

        Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);

        Assert.AreEqual(invitationRepository.Id, createdInvitation.Id);
        Assert.AreEqual(invitationRepository.UserName, createdInvitation.UserName);
        Assert.AreEqual(invitationRepository.Role.Name, createdInvitation.Role.Name);
        Assert.IsTrue(createdInvitation.Code.Length == 6);
        _userLogic.VerifyAll();
        _invitationRepository.VerifyAll();
        _pharmacyLogic.VerifyAll();
        _roleLogic.VerifyAll();
        _currentContext.VerifyAll();
    }
    
    [TestMethod]
    public void CreateNewInvitationByOwnerUserOk()
    {
        Pharmacy pharmacyOfOwner = new Pharmacy()
        {
            Name = "pharmacy"
        };
        Invitation invitationRepository = new Invitation()
        {
            Id = 1,
            UserName = "cris01",
            Role = new Role()
            {
                Name = "Employee"
            },
            Code = "123456",
            Pharmacy = pharmacyOfOwner
        };
        InvitationDto invitationToCreate = new InvitationDto()
        {
            UserName = "cris01"
        };
        

        _userLogic.Setup(m => m.GetFirst(It.IsAny<Func<User,bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _invitationRepository.Setup(m => m.Create(It.IsAny<Invitation>())).Returns(invitationRepository);
        _invitationRepository.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        
        _currentContext.Setup(m => m.CurrentUser).Returns(new User()
        {
            Role = new Role()
            {
                Name = Role.OWNER

            },
            Pharmacy = pharmacyOfOwner
        });
        _roleLogic.Setup(m => m.GetRoleByName(It.IsAny<string>())).Returns(new Role()
        {
            Name = Role.EMPLOYEE
        });
        
        Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);

        Assert.AreEqual(invitationRepository.Id, createdInvitation.Id);
        Assert.AreEqual(invitationRepository.UserName, createdInvitation.UserName);
        Assert.AreEqual(invitationRepository.Role.Name, createdInvitation.Role.Name);
        Assert.AreEqual(invitationRepository.Pharmacy.Name, createdInvitation.Pharmacy.Name);
        Assert.IsTrue(createdInvitation.Code.Length == 6);
        _userLogic.VerifyAll();
        _invitationRepository.VerifyAll();
        _currentContext.VerifyAll();
        _roleLogic.VerifyAll();
    }
    
    [TestMethod]
    public void CreateInvitationForInvitationAlreadyCreated()
    {
        Invitation invitationRepository = new Invitation()
        {
            Id = 1,
            UserName = "cris01",
            Role = new Role()
            {
                Name = "Admin"
            },
            Code = "123456"
        };
        InvitationDto invitationToCreate = new InvitationDto()
        {
            UserName = "cris01",
            RoleName = "Admin"
        };
        
        _invitationRepository.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Returns(invitationRepository);

        Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);

        Assert.AreEqual(invitationRepository.Id, createdInvitation.Id);
        Assert.AreEqual(invitationRepository.UserName, createdInvitation.UserName);
        Assert.AreEqual(invitationRepository.Role.Name, createdInvitation.Role.Name);
        Assert.IsTrue(createdInvitation.Code.Length == 6);

        _invitationRepository.VerifyAll();
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
            RoleName = "Employee",
            PharmacyName = "Farmashop"
        };

        _userLogic.Setup(m => m.GetFirst(It.IsAny<Func<User,bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _invitationRepository.Setup(m => m.Create(It.IsAny<Invitation>())).Returns(invitationRepository);
        _invitationRepository.SetupSequence(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Throws(new ResourceNotFoundException(""))
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
        _currentContext.Setup(m => m.CurrentUser).Returns(new User()
        {
            Role = new Role()
            {
                Name = Role.ADMIN

            }
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
        _currentContext.VerifyAll();
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

        _userLogic.Setup(m => m.GetFirst(It.IsAny<Func<User,bool>>()))
            .Returns(new User());
        _invitationRepository.SetupSequence(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Throws(new ResourceNotFoundException(""))
            .Returns(invitationRepository);

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

        _userLogic.Setup(m => m.GetFirst(It.IsAny<Func<User,bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _pharmacyLogic.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(new Pharmacy()
        {
            Name = invitationToCreate.PharmacyName
        });
        _roleLogic.Setup(m => m.GetRoleByName(It.IsAny<string>())).Throws(new ResourceNotFoundException(""));
        _invitationRepository.SetupSequence(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _currentContext.Setup(m => m.CurrentUser).Returns(new User()
        {
            Role = new Role()
            {
                Name = Role.ADMIN

            }
        });

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

        _userLogic.Setup(m => m.GetFirst(It.IsAny<Func<User,bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _roleLogic.Setup(m => m.GetRoleByName(It.IsAny<string>())).Returns(new Role()
        {
            Name = invitationToCreate.RoleName
        });
        _currentContext.Setup(m => m.CurrentUser).Returns(new User()
        {
            Role = new Role()
            {
                Name = Role.ADMIN

            }
        });
        _pharmacyLogic.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Throws(new ResourceNotFoundException(""));
        _invitationRepository.SetupSequence(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Throws(new ResourceNotFoundException(""));

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
        string invitationCode = "111111";
        InvitationDto invitationToUpdate = new InvitationDto()
        {
            UserName = "Cris01",
            Code = invitationCode,
            Email = "cris@gmail.com",
            Address = "Road A 1234",
            RoleName = "Employee",
            PharmacyName = "PharmacyName",
            Password = "Contraseña-"
        };
        InvitationDto invitationExpected = new InvitationDto()
        {
            UserName = "Cris01",
            Email = "cris@gmail.com",
            Address = "Road A 1234",
            RoleName = "Employee",
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
            Address = "Road A 1234",
            Password = "Contraseña-",
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
            Code = invitationToUpdate.Code,
            Pharmacy = new Pharmacy()
            {
                Name = "PharmacyName"
            }
        };
        _userLogic.Setup(m => m.Create(It.IsAny<User>())).Returns(userRepository);
        _invitationRepository.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>())).Returns(userInvitation);
        _invitationRepository.Setup(m => m.Update(It.IsAny<Invitation>())).Callback(() => { });
        _userLogic.Setup(m => m.GetFirst(It.IsAny<Func<User, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _currentContext.Setup(m => m.CurrentUser).Returns((User) null);
        
        InvitationDto invitationDtoUpdated = _invitationLogic.Update(invitationCode, invitationToUpdate);

        Assert.AreEqual(invitationExpected, invitationDtoUpdated);
        _userLogic.VerifyAll();
        _invitationRepository.VerifyAll();
        _currentContext.VerifyAll();
    }
    
    
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void UpdateInvitationUsedShouldThrowError()
    {
        string invitationCode = "111111";
        InvitationDto invitationToUpdate = new InvitationDto()
        {
            UserName = "Cris01",
            Code = invitationCode,
            Email = "cris@gmail.com",
            Address = "Road A 1234",
            RoleName = "Employee",
            PharmacyName = "PharmacyName",
            Password = "Contraseña-"
        };
        Invitation userInvitation = new Invitation
        {
            Id = 1,
            UserName = "Cris01",
            Role = new Role()
            {
                Name = "Employee"
            },
            Code = invitationToUpdate.Code,
            Pharmacy = new Pharmacy()
            {
                Name = "PharmacyName"
            },
            Used = true
        };
        _invitationRepository.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>())).Returns(userInvitation);

        _invitationLogic.Update(invitationCode, invitationToUpdate);
    }
    
    
    [TestMethod]
    public void UpdateInvitationAsAdminOk()
    {
        string invitationCode = "111111";
        InvitationDto invitationToUpdate = new InvitationDto()
        {
            UserName = "Cris01",
            RoleName = "Employee",
            PharmacyName = "PharmacyName"
        };
        Invitation userInvitation = new Invitation
        {
            Id = 1,
            UserName = "Cris02",
            Role = new Role()
            {
                Name = "Owner"
            },
            Code = invitationToUpdate.Code,
            Pharmacy = new Pharmacy()
            {
                Name = "PharmacyName2"
            }
        };

        _invitationRepository.SetupSequence(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Returns(userInvitation)
            .Throws(new ResourceNotFoundException(""));
        _invitationRepository.Setup(m => m.Update(It.IsAny<Invitation>())).Callback(() => { });
        _userLogic.Setup(m => m.GetFirst(It.IsAny<Func<User, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _currentContext.Setup(m => m.CurrentUser).Returns(new User()
        {
            Role = new Role()
            {
                Name = Role.ADMIN 
            }
        });
        _pharmacyLogic.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(new Pharmacy()
        {
            Name = invitationToUpdate.PharmacyName
        });
        _roleLogic.Setup(m => m.GetRoleByName(It.IsAny<string>())).Returns(new Role()
        {
            Name = invitationToUpdate.RoleName
        });
        
        InvitationDto invitationDtoUpdated = _invitationLogic.Update(invitationCode, invitationToUpdate);

        Assert.AreEqual(invitationToUpdate.UserName, invitationDtoUpdated.UserName);
        Assert.AreEqual(invitationToUpdate.PharmacyName, invitationDtoUpdated.PharmacyName);
        Assert.AreEqual(invitationToUpdate.RoleName, invitationDtoUpdated.RoleName);
        Assert.IsNotNull(invitationDtoUpdated.Code);
        _userLogic.VerifyAll();
        _invitationRepository.VerifyAll();
        _currentContext.VerifyAll();
    }



    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void UpdateNotExistantInvitationShouldFail()
    {
        string invitationCode = "code";
        InvitationDto invitationToUpdate = new InvitationDto()
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
        _invitationRepository.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Throws(new ResourceNotFoundException(""));

        _invitationLogic.Update(invitationCode, invitationToUpdate);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void UpdateInvitationCodeForDiferentUserShouldFail()
    {
        string invitationCode = "code";
        InvitationDto invitationToUpdate = new InvitationDto()
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
        _currentContext.Setup(m => m.CurrentUser).Returns((User) null);
        
        _invitationLogic.Update(invitationCode, invitationToUpdate);
    }
    
    [TestMethod]
    public void TestGetAllInvitationsOK()
    {

        Invitation userInvitation = new Invitation
        {
            Id = 1,
            UserName = "Cris01",
            Role = new Role()
            {
                Name = "Employee"
            },
            Code = "OtherInvitationCode",
            Used = false
        };
        List<Invitation> invitationItems = new List<Invitation>()
        {
            userInvitation
        };
        _invitationRepository.Setup(s => s.GetAll(It.IsAny<Func<Invitation, bool>>())).Returns(invitationItems);

        List<Invitation> invitationsReturned = _invitationLogic.GetAll().ToList();
        
        CollectionAssert.AreEqual(invitationItems, invitationsReturned);
        _invitationRepository.VerifyAll();
    }
}