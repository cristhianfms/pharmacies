using BusinessLogic.Dtos;
using Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.Test.DTO
{
    [TestClass]
    public class CredentialsDtoTest
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ValidateCredentialsWithNullPasswordThrowsException()
        {
            CredentialsDto credentialsDto = new CredentialsDto()
            {
                UserName = "ricardofort",
                Password = null
            };

            credentialsDto.ValidateNotNullCredentials();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ValidateCredentialsWithNullUserNameThrowsException()
        {
            CredentialsDto credentialsDto = new CredentialsDto()
            {
                UserName = null,
                Password = "test"
            };

            credentialsDto.ValidateNotNullCredentials();
        }

        [TestMethod]
        public void ValidateCredentialsOK()
        {
            CredentialsDto credentialsDto = new CredentialsDto()
            {
                UserName = "ricardofort",
                Password = "test"
            };

            credentialsDto.ValidateNotNullCredentials();
        }
    }
}