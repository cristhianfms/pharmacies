using Domain;
using Domain.Dto;
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
    private Mock<IInvitationRepository> _invitationRepositoryMock;
    private Mock<IUserLogic> _userLogicMock;
    private Mock<IRoleLogic> _roleLogicMock;
    private Mock<IPharmacyLogic> _pharmacyLogicMock;
    private Mock<Context> _currentContextMock;

    [TestInitialize]
    public void Initialize()
    {
        this._userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
        this._roleLogicMock = new Mock<IRoleLogic>(MockBehavior.Strict);
        this._pharmacyLogicMock = new Mock<IPharmacyLogic>(MockBehavior.Strict);
        this._invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
        this._currentContextMock = new Mock<Context>(MockBehavior.Strict);
        this._invitationLogic = new InvitationLogic(
            this._invitationRepositoryMock.Object,
            this._userLogicMock.Object,
            this._roleLogicMock.Object,
            this._pharmacyLogicMock.Object,
            this._currentContextMock.Object);
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

        _userLogicMock.Setup(u => u.GetFirst(It.IsAny<Func<User, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _invitationRepositoryMock.Setup(i => i.Create(It.IsAny<Invitation>())).Returns(invitationRepository);
        _invitationRepositoryMock.Setup(i => i.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _pharmacyLogicMock.Setup(p => p.GetPharmacyByName(It.IsAny<string>())).Returns(new Pharmacy()
        {
            Name = invitationToCreate.PharmacyName
        });
        _roleLogicMock.Setup(r => r.GetRoleByName(It.IsAny<string>())).Returns(new Role()
        {
            Name = invitationToCreate.RoleName
        });
        _currentContextMock.Setup(m => m.CurrentUser).Returns(new User()
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
        Assert.AreEqual(invitationRepository.Pharmacy.Name, createdInvitation.Pharmacy.Name);
        Assert.IsTrue(createdInvitation.Code.Length == 6);
        _userLogicMock.VerifyAll();
        _invitationRepositoryMock.VerifyAll();
        _pharmacyLogicMock.VerifyAll();
        _roleLogicMock.VerifyAll();
        _currentContextMock.VerifyAll();
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

        _userLogicMock.Setup(m => m.GetFirst(It.IsAny<Func<User, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _invitationRepositoryMock.Setup(m => m.Create(It.IsAny<Invitation>())).Returns(invitationRepository);
        _invitationRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _roleLogicMock.Setup(m => m.GetRoleByName(It.IsAny<string>())).Returns(new Role()
        {
            Name = invitationToCreate.RoleName
        });
        _currentContextMock.Setup(m => m.CurrentUser).Returns(new User()
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
        Assert.IsTrue(createdInvitation.Code.Length == 6);
        _userLogicMock.VerifyAll();
        _invitationRepositoryMock.VerifyAll();
        _pharmacyLogicMock.VerifyAll();
        _roleLogicMock.VerifyAll();
        _currentContextMock.VerifyAll();
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


        _userLogicMock.Setup(m => m.GetFirst(It.IsAny<Func<User, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _invitationRepositoryMock.Setup(m => m.Create(It.IsAny<Invitation>())).Returns(invitationRepository);
        _invitationRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Throws(new ResourceNotFoundException(""));

        _currentContextMock.Setup(m => m.CurrentUser).Returns(new User()
        {
            Role = new Role()
            {
                Name = Role.OWNER

            },
            Pharmacy = pharmacyOfOwner
        });
        _roleLogicMock.Setup(m => m.GetRoleByName(It.IsAny<string>())).Returns(new Role()
        {
            Name = Role.EMPLOYEE
        });

        Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);

        Assert.AreEqual(invitationRepository.Id, createdInvitation.Id);
        Assert.AreEqual(invitationRepository.UserName, createdInvitation.UserName);
        Assert.AreEqual(invitationRepository.Role.Name, createdInvitation.Role.Name);
        Assert.AreEqual(invitationRepository.Pharmacy.Name, createdInvitation.Pharmacy.Name);
        Assert.IsTrue(createdInvitation.Code.Length == 6);
        _userLogicMock.VerifyAll();
        _invitationRepositoryMock.VerifyAll();
        _currentContextMock.VerifyAll();
        _roleLogicMock.VerifyAll();
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

        _invitationRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Returns(invitationRepository);

        Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);

        Assert.AreEqual(invitationRepository.Id, createdInvitation.Id);
        Assert.AreEqual(invitationRepository.UserName, createdInvitation.UserName);
        Assert.AreEqual(invitationRepository.Role.Name, createdInvitation.Role.Name);
        Assert.IsTrue(createdInvitation.Code.Length == 6);

        _invitationRepositoryMock.VerifyAll();
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

        _userLogicMock.Setup(m => m.GetFirst(It.IsAny<Func<User, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _invitationRepositoryMock.Setup(m => m.Create(It.IsAny<Invitation>())).Returns(invitationRepository);
        _invitationRepositoryMock.SetupSequence(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Throws(new ResourceNotFoundException(""))
            .Returns(new Invitation() { })
            .Throws(new ResourceNotFoundException(""));
        _pharmacyLogicMock.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(new Pharmacy()
        {
            Name = invitationToCreate.PharmacyName
        });
        _roleLogicMock.Setup(m => m.GetRoleByName(It.IsAny<string>())).Returns(new Role()
        {
            Name = invitationToCreate.RoleName
        });
        _currentContextMock.Setup(m => m.CurrentUser).Returns(new User()
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
        _userLogicMock.VerifyAll();
        _invitationRepositoryMock.VerifyAll();
        _pharmacyLogicMock.VerifyAll();
        _roleLogicMock.VerifyAll();
        _currentContextMock.VerifyAll();
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

        _userLogicMock.Setup(m => m.GetFirst(It.IsAny<Func<User, bool>>()))
            .Returns(new User());
        _invitationRepositoryMock.SetupSequence(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
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

        _userLogicMock.Setup(m => m.GetFirst(It.IsAny<Func<User, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _pharmacyLogicMock.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(new Pharmacy()
        {
            Name = invitationToCreate.PharmacyName
        });
        _roleLogicMock.Setup(m => m.GetRoleByName(It.IsAny<string>())).Throws(new ResourceNotFoundException(""));
        _invitationRepositoryMock.SetupSequence(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _currentContextMock.Setup(m => m.CurrentUser).Returns(new User()
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

        _userLogicMock.Setup(m => m.GetFirst(It.IsAny<Func<User, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _roleLogicMock.Setup(m => m.GetRoleByName(It.IsAny<string>())).Returns(new Role()
        {
            Name = invitationToCreate.RoleName
        });
        _currentContextMock.Setup(m => m.CurrentUser).Returns(new User()
        {
            Role = new Role()
            {
                Name = Role.ADMIN

            }
        });
        _pharmacyLogicMock.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Throws(new ResourceNotFoundException(""));
        _invitationRepositoryMock.SetupSequence(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
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

        _invitationRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>())).Returns(invitationRepository);


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
        _userLogicMock.Setup(m => m.Create(It.IsAny<User>())).Returns(userRepository);
        _invitationRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>())).Returns(userInvitation);
        _invitationRepositoryMock.Setup(m => m.Update(It.IsAny<Invitation>())).Callback(() => { });
        _userLogicMock.Setup(m => m.GetFirst(It.IsAny<Func<User, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _currentContextMock.Setup(m => m.CurrentUser).Returns((User)null);

        InvitationDto invitationDtoUpdated = _invitationLogic.Update(invitationCode, invitationToUpdate);

        Assert.AreEqual(invitationExpected, invitationDtoUpdated);
        _userLogicMock.VerifyAll();
        _invitationRepositoryMock.VerifyAll();
        _currentContextMock.VerifyAll();
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
        _invitationRepositoryMock.Setup(i => i.GetFirst(It.IsAny<Func<Invitation, bool>>())).Returns(userInvitation);

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

        _invitationRepositoryMock.SetupSequence(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
            .Returns(userInvitation)
            .Throws(new ResourceNotFoundException(""));
        _invitationRepositoryMock.Setup(m => m.Update(It.IsAny<Invitation>())).Callback(() => { });
        _userLogicMock.Setup(m => m.GetFirst(It.IsAny<Func<User, bool>>()))
            .Throws(new ResourceNotFoundException(""));
        _currentContextMock.Setup(m => m.CurrentUser).Returns(new User()
        {
            Role = new Role()
            {
                Name = Role.ADMIN
            }
        });
        _pharmacyLogicMock.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(new Pharmacy()
        {
            Name = invitationToUpdate.PharmacyName
        });
        _roleLogicMock.Setup(m => m.GetRoleByName(It.IsAny<string>())).Returns(new Role()
        {
            Name = invitationToUpdate.RoleName
        });

        InvitationDto invitationDtoUpdated = _invitationLogic.Update(invitationCode, invitationToUpdate);

        Assert.AreEqual(invitationToUpdate.UserName, invitationDtoUpdated.UserName);
        Assert.AreEqual(invitationToUpdate.PharmacyName, invitationDtoUpdated.PharmacyName);
        Assert.AreEqual(invitationToUpdate.RoleName, invitationDtoUpdated.RoleName);
        Assert.IsNotNull(invitationDtoUpdated.Code);
        _userLogicMock.VerifyAll();
        _invitationRepositoryMock.VerifyAll();
        _currentContextMock.VerifyAll();
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
        _invitationRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Invitation, bool>>()))
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
        _invitationRepositoryMock.Setup(i => i.GetFirst(It.IsAny<Func<Invitation, bool>>())).Returns(userInvitation);
        _invitationRepositoryMock.Setup(i => i.Delete(It.IsAny<Invitation>())).Callback(() => { });
        _currentContextMock.Setup(c => c.CurrentUser).Returns((User)null);

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
        QueryInvitationDto queryInvitationDto = new QueryInvitationDto()
        {

        };
        _invitationRepositoryMock.Setup(s => s.GetAll(It.IsAny<Func<Invitation, bool>>())).Returns(invitationItems);

        List<Invitation> invitationsReturned = _invitationLogic.GetInvitations(queryInvitationDto).ToList();

        CollectionAssert.AreEqual(invitationItems, invitationsReturned);
        _invitationRepositoryMock.VerifyAll();
    }


    [TestMethod]
    public void TestGetInvitationsByPharmacyName()
    {

        QueryInvitationDto queryInvitationDto = new QueryInvitationDto()
        {
            PharmacyName = "Farmashop"
        };

        Invitation userInvitation = new Invitation
        {
            Id = 1,
            Pharmacy = new Pharmacy()
            {
                Id = 1,
                Name = "Farmashop",
            },
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


        _pharmacyLogicMock.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(new Pharmacy()
        {
            Name = userInvitation.Pharmacy.Name
        });
        _invitationRepositoryMock.Setup(s => s.GetAll(It.IsAny<Func<Invitation, bool>>())).Returns(invitationItems);
        List<Invitation> invitationsReturned = _invitationLogic.GetInvitations(queryInvitationDto).ToList();

        CollectionAssert.AreEqual(invitationItems, invitationsReturned);
        _invitationRepositoryMock.VerifyAll();
        _pharmacyLogicMock.VerifyAll();

    }

    [TestMethod]
    public void TestGetInvitationsByUserName()
    {

        QueryInvitationDto queryInvitationDto = new QueryInvitationDto()
        {
            UserName = "Cris01"
        };

        Invitation userInvitation = new Invitation
        {
            Id = 1,
            Pharmacy = new Pharmacy()
            {
                Id = 1,
                Name = "Farmashop",
            },
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
        _invitationRepositoryMock.Setup(i => i.GetAll(It.IsAny<Func<Invitation, bool>>())).Returns(invitationItems);
        _invitationRepositoryMock.Setup(i => i.GetFirst(It.IsAny<Func<Invitation, bool>>())).Returns(userInvitation);

        List<Invitation> invitationsReturned = _invitationLogic.GetInvitations(queryInvitationDto).ToList();

        CollectionAssert.AreEqual(invitationItems, invitationsReturned);
        _invitationRepositoryMock.VerifyAll();

    }

    [TestMethod]
    public void TestGetInvitationsByRole()
    {

        QueryInvitationDto queryInvitationDto = new QueryInvitationDto()
        {
            Role = "Employee"
        };

        Invitation userInvitation = new Invitation
        {
            Id = 1,
            Pharmacy = new Pharmacy()
            {
                Id = 1,
                Name = "Farmashop",
            },
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
        _roleLogicMock.Setup(m => m.GetRoleByName(queryInvitationDto.Role)).Returns(new Role()
        {
            Name = userInvitation.Role.Name
        });

        _invitationRepositoryMock.Setup(i => i.GetAll(It.IsAny<Func<Invitation, bool>>())).Returns(invitationItems);

        List<Invitation> invitationsReturned = _invitationLogic.GetInvitations(queryInvitationDto).ToList();

        CollectionAssert.AreEqual(invitationItems, invitationsReturned);
        _invitationRepositoryMock.VerifyAll();

    }
    //TODO: ver si esta exception o ResourceNotFound
    
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void GetInvitationsByNotExistantPharmacy()
    {
        QueryInvitationDto queryInvitationDto = new QueryInvitationDto()
        {
            PharmacyName = "NotExistantPharmacy"
        };
        Invitation userInvitation = new Invitation
        {
            Id = 1,
            Pharmacy = new Pharmacy()
            {
                Id = 1,
                Name = "Farmashop",
            },
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


        _pharmacyLogicMock.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Throws(new ResourceNotFoundException(""));
        _invitationRepositoryMock.Setup(s => s.GetAll(It.IsAny<Func<Invitation, bool>>())).Returns(invitationItems);


        List<Invitation> invitationsReturned = _invitationLogic.GetInvitations(queryInvitationDto).ToList();
    }

    [TestMethod]
    [ExpectedException(typeof(ResourceNotFoundException))]
    public void GetInvitationsByNotExistantUser()
    {
        QueryInvitationDto queryInvitationDto = new QueryInvitationDto()
        {
            UserName = "NotExistantUser"
        };
        Invitation userInvitation = new Invitation
        {
            Id = 1,
            Pharmacy = new Pharmacy()
            {
                Id = 1,
                Name = "Farmashop",
            },
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
        _invitationRepositoryMock.Setup(i => i.GetFirst(It.IsAny<Func<Invitation, bool>>())).Throws(new ResourceNotFoundException(""));
        _invitationRepositoryMock.Setup(s => s.GetAll(It.IsAny<Func<Invitation, bool>>())).Returns(invitationItems);


        List<Invitation> invitationsReturned = _invitationLogic.GetInvitations(queryInvitationDto).ToList();

    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void GetInvitationsByInvalidRole()
    {
        QueryInvitationDto queryInvitationDto = new QueryInvitationDto()
        {
            Role = "Engineer"
        };
        Invitation userInvitation = new Invitation
        {
            Id = 1,
            Pharmacy = new Pharmacy()
            {
                Id = 1,
                Name = "Farmashop",
            },
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
        _roleLogicMock.Setup(r => r.GetRoleByName(It.IsAny<string>())).Throws(new ValidationException(""));
        _invitationRepositoryMock.Setup(s => s.GetAll(It.IsAny<Func<Invitation, bool>>())).Returns(invitationItems);


        List<Invitation> invitationsReturned = _invitationLogic.GetInvitations(queryInvitationDto).ToList();

    }
}

