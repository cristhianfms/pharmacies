using Domain;
using Domain.Dtos;
using Exceptions;
using IDataAccess;
using Moq;

namespace BusinessLogic.Test;

[TestClass]
public class PurchaseLogicTest
{
  private PurchaseLogic _purchaseLogic;
  private Mock<IPurchaseRepository> _purchaseRepository;
  
  [TestInitialize]
  public void Initialize()
  {
    this._purchaseRepository = new Mock<IPurchaseRepository>(MockBehavior.Strict);
    this._purchaseLogic = new PurchaseLogic(this._purchaseRepository.Object);
  }
  
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
    _purchaseRepository.Setup(m => m.Create(It.IsAny<Purchase>())).Returns(purchase);
    
    _purchaseLogic.Create(purchase);
  }
  
  [TestMethod]
  [ExpectedException(typeof(ValidationException))]
  public void CreatePurchaseWithNotExistantPharmacyShouldFail()
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
      Items = items,
      Pharmacy = new Pharmacy()
      {
        Name = "PharmacyName"
      }
    };
    _purchaseRepository.Setup(m => m.Create(It.IsAny<Purchase>())).Returns(purchase);
    
    _purchaseLogic.Create(purchase);
  }
}