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
    public class UserLogicTest
    {
        private UserLogic _userLogic;
        private Mock<IUserRepository> _userRepository;
        private Mock<InvitationLogic> _invitationLogic;

        [TestInitialize]
        public void Initialize()
        {
            this._userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            this._invitationLogic = new Mock<InvitationLogic>(MockBehavior.Strict);
            _userLogic = new UserLogic(this._userRepository.Object, this._invitationLogic.Object);
        }

        [TestMethod]
        public void CreateNewUserOk()
        {
            string invitationCode = "123456";
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
            UserDto userToCreate = new UserDto()
            {
                UserName = userRepository.UserName,
                Email = userRepository.Email,
                Address = userRepository.Address,
                Password = userRepository.Password,
                InvitationCode = invitationCode,
            };
            Invitation userInvitation = new Invitation
            {
                Id = 1,
                UserName = "Cris01",
                Role = new Role()
                {
                    Name = "Admin"
                },
                Code = invitationCode
            };
            _invitationLogic.Setup(m => m.GetInvitationByCode(invitationCode)).Returns(userInvitation);
            _invitationLogic.Setup(m => m.Delete(userInvitation.Id)).Callback(() => { });
            _userRepository.Setup(m => m.Create(It.IsAny<User>())).Returns(userRepository);

            User userCreated = _userLogic.Create(userToCreate);

            Assert.AreEqual(userCreated, userRepository);
            _invitationLogic.VerifyAll();
            _userRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateNewUserWithoutInvitationShouldFail()
        {
            string invitationCode = "123456";
            UserDto userToCreate = new UserDto()
            {
                UserName = "Cris01",
                Email = "cris@gmail.com",
                Address = "calle a 123",
                Password = "pass.1234",
                InvitationCode = invitationCode,
            };
            _invitationLogic.Setup(m => m.GetInvitationByCode(invitationCode)).Throws(new ResourceNotFoundException(""));

            _userLogic.Create(userToCreate);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InvitationCodeForDiferentUserShouldFail()
        {
            string invitationCode = "123456";
            UserDto userToCreate = new UserDto()
            {
                UserName = "Cris01",
                Email = "cris@gmail.com",
                Address = "calle a 123",
                Password = "pass.1234",
                InvitationCode = invitationCode,
            };
            Invitation userInvitation = new Invitation
            {
                UserName = "OtherUserName",
                Role = new Role()
                {
                    Name = "Admin"
                },
                Code = invitationCode
            };
            _invitationLogic.Setup(m => m.GetInvitationByCode(invitationCode)).Returns(userInvitation);

            _userLogic.Create(userToCreate);
        }
    }
}
