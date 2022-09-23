using Microsoft.EntityFrameworkCore;
using Domain;
using IDataAccess;

namespace DataAccess
{
    public class DrugInfoRepository : BaseRepository<DrugInfo>, IDrugInfoRepository
    {
        public DrugInfoRepository(DbContext dbContext) : base(dbContext){

        }
    }
}
