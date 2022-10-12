using System.Data.Common;
using DataAccess.Context;
using Domain;
using Exceptions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Test;

[TestClass]
public class InvitationRepositoryTest
{
    private DbConnection _connection;
    private InvitationRepository _invitationRepository;

    private PharmacyManagerContext _pharmacyManagerContext;
    private DbContextOptions<PharmacyManagerContext> _contextOptions;

    public InvitationRepositoryTest()
    {
        this._connection = new SqliteConnection("Filename=:memory:");
        this._contextOptions =
            new DbContextOptionsBuilder<PharmacyManagerContext>().UseSqlite(this._connection).Options;

        this._pharmacyManagerContext = new PharmacyManagerContext(this._contextOptions);
        this._invitationRepository = new InvitationRepository(this._pharmacyManagerContext);
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
    public void GetFirstOk()
    {
        Invitation invitationRepository = new Invitation()
        {
            Id = 1,
            UserName = "cris01",
            Role = new Role()
            {
                Name = "Employee"
            },
            Code = "123456",
            Pharmacy = new Pharmacy()
            {
                Name = "PharmacyB"
            }
        };
        using (var context = new PharmacyManagerContext(this._contextOptions))
        {
            context.Add(invitationRepository);
            context.SaveChanges();
        }

        Invitation returnedUser = this._invitationRepository.GetFirst(u => true);

        Assert.AreEqual(invitationRepository.UserName, returnedUser.UserName);
        Assert.AreEqual(invitationRepository.Pharmacy.Name, returnedUser.Pharmacy.Name);
        Assert.AreEqual(invitationRepository.Role.Name, returnedUser.Role.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ResourceNotFoundException))]
    public void GetFirstShoudlThrowExcptionWhenUserNotExists()
    {
        this._invitationRepository.GetFirst(u => true);
    }
    
    [TestMethod]
    public void GetAllInvitationsOK()
    {
        Invitation invitationRepository = new Invitation()
        {
            Id = 1,
            UserName = "cris01",
            Role = new Role()
            {
                Name = "Employee"
            },
            Code = "123456",
            Pharmacy = new Pharmacy()
            {
                Name = "PharmacyB"
            }
        };
        using (var context = new PharmacyManagerContext(this._contextOptions))
        {
            context.Add(invitationRepository);
            context.SaveChanges();
        }

        List<Invitation> returnedInvitations = this._invitationRepository.GetAll().ToList();
        
        Assert.IsTrue(returnedInvitations.Exists(p => p.UserName == invitationRepository.UserName));
    }

    [TestMethod]
    public void GetAllUsersWithExpresion()
    {
        Invitation invitationRepository = new Invitation()
        {
            Id = 1,
            UserName = "cris01",
            Role = new Role()
            {
                Name = "Employee"
            },
            Code = "123456",
            Pharmacy = new Pharmacy()
            {
                Name = "PharmacyB"
            }
        };
        using (var context = new PharmacyManagerContext(this._contextOptions))
        {
            context.Add(invitationRepository);
            context.SaveChanges();
        }

        List<Invitation> returnedInvitations = this._invitationRepository.GetAll(i => i.UserName == invitationRepository.UserName).ToList();
        
        Assert.IsTrue(returnedInvitations.Count == 1);
    }
}