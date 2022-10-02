using Domain;
using Domain.Dtos;
using Moq;

namespace BusinessLogic.Test;

[TestClass]
public class PurchaseLogicTest
{
  private PurchaseLogic _purchaseLogic;
  private Mock<IPurchaseRepository> _purchaseRepository;
  
  [TestMethod]
  public void CreatePurchaseOk()
  {
    List<PurchaseItem> items = new List<PurchaseItem>()
    {
      new PurchaseItem()
      {
        Quantity = 1,
        DrugId = 1
      }
    };
    Purchase purchase = new Purchase()
    {
      UserEmail = "email@email.com",
      Items = items
    };

    PurchaseDto purchaseDto = new PurchaseDto()
    {

    };

    _purchaseLogic.Create(purchase);
  }
}