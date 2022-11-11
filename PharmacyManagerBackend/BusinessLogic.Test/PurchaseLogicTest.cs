using Domain;
using Domain.Dtos;
using Exceptions;
using IDataAccess;
using Moq;
using System.Net;

namespace BusinessLogic.Test;

[TestClass]
public class PurchaseLogicTest
{
    private PurchaseLogic _purchaseLogic;
    private Mock<IPurchaseRepository> _purchaseRepositoryMock;
    private Mock<PharmacyLogic> _pharmacyLogicMock;
    private Mock<DrugLogic> _drugLogicMock;
    private Mock<Context> _context;

    [TestInitialize]
    public void Initialize()
    {
        this._context = new Mock<Context>(MockBehavior.Strict);
        this._purchaseRepositoryMock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
        this._pharmacyLogicMock = new Mock<PharmacyLogic>(MockBehavior.Strict, null);
        this._drugLogicMock = new Mock<DrugLogic>(MockBehavior.Strict, null, null, null, null);
        this._purchaseLogic = new PurchaseLogic(this._purchaseRepositoryMock.Object,
            this._pharmacyLogicMock.Object,
            this._drugLogicMock.Object,
            this._context.Object);
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
            Drugs = new List<Drug>() { drug }
        };
        Purchase purchaseToCreate = new Purchase()
        {
            UserEmail = "email@email.com",
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = new Drug()
                    {
                        DrugCode = drugCode
                    },
                    Pharmacy = new Pharmacy()
                    {
                        Name = pharmacyName
                    },
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
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = drug,
                    Pharmacy = new Pharmacy()
                    {
                        Name = pharmacyName,
                        Drugs = new List<Drug>(){drugRepository}
                    },
                },
            }
        };
        _pharmacyLogicMock.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(pharmacyRepository);
        _purchaseRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Purchase, bool>>())).Throws(new ResourceNotFoundException(""));
        _purchaseRepositoryMock.Setup(m => m.Create(It.IsAny<Purchase>())).Returns(purchaseRepository);

        Purchase purchaseCreated = _purchaseLogic.Create(purchaseToCreate);

        Assert.AreEqual(purchaseRepository, purchaseCreated);
        _pharmacyLogicMock.VerifyAll();
        _purchaseRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void CreatePurchaseRepeteadCodeOk()
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
            Drugs = new List<Drug>() { drug }
        };
        Purchase purchaseToCreate = new Purchase()
        {
            UserEmail = "email@email.com",
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = new Drug()
                    {
                        DrugCode = drugCode
                    },
                    Pharmacy = new Pharmacy()
                    {
                        Name = pharmacyName
                    },
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
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = drug,
                    Pharmacy = new Pharmacy()
                    {
                        Name = pharmacyName,
                        Drugs = new List<Drug>(){drugRepository}
                    },
                },
            }
        };
        _pharmacyLogicMock.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(pharmacyRepository);
        _purchaseRepositoryMock.SetupSequence(m => m.GetFirst(It.IsAny<Func<Purchase, bool>>()))
            .Returns(new Purchase() { })
            .Throws(new ResourceNotFoundException(""));
        _purchaseRepositoryMock.Setup(m => m.Create(It.IsAny<Purchase>())).Returns(purchaseRepository);

        Purchase purchaseCreated = _purchaseLogic.Create(purchaseToCreate);

        Assert.AreEqual(purchaseRepository, purchaseCreated);
        _pharmacyLogicMock.VerifyAll();
        _purchaseRepositoryMock.VerifyAll();
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
                DrugId = 1,
                Pharmacy = new Pharmacy()
                {
                    Name = "PharmacyName"
                }
            }
        };
        Purchase purchase = new Purchase()
        {
            UserEmail = "email@email.com",
            Items = items,
        };
        _purchaseRepositoryMock.Setup(m => m.Create(It.IsAny<Purchase>())).Returns(purchase);
        _pharmacyLogicMock.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Throws(new ResourceNotFoundException(""));

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
                Drug = drug,
                Pharmacy = pharmacy
            }
        };
        Purchase purchase = new Purchase()
        {
            UserEmail = "email@email.com",
            Items = items
        };
        _pharmacyLogicMock.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(pharmacy);
        _purchaseRepositoryMock.Setup(m => m.Create(It.IsAny<Purchase>())).Returns(purchase);

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
            Drugs = new List<Drug>() { drug }
        };
        Purchase purchaseToCreate = new Purchase()
        {
            UserEmail = "email@email.com",
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 3,
                    Drug = new Drug()
                    {
                        DrugCode = drugCode
                    },
                    Pharmacy = new Pharmacy()
                    {
                        Name = pharmacyName
                    },
                }
            }
        };
        _pharmacyLogicMock.Setup(m => m.GetPharmacyByName(It.IsAny<string>())).Returns(pharmacyRepository);
        _purchaseRepositoryMock.Setup(m => m.Create(It.IsAny<Purchase>())).Returns(purchaseToCreate);

        _purchaseLogic.Create(purchaseToCreate);
    }

    [TestMethod]
    public void GetPurchaseReportOk()
    {
        string drugCode = "A01";
        string drugName = "Perifar";
        string pharmacyName = "PharmacyName";
        Drug drugRepository = new Drug()
        {
            DrugCode = drugCode,
            Stock = 20,
            PharmacyId = 1,
            Price = 100,
            DrugInfo = new DrugInfo()
            {
                Name = drugName
            }
        };
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Id = 1,
            Name = pharmacyName,
            Drugs = new List<Drug>() { drugRepository }
        };

        Purchase purchaseRepository1 = new Purchase()
        {
            Id = 1,
            TotalPrice = 200,
            UserEmail = "email@email.com",
            Date = new DateTime(2022 - 09 - 30),
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = drugRepository,
                    PharmacyId = pharmacyRepository.Id,
                    State = PurchaseState.PENDING
                },
            },
        };
        Purchase purchaseRepository2 = new Purchase()
        {
            Id = 2,
            TotalPrice = 300,
            UserEmail = "email2@email.com",
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 3,
                    Drug = drugRepository,
                    PharmacyId = pharmacyRepository.Id,
                    State = PurchaseState.PENDING
                },
            },
        };

        List<Purchase> purchasesRepository = new List<Purchase>() { purchaseRepository1, purchaseRepository2 };
        double totalPrice = purchaseRepository1.TotalPrice + purchaseRepository2.TotalPrice;
        User currentUser = new User()
        {
            Role = new Role()
            {
                Name = Role.EMPLOYEE
            },
            Pharmacy = pharmacyRepository
        };
        QueryPurchaseDto queryPurchaseDto = new QueryPurchaseDto()
        {
            DateFrom = "2022-09-01",
            DateTo = "2022-10-30"
        };
        _purchaseRepositoryMock.Setup(m => m.GetAll(It.IsAny<Func<Purchase, bool>>())).Returns(purchasesRepository);
        _context.Setup(m => m.CurrentUser).Returns(currentUser);

        PurchaseReportDto purchasesReport = _purchaseLogic.GetPurchasesReport(queryPurchaseDto);

        PurchaseItemReportDto purchaseItemReportDto = new PurchaseItemReportDto()
        {
            Name = drugCode + " - " + drugName,
            Quantity = 5,
            Amount = totalPrice
        };

        List<PurchaseItemReportDto> purchaseItemReportRepository = new List<PurchaseItemReportDto>() {
            purchaseItemReportDto
        };

        Assert.AreEqual(totalPrice, purchasesReport.TotalPrice);
        CollectionAssert.AreEqual(purchaseItemReportRepository, purchasesReport.Purchases.ToList());

        _purchaseRepositoryMock.VerifyAll();
        _context.VerifyAll();
    }

    [TestMethod]
    public void UpdatePurchaseOk()
    {
        string drugCode = "A01";
        string pharmacyName = "PharmacyName";
        Drug drug = new Drug()
        {
            DrugCode = drugCode,
            Stock = 2,
            PharmacyId = 1
        };
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Id = 1,
            Name = pharmacyName,
            Drugs = new List<Drug>() { drug }
        };
        User currentUser = new User()
        {
            Role = new Role()
            {
                Name = Role.EMPLOYEE
            },
            Pharmacy = pharmacyRepository
        };
        Purchase purchaseRepository = new Purchase()
        {
            Id = 1,
            TotalPrice = 100.50,
            UserEmail = "email@email.com",
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = drug,
                    Pharmacy = pharmacyRepository,
                    PharmacyId = 1,
                    State = PurchaseState.PENDING
                },
            },
        };
        Purchase purchaseToUpdate = new Purchase()
        {
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Drug = new Drug()
                    {
                        DrugCode = drugCode
                    },
                    State = PurchaseState.ACCEPTED
                }
            }
        };
        _purchaseRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Purchase, bool>>())).Returns(purchaseRepository);
        _purchaseRepositoryMock.Setup(m => m.Update(It.IsAny<Purchase>()));
        _context.Setup(m => m.CurrentUser).Returns(currentUser);
        _drugLogicMock.Setup(m => m.Update(It.IsAny<int>(), It.IsAny<Drug>())).Returns(drug);

        Purchase purchaseUpdated = _purchaseLogic.Update(1, purchaseToUpdate);

        Assert.AreEqual(purchaseUpdated.Items[0].State, PurchaseState.ACCEPTED);
        Assert.AreEqual(purchaseUpdated.Items[0].Drug.Stock, 0);
        _purchaseRepositoryMock.VerifyAll();
        _context.VerifyAll();
        _drugLogicMock.VerifyAll();
    }

    [TestMethod]
    [ExpectedException(typeof(ResourceNotFoundException))]
    public void UpdateNotExistantPurchase()
    {
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Id = 1,
            Name = "pharmacyName",
        };
        User currentUser = new User()
        {
            Role = new Role()
            {
                Name = Role.EMPLOYEE
            },
            Pharmacy = pharmacyRepository
        };
        Purchase purchaseToUpdate = new Purchase()
        {
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Drug = new Drug()
                    {
                        DrugCode = "drugCode"
                    },
                    State = PurchaseState.ACCEPTED
                }
            }
        };
        _purchaseRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Purchase, bool>>())).Throws(new ResourceNotFoundException(""));
        _context.Setup(m => m.CurrentUser).Returns(currentUser);

        _purchaseLogic.Update(1, purchaseToUpdate);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void UpdatePurchaseWithNotExitentItem()
    {
        string drugCode = "A01";
        string pharmacyName = "PharmacyName";
        Drug drug = new Drug()
        {
            DrugCode = drugCode,
            Stock = 2,
            PharmacyId = 1
        };
        Drug drug2 = new Drug()
        {
            DrugCode = "A02",
            Stock = 2,
            PharmacyId = 1
        };
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Id = 1,
            Name = pharmacyName,
            Drugs = new List<Drug>() { drug }
        };
        User currentUser = new User()
        {
            Role = new Role()
            {
                Name = Role.EMPLOYEE
            },
            Pharmacy = pharmacyRepository
        };
        Purchase purchaseRepository = new Purchase()
        {
            Id = 1,
            TotalPrice = 100.50,
            UserEmail = "email@email.com",
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = drug2,
                    Pharmacy = pharmacyRepository,
                    PharmacyId = 1,
                    State = PurchaseState.PENDING
                },
            },
        };
        Purchase purchaseToUpdate = new Purchase()
        {
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Drug = new Drug()
                    {
                        DrugCode = drugCode
                    },
                    State = PurchaseState.ACCEPTED
                }
            }
        };
        _purchaseRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Purchase, bool>>())).Returns(purchaseRepository);
        _purchaseRepositoryMock.Setup(m => m.Update(It.IsAny<Purchase>()));
        _context.Setup(m => m.CurrentUser).Returns(currentUser);

        _purchaseLogic.Update(1, purchaseToUpdate);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void UpdatePurchaseWithoutStock()
    {
        string drugCode = "A01";
        string pharmacyName = "PharmacyName";
        Drug drug = new Drug()
        {
            DrugCode = drugCode,
            Stock = 0,
            PharmacyId = 1
        };
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Id = 1,
            Name = pharmacyName,
            Drugs = new List<Drug>() { drug }
        };
        User currentUser = new User()
        {
            Role = new Role()
            {
                Name = Role.EMPLOYEE
            },
            Pharmacy = pharmacyRepository
        };
        Purchase purchaseRepository = new Purchase()
        {
            Id = 1,
            TotalPrice = 100.50,
            UserEmail = "email@email.com",
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = drug,
                    Pharmacy = pharmacyRepository,
                    PharmacyId = 1,
                    State = PurchaseState.PENDING
                },
            },
        };
        Purchase purchaseToUpdate = new Purchase()
        {
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Drug = new Drug()
                    {
                        DrugCode = drugCode
                    },
                    State = PurchaseState.ACCEPTED
                }
            }
        };
        _purchaseRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Purchase, bool>>())).Returns(purchaseRepository);
        _purchaseRepositoryMock.Setup(m => m.Update(It.IsAny<Purchase>()));
        _context.Setup(m => m.CurrentUser).Returns(currentUser);

        _purchaseLogic.Update(1, purchaseToUpdate);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void UpdatePurchaseAlreadyAceptedItem()
    {
        string drugCode = "A01";
        string pharmacyName = "PharmacyName";
        Drug drug = new Drug()
        {
            DrugCode = drugCode,
            Stock = 2,
            PharmacyId = 1
        };
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Id = 1,
            Name = pharmacyName,
            Drugs = new List<Drug>() { drug }
        };
        User currentUser = new User()
        {
            Role = new Role()
            {
                Name = Role.EMPLOYEE
            },
            Pharmacy = pharmacyRepository
        };
        Purchase purchaseRepository = new Purchase()
        {
            Id = 1,
            TotalPrice = 100.50,
            UserEmail = "email@email.com",
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = drug,
                    Pharmacy = pharmacyRepository,
                    PharmacyId = 1,
                    State = PurchaseState.ACCEPTED
                },
            },
        };
        Purchase purchaseToUpdate = new Purchase()
        {
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Drug = new Drug()
                    {
                        DrugCode = drugCode
                    },
                    State = PurchaseState.ACCEPTED
                }
            }
        };
        _purchaseRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Purchase, bool>>())).Returns(purchaseRepository);
        _purchaseRepositoryMock.Setup(m => m.Update(It.IsAny<Purchase>()));
        _context.Setup(m => m.CurrentUser).Returns(currentUser);

        _purchaseLogic.Update(1, purchaseToUpdate);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void UpdatePurchaseAlreadyRejectedItem()
    {
        string drugCode = "A01";
        string pharmacyName = "PharmacyName";
        Drug drug = new Drug()
        {
            DrugCode = drugCode,
            Stock = 2,
            PharmacyId = 1
        };
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Id = 1,
            Name = pharmacyName,
            Drugs = new List<Drug>() { drug }
        };
        User currentUser = new User()
        {
            Role = new Role()
            {
                Name = Role.EMPLOYEE
            },
            Pharmacy = pharmacyRepository
        };
        Purchase purchaseRepository = new Purchase()
        {
            Id = 1,
            TotalPrice = 100.50,
            UserEmail = "email@email.com",
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = drug,
                    Pharmacy = pharmacyRepository,
                    PharmacyId = 1,
                    State = PurchaseState.REJECTED
                },
            },
        };
        Purchase purchaseToUpdate = new Purchase()
        {
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Drug = new Drug()
                    {
                        DrugCode = drugCode
                    },
                    State = PurchaseState.ACCEPTED
                }
            }
        };
        _purchaseRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Purchase, bool>>())).Returns(purchaseRepository);
        _purchaseRepositoryMock.Setup(m => m.Update(It.IsAny<Purchase>()));
        _context.Setup(m => m.CurrentUser).Returns(currentUser);

        _purchaseLogic.Update(1, purchaseToUpdate);
    }

    [TestMethod]
    public void UpdateRejectedPurchaseOk()
    {
        string drugCode = "A01";
        string pharmacyName = "PharmacyName";
        Drug drug = new Drug()
        {
            DrugCode = drugCode,
            Stock = 2,
            PharmacyId = 1
        };
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Id = 1,
            Name = pharmacyName,
            Drugs = new List<Drug>() { drug }
        };
        User currentUser = new User()
        {
            Role = new Role()
            {
                Name = Role.EMPLOYEE
            },
            Pharmacy = pharmacyRepository
        };
        Purchase purchaseRepository = new Purchase()
        {
            Id = 1,
            TotalPrice = 100.50,
            UserEmail = "email@email.com",
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = drug,
                    Pharmacy = pharmacyRepository,
                    PharmacyId = 1,
                    State = PurchaseState.PENDING
                },
            },
        };
        Purchase purchaseToUpdate = new Purchase()
        {
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Drug = new Drug()
                    {
                        DrugCode = drugCode
                    },
                    State = PurchaseState.REJECTED
                }
            }
        };
        _purchaseRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Purchase, bool>>())).Returns(purchaseRepository);
        _purchaseRepositoryMock.Setup(m => m.Update(It.IsAny<Purchase>()));
        _context.Setup(m => m.CurrentUser).Returns(currentUser);

        Purchase purchaseUpdated = _purchaseLogic.Update(1, purchaseToUpdate);

        Assert.AreEqual(purchaseUpdated.Items[0].State, PurchaseState.REJECTED);
        _purchaseRepositoryMock.VerifyAll();
        _context.VerifyAll();
    }

    [TestMethod]
    public void GetPurchaseOk()
    {
        string drugCode = "A01";
        string pharmacyName = "PharmacyName";
        Drug drug = new Drug()
        {
            DrugCode = drugCode,
            Stock = 2,
            PharmacyId = 1
        };
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Id = 1,
            Name = pharmacyName,
            Drugs = new List<Drug>() { drug }
        };
        Purchase purchaseRepository = new Purchase()
        {
            Id = 1,
            Code = "1234",
            TotalPrice = 100.50,
            UserEmail = "email@email.com",
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Quantity = 2,
                    Drug = drug,
                    Pharmacy = pharmacyRepository,
                    PharmacyId = 1,
                    State = PurchaseState.PENDING
                },
            },
        };
        _purchaseRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Func<Purchase, bool>>())).Returns(purchaseRepository);

        Purchase purchaseObtained = _purchaseLogic.Get("1234");

        Assert.AreEqual(purchaseRepository, purchaseObtained);
        _purchaseRepositoryMock.VerifyAll();
    }

}