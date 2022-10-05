using Exceptions;

namespace Domain.Test;

[TestClass]
public class PurchaseTest
{
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void EmptyUserEmailShouldFail()
    {
        Purchase purchase = new Purchase()
        {
            UserEmail = ""
        };
    }
    
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void BadFormatUserEmailShouldFail()
    {
        Purchase purchase = new Purchase()
        {
            UserEmail = "test"
        };
    }
    
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void EmptyItemsListShouldFail()
    {
        Purchase purchase = new Purchase()
        {
            UserEmail = "mail@mail.com",
            Items = new List<PurchaseItem>()
        };
    }
}