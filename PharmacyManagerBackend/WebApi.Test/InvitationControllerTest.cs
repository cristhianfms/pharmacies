using System.Collections.Generic;
using BusinessLogic;
using Domain;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Test.Utils;

namespace WebApi.Test
{
    [TestClass]
    public class InvitationControllerTest
    {
        private Mock<InvitationLogic> _invitationLogicMock;
        private InvitationController _invitationApiController;
        private Invitation _invitation;

        [TestInitialize]
        public void InitTest()
        {
            _invitationLogicMock = new Mock<InvitationLogic>(MockBehavior.Strict);
            _invitationApiController = new InvitationController(_invitationLogicMock.Object);
            _invitation = new Invitation()
            {
                
            };
        }


        [TestMethod]
        public void CreateInvitationOk()
        {
            _invitationLogicMock.Setup(m => m.Create(It.IsAny<Invitation>())).Returns(_invitation);
            var invitationModel = new InvitationModel()
            {
                
            };


            var result = _invitationApiController.Create(invitationModel);
            var okResult = result as OkObjectResult;
            var createdInvitation = okResult.Value as InvitationModel;

            Assert.IsTrue(ModelsComparer.InvitationCompare(invitationModel, createdInvitation));
            _invitationLogicMock.VerifyAll();
        }
    }
}
