using Castle.Core.Resource;
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
  private Mock<PharmacyLogic> _pharmacyLogic;
  
  [TestInitialize]
  public void Initialize()
  {
    this._purchaseRepository = new Mock<IPurchaseRepository>(MockBehavior.Strict);
    this._pharmacyLogic = new Mock<PharmacyLogic>(MockBehavior.Strict, null);
    this._purchaseLogic = new PurchaseLogic(this._purchaseRepository.Object, this._pharmacyLogic.Object);
  }
  
  [TestMethod]
  public void CreatePurchaseOk()
  {
    Drug drug = new Drug()
    {
      DrugCode = "A01"
    };
    Pharmacy pharmacy = new Pharmacy()
    {
      Id = 1,
      Name = "PharmacyName"
    };
    List<PurchaseItem> items = new List<PurchaseItem>()
    {
      new PurchaseItem()
      {
        Quantity = 1,
        DrugId = 1,
        Drug = drug
      }
    };
    Purchase purchase = new Purchase()
    {
      UserEmail = "email@email.com",
      Items = items,
      Pharmacy = pharmacy
    };
    _pharmacyLogic.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(pharmacy);
    _purchaseRepository.Setup(m => m.Create(It.IsAny<Purchase>())).Returns(purchase);
    _pharmacyLogic.Setup(m => m.GetDrug(It.IsAny<int>(), It.IsAny<string>())).Returns(drug);
    
    _purchaseLogic.Create(purchase);
    
    _pharmacyLogic.VerifyAll();
    _purchaseRepository.VerifyAll();
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
    _pharmacyLogic.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Throws(new ResourceNotFoundException(""));
    
    _purchaseLogic.Create(purchase);
  }


  [TestMethod]
  [ExpectedException(typeof(ValidationException))]
  public void CreatePurchaseWithNotExistentDrugShouldFail()
  {
    Pharmacy pharmacy = new Pharmacy()
    {
      Id = 1,
      Name = "PharmacyName"
    };
    Drug drug = new Drug()
    {
      DrugCode = "A01"
    };
    List<PurchaseItem> items = new List<PurchaseItem>()
    {
      new PurchaseItem()
      {
        Quantity = 1,
        DrugId = 1,
        Drug = drug
      }
    };
    Purchase purchase = new Purchase()
    {
      UserEmail = "email@email.com",
      Items = items,
      Pharmacy = pharmacy
    };
    _pharmacyLogic.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(pharmacy);
    _pharmacyLogic.Setup(m => m.GetDrug(It.IsAny<int>(), It.IsAny<string>())).Throws(new ResourceNotFoundException(""));
    _purchaseRepository.Setup(m => m.Create(It.IsAny<Purchase>())).Returns(purchase);
    
    _purchaseLogic.Create(purchase);
  }
}