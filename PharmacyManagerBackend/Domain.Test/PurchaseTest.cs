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
}