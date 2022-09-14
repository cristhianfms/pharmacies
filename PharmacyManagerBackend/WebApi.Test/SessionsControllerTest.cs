using System;
using System.Collections.Generic;
using BusinessLogic;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApi.Test
{
    [TestClass]
    public class SessionsControllerTest
    {
        private Mock<SessionLogic> _sessionLogicMock;
        private SessionsController _sessionsApiController;

        [TestInitialize]
        public void InitTest()
        {
            _sessionLogicMock = new Mock<SessionLogic>(MockBehavior.Strict);
            _sessionsApiController = new SessionsController(_sessionLogicMock.Object);
        }
        
        [TestMethod]
        public void CreateSessionOk()
        {
            var user = new User 
            {
                UserName = "Cris",
                Password = "Cris.2022"
            };
            var session = new Session()
            {
                User = user,
                Token = Guid.NewGuid()
            };
            var sessionRequestModel = new SessionRequestModel()
            {
                UserName = user.UserName,
                Password = user.Password    
            };
            _sessionLogicMock.Setup(m => m.Create(It.IsAny<Session>())).Returns(session);


            var result = _sessionsApiController.Create(sessionRequestModel);
            var okResult = result as OkObjectResult;
            var sessionResponseModel = okResult.Value as SessionResponseModel;

            Assert.AreEqual(sessionResponseModel.Token, session.Token);
            _sessionLogicMock.VerifyAll();
        }

    }
}
