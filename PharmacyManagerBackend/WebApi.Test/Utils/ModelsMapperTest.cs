using System;
using AuthDomain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Test.Utils
{
    [TestClass]
    public class ModelsMapperTest
    {

        [TestMethod]
        public void SessionToSessionResponseModelOk()
        {
            Session session = new Session
            {
                Token = Guid.NewGuid()
            };

            SessionResponseModel sessionResponseModel = ModelsMapper.ToModel(session);

            Assert.AreEqual(session.Token, sessionResponseModel.Token);
        }

        [TestMethod]
        public void SessionRequestModelToSessionOK()
        {
            SessionRequestModel sessionRequestModel = new SessionRequestModel
            {
                UserName = "Cris",
                Password = "Cris.2022"
            };

            Session session = ModelsMapper.ToEntity(sessionRequestModel);

            Assert.AreEqual(session.User.UserName, sessionRequestModel.UserName);
            Assert.AreEqual(session.User.Password, sessionRequestModel.Password);
        }
    }
}