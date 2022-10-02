using Exceptions;

namespace Domain.Test;

[TestClass]
public class PurchaseItemTest
{
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void QuantityLessThanOneShouldFail()
    {
        PurchaseItem purchaseItem = new PurchaseItem()
        {
            Quantity = 0,
            Drug = new Drug(){}
        };
    }
}