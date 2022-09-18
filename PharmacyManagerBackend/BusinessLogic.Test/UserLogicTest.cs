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

        [TestInitialize]
        public void Initialize()
        {
            this._userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
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
                UserName = userRepository.UserName,
                Role = userRepository.Role,
                Email = userRepository.Email,
                Address = userRepository.Address,
                Password = userRepository.Password
            };
            _userRepository.Setup(m => m.Create(It.IsAny<User>())).Returns(userRepository);

            User userCreated = _userLogic.Create(userToCreate);

            Assert.AreEqual(userCreated, userRepository);
        }
    }
}
