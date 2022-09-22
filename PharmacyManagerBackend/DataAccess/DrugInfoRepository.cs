using System;
using Domain;
using IDataAccess;

namespace DataAccess
{
    public class DrugInfoRepository : BaseRepository<DrugInfo>, IDrugInfoRepository
    {
    }
}
