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
}