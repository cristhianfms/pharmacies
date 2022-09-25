using System;
using Microsoft.EntityFrameworkCore;
using IDataAccess;
using Domain;

namespace DataAccess
{
    public class DrugRepository : BaseRepository<Drug>, IDrugRepository
    {
        public DrugRepository(DbContext dbContext) : base(dbContext){

        }
        
    }
}
