using System;
using DataAccess;
using IDataAccess;
using Domain;

namespace DataAccess
{
    public class DrugRepository : BaseRepository<Drug>, IDrugRepository
    {

        public void Delete(int drugId)
        {

        }

    }
}
