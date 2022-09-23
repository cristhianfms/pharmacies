
using Domain;
using Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.Test.Dtos;

[TestClass]
public class RoleTest
{
    [TestMethod]
    public void CheckValidRole()
    {
        Role role = new Role()
        {
            Name = "Admin"
        };
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void CheckEmptyNameThrowsException()
    {
        Role role = new Role()
        {
            Name = ""
        };
    }
}
