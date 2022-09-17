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

        [TestInitialize]
        public void Initialize()
        {
            this._invitationRepository = new Mock<IBaseRepository<Invitation>>(MockBehavior.Strict);
            this._invitationLogic = new InvitationLogic(this._invitationRepository.Object);
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
            _invitationRepository.Setup(m => m.Create(invitationToCreate)).Returns(invitationRepository);

            Invitation createdInvitation = _invitationLogic.Create(invitationToCreate);

            Assert.AreEqual(invitationRepository.Id, createdInvitation.Id);
            Assert.AreEqual(invitationRepository.UserName, createdInvitation.UserName);
            Assert.AreEqual(invitationRepository.Role.Name, createdInvitation.Role.Name);
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
    }
}
