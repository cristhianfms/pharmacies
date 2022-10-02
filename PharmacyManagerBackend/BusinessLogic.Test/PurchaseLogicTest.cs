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
    private Mock<DrugLogic> _drugLogic;

    [TestInitialize]
    public void Initialize()
    {
        this._purchaseRepository = new Mock<IPurchaseRepository>(MockBehavior.Strict);
        this._pharmacyLogic = new Mock<PharmacyLogic>(MockBehavior.Strict, null);
        this._drugLogic = new Mock<DrugLogic>(MockBehavior.Strict, null, null, null);
        this._purchaseLogic = new PurchaseLogic(this._purchaseRepository.Object, this._pharmacyLogic.Object, this._drugLogic.Object);
    }

    [TestMethod]
    public void CreatePurchaseOk()
    {
        string drugCode = "A01";
        string pharmacyName = "PharmacyName";
        Drug drug = new Drug()
        {
            DrugCode = drugCode,
            Stock = 2
        };
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Id = 1,
            Name = pharmacyName,
            Drugs = new List<Drug>(){drug}
        };
        Purchase purchaseToCreate = new Purchase()
        {
            UserEmail = "email@email.com",
            Pharmacy = new Pharmacy()
            {
                Name = pharmacyName
            },
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = new Drug()
                    {
                        DrugCode = drugCode
                    }
                }
            }
        };
        Drug drugRepository = new Drug()
        {
            DrugCode = drugCode
        };
        Purchase purchaseRepository = new Purchase()
        {
            Id = 1,
            TotalPrice = 100.50,
            UserEmail = "email@email.com",
            Pharmacy = new Pharmacy()
            {
                Name = pharmacyName,
                Drugs = new List<Drug>(){drugRepository}
            },
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = drug
                }
            }
        };
        _pharmacyLogic.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(pharmacyRepository);
        _purchaseRepository.Setup(m => m.Create(It.IsAny<Purchase>())).Returns(purchaseRepository);
        _drugLogic.Setup(m => m.Update(It.IsAny<int>(), It.IsAny<Drug>())).Returns(drug);

        Purchase purchaseCreated = _purchaseLogic.Create(purchaseToCreate);
        
        Assert.AreEqual(purchaseRepository, purchaseCreated);
        _pharmacyLogic.VerifyAll();
        _purchaseRepository.VerifyAll();
        _drugLogic.VerifyAll();
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
            Name = "PharmacyName",
            Drugs = new List<Drug>()
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
        _purchaseRepository.Setup(m => m.Create(It.IsAny<Purchase>())).Returns(purchase);

        _purchaseLogic.Create(purchase);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void CreatePurchaseWithNotStockDrugShouldFail()
    {
        string drugCode = "A01";
        string pharmacyName = "PharmacyName";
        Drug drug = new Drug()
        {
            DrugCode = drugCode,
            Stock = 2
        };
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Id = 1,
            Name = pharmacyName,
            Drugs = new List<Drug>(){drug}
        };
        Purchase purchaseToCreate = new Purchase()
        {
            UserEmail = "email@email.com",
            Pharmacy = new Pharmacy()
            {
                Name = pharmacyName
            },
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 3,
                    Drug = new Drug()
                    {
                        DrugCode = drugCode
                    }
                }
            }
        };
        _pharmacyLogic.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(pharmacyRepository);
        _purchaseRepository.Setup(m => m.Create(It.IsAny<Purchase>())).Returns(purchaseToCreate);

        _purchaseLogic.Create(purchaseToCreate);
    }

    [TestMethod]
    public void GetPurchaseReportOk()
    {
        string drugCode = "A01";
        string pharmacyName = "PharmacyName";
        Drug drugRepository = new Drug()
        {
            DrugCode = drugCode,
            Stock = 2
        };
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Id = 1,
            Name = pharmacyName,
            Drugs = new List<Drug>(){drugRepository}
        };

        Purchase purchaseRepository1 = new Purchase()
        {
            Id = 1,
            TotalPrice = 100.50,
            UserEmail = "email@email.com",
            Pharmacy = pharmacyRepository,
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = drugRepository
                }
            }
        };
        Purchase purchaseRepository2 = new Purchase()
        {
            Id = 2,
            TotalPrice = 200.99,
            UserEmail = "email2@email.com",
            Pharmacy = pharmacyRepository,
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = drugRepository
                }
            }
        };
        List<Purchase> purchasesRepository = new List<Purchase>() { purchaseRepository1, purchaseRepository2 };
        double totalPrice = purchaseRepository1.TotalPrice + purchaseRepository2.TotalPrice;

        Context context = new Context()
        {
            CurrentUser = new User()
            {
                Role = new Role()
                {
                    Name = Role.EMPLOYEE
                }
            }
        };
        QueryPurchaseDto queryPurchaseDto = new QueryPurchaseDto()
        {
            DateFrom = "2022-09-01",
            DateTo = "2022-09-30"
        };
        _purchaseRepository.Setup(m => m.GetAll(It.IsAny<Func<Purchase, bool>>())).Returns(purchasesRepository);
        
        _purchaseLogic.SetContext(context);
        PurchaseReportDto purchasesReport = _purchaseLogic.GetPurchasesReport(queryPurchaseDto);
        
        Assert.AreEqual(totalPrice, purchasesReport.TotalPrice);
        CollectionAssert.AreEqual(purchasesRepository, purchasesReport.Purchases.ToList());
    }
}