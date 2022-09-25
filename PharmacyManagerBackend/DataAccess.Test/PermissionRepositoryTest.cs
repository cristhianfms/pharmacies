using System.Data.Common;
using DataAccess.Context;
using Domain;
using Domain.AuthDomain;
using Exceptions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Test;

[TestClass]
public class PermissionRepositoryTest
{
    private DbConnection _connection;
    private PermissionRepository _premissionRepository;

    private PharmacyManagerContext _pharmacyManagerContext;
    private DbContextOptions<PharmacyManagerContext> _contextOptions;

    public PermissionRepositoryTest()
    {
        this._connection = new SqliteConnection("Filename=:memory:");
        this._contextOptions =
            new DbContextOptionsBuilder<PharmacyManagerContext>().UseSqlite(this._connection).Options;

        this._pharmacyManagerContext = new PharmacyManagerContext(this._contextOptions);
        this._premissionRepository = new PermissionRepository(this._pharmacyManagerContext);
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
        string endpoint = "put/api/pharmacies";
        Role role = new Role
        {
            Name = Role.ADMIN
        };
        Permission premissionRepository = new Permission
        {
            Endpoint = endpoint
        };
        List<PermissionRole> permissionRoles = new List<PermissionRole>{
            new PermissionRole {
                Role = role,
                Permission = premissionRepository
            }
        };
        premissionRepository.PermissionRoles = permissionRoles;
        using (var context = new PharmacyManagerContext(this._contextOptions))
        {
            context.Add(premissionRepository);
            context.SaveChanges();
        }

        Permission permissionReturned = this._premissionRepository.GetFirst(u => true);

        Assert.AreEqual(premissionRepository.Endpoint, permissionReturned.Endpoint);
        Assert.AreEqual(premissionRepository.PermissionRoles.ToList()[0].Role.Name, 
            permissionReturned.PermissionRoles.ToList()[0].Role.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ResourceNotFoundException))]
    public void GetFirstShoudlThrowExcptionWhenUserNotExists()
    {
        this._premissionRepository.GetFirst(u => true);
    }
}