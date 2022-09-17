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
        private Mock<IBaseRepository<Invitation>> _invitationRepository;
        private Mock<UserLogic> _userLogic;

        [TestInitialize]
        public void Initialize()
        {
            this._userLogic = new Mock<UserLogic>(MockBehavior.Strict);
            this._invitationRepository = new Mock<IBaseRepository<Invitation>>(MockBehavior.Strict);
            this._invitationLogic = new InvitationLogic(this._invitationRepository.Object, _userLogic.Object);
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
                }
            };
            Invitation invitationToCreate = new Invitation()
            {
                UserName = "cris01",
                Role = new Role()
                {
                    Name = "Employee"
                }
            };
            _userLogic.Setup(m => m.GetUserByUserName(invitationToCreate.UserName)).Throws(new ResourceNotFoundException(""));
            _invitationRepository.Setup(m => m.Create(invitationToCreate)).Returns(invitationRepository);

            Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);

            Assert.AreEqual(invitationRepository.Id, createdInvitation.Id);
            Assert.AreEqual(invitationRepository.UserName, createdInvitation.UserName);
            Assert.AreEqual(invitationRepository.Role.Name, createdInvitation.Role.Name);
            _userLogic.VerifyAll();
            _invitationRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateInvitationWithoutNameShouldThrowError()
        {
            Invitation invitationToCreate = new Invitation()
            {
                Role = new Role()
                {
                    Name = "Employee"
                }
            };

            Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);
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
            Invitation invitationToCreate = new Invitation()
            {
                UserName = "cris01",
                Role = new Role()
                {
                    Name = "Employee"
                }
            };

            _userLogic.Setup(m => m.GetUserByUserName(invitationToCreate.UserName)).Returns(new User());
            _invitationRepository.Setup(m => m.Create(invitationToCreate)).Returns(invitationRepository);

            Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateInvitationWithoutRoleShouldThrowError()
        {
            Invitation invitationToCreate = new Invitation()
            {
                UserName = "cris01",
            };

            Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);
        }
    }
}
