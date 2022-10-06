using Domain.Dtos;
using Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.Test.Dtos;

[TestClass]
public class CredentialsDtoTest
{
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void ValidateCredentialsWithEmptyPasswordThrowsException()
    {
        CredentialsDto credentialsDto = new CredentialsDto()
        {
            UserName = "ricardofort",
            Password = ""
        };
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void ValidateCredentialsWithEmptyUserNameThrowsException()
    {
        CredentialsDto credentialsDto = new CredentialsDto()
        {
            UserName = "",
            Password = "test"
        };
    }

    [TestMethod]
    public void ValidateCredentialsOK()
    {
        CredentialsDto credentialsDto = new CredentialsDto()
        {
            UserName = "ricardofort",
            Password = "test"
        };
    }
}
