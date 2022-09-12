using BusinessLogic;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApi.Test
{
    [TestClass]
    public class UserControllerTest
    {
        private Mock<UserLogic> _userLogicMock;
        private UserController _userApiController;
        private User _user;

        [TestInitialize]
        public void InitTest()
        {
            _userLogicMock = new Mock<UserLogic>(MockBehavior.Strict);
            _userApiController = new UserController(_userLogicMock.Object);
            _user = new User()
            {
                UserName = "Usuario1",
                Mail = "ususario@user.com",
                Address = "Cuareim 123",
                Password = "Usuario+1",
            };
        }

        [TestMethod]
        public void CreateUserOk()
        {
            _userLogicMock.Setup(m => m.Create(It.IsAny<User>())).Returns(_user);
            var userModel = new UserModel()
            {
                UserName = "Usuario1",
                Mail = "ususario@user.com",
                Address = "Cuareim 123",
            };

            var result = _userApiController.Create(userModel);
            var okResult = result as OkObjectResult;
            var createdUser = okResult.Value as UserModel;

            Assert.IsTrue(ModelsComparer.UserCompare(userModel, createdUser));
            _userLogicMock.VerifyAll();
        }

    }
}
