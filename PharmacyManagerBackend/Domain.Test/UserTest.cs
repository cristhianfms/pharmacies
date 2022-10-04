using Exceptions;

namespace Domain.Test;

[TestClass]
public class UserTest
{
    [TestMethod]
    public void SetPharmacyEmployee()
    {
        Pharmacy pharmacy = new Pharmacy()
        {
            Name = "Pharmacy"
        };
        User user = new User();
        user.Role = new Role()
        {
            Name = Role.EMPLOYEE
        };
        user.Pharmacy = pharmacy;

        Assert.AreEqual(pharmacy, user.Pharmacy);
    }

    [TestMethod]
    public void SetPharmacyOwner()
    {
        Pharmacy pharmacy = new Pharmacy()
        {
            Name = "Pharmacy"
        };
        User user = new User();
        user.Role = new Role()
        {
            Name = Role.OWNER
        };
        user.Pharmacy = pharmacy;

        Assert.AreEqual(pharmacy, user.Pharmacy);
    }

    [TestMethod]
    public void SetPharmacyAdmin()
    {
        Pharmacy pharmacy = new Pharmacy()
        {
            Name = "Pharmacy"
        };
        User user = new User();
        user.Role = new Role()
        {
            Name = Role.ADMIN
        };
        user.Pharmacy = pharmacy;
        var test = user.Pharmacy;

        Assert.IsNull(user.Pharmacy);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void UserWithEmptyPassword()
    {
        User user = new User()
        {
            Password = ""
        };
    }
    
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void UserWithShortPassword()
    {
        User user = new User()
        {
            Password = "abcd"
        };
    }
    
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void UserWithSpecialCharInPassword()
    {
        User user = new User()
        {
            Password = "abcdabcd1"
        };
    }
    
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void UserWithEmptyEmail()
    {
        User user = new User()
        {
            Email = ""
        };
    }
    
    
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void UserWithBadEmailFormat()
    {
        User user = new User()
        {
            Email = "asdfasdf@"
        };
    }
}