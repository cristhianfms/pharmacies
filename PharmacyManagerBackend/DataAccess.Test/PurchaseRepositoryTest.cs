using System.Data.Common;
using DataAccess.Context;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Test;
[TestClass]
public class PurchaseRepositoryTest
{
    private DbConnection _connection;
    private BaseRepository<Purchase> _purchaseRepository;
    private BaseRepository<Pharmacy> _pharmacyRepository;
    private BaseRepository<Drug> _drugRepository;

    private PharmacyManagerContext _pharmacyManagerContext;
    private DbContextOptions<PharmacyManagerContext> _contextOptions;

    public PurchaseRepositoryTest()
    {
        this._connection = new SqliteConnection("Filename=:memory:");
        this._contextOptions = new DbContextOptionsBuilder<PharmacyManagerContext>().UseSqlite(this._connection).Options;

        this._pharmacyManagerContext = new PharmacyManagerContext(this._contextOptions);
        this._purchaseRepository = new BaseRepository<Purchase>(this._pharmacyManagerContext);
        this._pharmacyRepository = new BaseRepository<Pharmacy>(this._pharmacyManagerContext);
        this._drugRepository = new BaseRepository<Drug>(this._pharmacyManagerContext);
    }

    [TestInitialize]
    public void SetUp()
    {
        this._connection.Open();
        this._pharmacyManagerContext.Database.EnsureCreated();
    }

    [TestCleanup]
    public void CleanUp()
    {
        this._pharmacyManagerContext.Database.EnsureDeleted();
    }
    
    [TestMethod]
    public void GetAllPurchasesOK()
    {
        string drugCode = "A01";
        string pharmacyName = "PharmacyName";
        DrugInfo drugInfo = new DrugInfo()
        {
            Name = "Drug",
            Symptoms = "Symtoms",
            Presentation = "Presentation",
            QuantityPerPresentation = 1,
            UnitOfMeasurement = "Comprimidos"
        };
        Drug drugRepository = new Drug()
        {
            DrugCode = drugCode
        };
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Name = pharmacyName
        };
        Purchase purchaseRepository = new Purchase()
        {
            Pharmacy = pharmacyRepository,
            TotalPrice = 100.50,
            UserEmail = "email@email.com",
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Id = 1,
                    Quantity = 2,
                    Drug = drugRepository
                }
            }
        };
        using (var context = new PharmacyManagerContext(this._contextOptions))
        {
            context.Add(pharmacyRepository);
            context.Add(drugInfo);
            context.SaveChanges();
            drugRepository.DrugInfo = drugInfo;
            drugRepository.PharmacyId = pharmacyRepository.Id;
            context.Add(drugRepository);
            context.SaveChanges();
            context.Add(purchaseRepository);
            context.SaveChanges();
        }

        List<Purchase> returnedPurchases = this._purchaseRepository.GetAll().ToList();
        
        Assert.IsTrue(returnedPurchases.Exists(p => p.UserEmail == purchaseRepository.UserEmail));
    }

    [TestMethod]
    public void GetAllUsersWithExpresion()
    {
        string drugCode = "A01";
        string pharmacyName = "PharmacyName";
        DrugInfo drugInfo = new DrugInfo()
        {
            Name = "Drug",
            Symptoms = "Symtoms",
            Presentation = "Presentation",
            QuantityPerPresentation = 1,
            UnitOfMeasurement = "Comprimidos"
        };
        Drug drugRepository = new Drug()
        {
            DrugCode = drugCode
        };
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Name = pharmacyName
        };
        Purchase purchaseRepository = new Purchase()
        {
            Pharmacy = pharmacyRepository,
            TotalPrice = 100.50,
            UserEmail = "email@email.com",
            Items = new List<PurchaseItem>()
            {
                new PurchaseItem()
                {
                    Id = 1,
                    Quantity = 2,
                    Drug = drugRepository
                }
            }
        };
        using (var context = new PharmacyManagerContext(this._contextOptions))
        {
            context.Add(pharmacyRepository);
            context.Add(drugInfo);
            context.SaveChanges();
            drugRepository.DrugInfo = drugInfo;
            drugRepository.PharmacyId = pharmacyRepository.Id;
            context.Add(drugRepository);
            context.SaveChanges();
            context.Add(purchaseRepository);
            context.SaveChanges();
        }
        List<Purchase> returnedPurchases = this._purchaseRepository.GetAll(p => p.UserEmail == purchaseRepository.UserEmail).ToList();
        
        Assert.IsTrue(returnedPurchases.Count == 1);
    }
}