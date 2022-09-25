using System.Data.Common;
using DataAccess.Context;
using Domain;
using Domain.AuthDomain;
using Exceptions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Test;

[TestClass]
public class SessionRepositoryTest
{
    private DbConnection _connection;
    private SessionRepository _sesionRepository;

    private PharmacyManagerContext _pharmacyManagerContext;
    private DbContextOptions<PharmacyManagerContext> _contextOptions;

    public SessionRepositoryTest()
    {
        this._connection = new SqliteConnection("Filename=:memory:");
        this._contextOptions =
            new DbContextOptionsBuilder<PharmacyManagerContext>().UseSqlite(this._connection).Options;

        this._pharmacyManagerContext = new PharmacyManagerContext(this._contextOptions);
        this._sesionRepository = new SessionRepository(this._pharmacyManagerContext);
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
        Guid token = Guid.NewGuid();
        User userRepository = new User()
        {
            UserName = "username",
            Email = "email@email.com",
            Address = "dire 1",
            Password = "password+1",
            Role = new Role()
            {
                Name = Role.ADMIN
            }
        };
        Session sessionRepository = new Session()
        {
            Token = token,
            User = userRepository
        };
        using (var context = new PharmacyManagerContext(this._contextOptions))
        {
            context.Add(sessionRepository);
            context.SaveChanges();
        }

        Session sessionReturned = this._sesionRepository.GetFirst(u => true);

        Assert.AreEqual(sessionRepository.Token, sessionReturned.Token);
        Assert.AreEqual(sessionRepository.User.Role.Name, sessionReturned.User.Role.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ResourceNotFoundException))]
    public void GetFirstShoudlThrowExcptionWhenUserNotExists()
    {
        this._sesionRepository.GetFirst(u => true);
    }
}