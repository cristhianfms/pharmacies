
using Domain;
using Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.Test.Dtos
{
    [TestClass]
    public class InvitationTest
    {
        [TestMethod]
        public void CheckValidInvitation()
        {
            Invitation invitation = new Invitation()
            {
                UserName = "Cris01",
                Role = new Role()
                {
                    Name = "Employee"
                }
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CheckEmptyUserNameThrowsException()
        {
            Invitation invitation = new Invitation()
            {
                UserName = "",
                Role = new Role()
                {
                    Name = "Employee"
                }
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CheckEmptyRoleNameThrowsException()
        {
            Invitation invitation = new Invitation()
            {
                UserName = "Cris01",
                Role = new Role()
                {
                    Name = ""
                }
            };
        }
    }
}