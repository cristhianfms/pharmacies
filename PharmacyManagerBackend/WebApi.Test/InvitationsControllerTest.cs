using System.Collections.Generic;
using Domain;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Test.Utils;
using IBusinessLogic;

namespace WebApi.Test
{
    [TestClass]
    public class InvitationsControllerTest
    {
        private Mock<IInvitationLogic> _invitationLogicMock;
        private InvitationController _invitationApiController;
        private Invitation _invitation;

        [TestInitialize]
        public void InitTest()
        {
            _invitationLogicMock = new Mock<IInvitationLogic>(MockBehavior.Strict);
            _invitationApiController = new InvitationController(_invitationLogicMock.Object);
            _invitation = new Invitation()
            {
                Id = 1,
                UserName = "JuanPerez",
                Role = new Role(),
                Code = "2A5678BX"
            };
        }


        [TestMethod]
        public void CreateInvitationOk()
        {
            _invitationLogicMock.Setup(m => m.Create(It.IsAny<Invitation>())).Returns(_invitation);
            var invitationModel = new InvitationModel()
            {
                Id = 1,
                UserName = "JuanPerez",
                Role = new Role(){
                    Name = "Admin"
                },
                Code = "2A5678BX"
            };


            var result = _invitationApiController.Create(invitationModel);
            var okResult = result as OkObjectResult;
            var createdInvitation = okResult.Value as InvitationModel;

            Assert.IsTrue(ModelsComparer.InvitationCompare(invitationModel, createdInvitation));
            _invitationLogicMock.VerifyAll();
        }
    }
}
