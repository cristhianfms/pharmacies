using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        private DbConnection _connection;
        private BaseRepository<Concert> _concertRepository;
        private PharmacyManagerContext _antelArenaContext;
        private DbContextOptions<PharmacyManagerContext> _contextOptions;
        
        [TestMethod]
        public void ConcertRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<PharmacyManagerContext>().UseSqlite(this._connection).Options;

            this._antelArenaContext = new PharmacyManagerContext(this._contextOptions);
            this._concertRepository = new BaseRepository<User>(this._antelArenaContext);
        }
    }
}
