
using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Test.Utils;

namespace WebApi.Test
{
    [TestClass]
    public class UsersControllerTest
    {
        private Mock<IUserLogic> _userLogicMock;
        private UsersController _userApiController;
        private User _user;

        [TestInitialize]
        public void InitTest()
        {
            _userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            _userApiController = new UsersController(_userLogicMock.Object);
            _user = new User()
            {
                Id = 1,
                UserName = "Usuario1",
                Email = "ususario@user.com",
                Address = "Cuareim 123",
                Password = "Usuario+1",
                Pharmacy = new Pharmacy()
                {
                    Name = "Pharmashop"
                },
                Role = new Role()
                {
                    Name = "Employee"
                }
            };
        }

        [TestMethod]
        public void CreateUserOk()
        {
            _userLogicMock.Setup(m => m.Create(It.IsAny<UserDto>())).Returns(_user);
            var userToCreate = new UserRequestModel()
            {
                UserName = "Usuario1",
                Email = "ususario@user.com",
                Address = "Cuareim 123",
                InvitationCode = "123456",
                Password = "Usuario+1"
            };

            var result = _userApiController.Create(userToCreate);
            var okResult = result as OkObjectResult;
            var createdUserModel = okResult.Value as UserResponseModel;

            Assert.AreEqual(_user.Pharmacy.Name, createdUserModel.PharmacyName);
            Assert.AreEqual(_user.Role.Name, createdUserModel.Role);
            Assert.AreEqual(_user.Id, createdUserModel.Id);
            Assert.IsTrue(ModelsComparer.UserCompare(userToCreate, createdUserModel));
            _userLogicMock.VerifyAll();
        }

    }
}
