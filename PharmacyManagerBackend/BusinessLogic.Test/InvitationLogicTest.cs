using System;
using Domain;
using Domain.Dtos;
using Exceptions;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogic.Test
{
    [TestClass]
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
            _invitationRepository.Setup(m => m.GetInvitationByCode(It.IsAny<string>())).Throws(new ResourceNotFoundException(""));
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
            _invitationRepository.SetupSequence(m => m.GetInvitationByCode(It.IsAny<string>()))
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
    }
}
