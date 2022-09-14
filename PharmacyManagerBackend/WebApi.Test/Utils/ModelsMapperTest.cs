using System;
using Domain;
using Domain.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Test.Utils
{
    [TestClass]
    public class ModelsMapperTest
    {

        [TestMethod]
        public void CredentialsDtoToCredentialsModelOk()
        {
            TokenDto token = new TokenDto
            {
                Token = Guid.NewGuid()
            };

            TokenModel tokenModel = ModelsMapper.ToModel(token);

            Assert.AreEqual(token.Token, tokenModel.Token);
        }

        [TestMethod]
        public void SessionRequestModelToSessionOK()
        {
            CredentialsModel credentialsModel = new CredentialsModel
            {
                UserName = "Cris",
                Password = "Cris.2022"
            };

            CredentialsDto credentialsDto = ModelsMapper.ToEntity(credentialsModel);

            Assert.AreEqual(credentialsModel.UserName, credentialsDto.UserName);
            Assert.AreEqual(credentialsModel.Password, credentialsDto.Password);
        }
    }
}