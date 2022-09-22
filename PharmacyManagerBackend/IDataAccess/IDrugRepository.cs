using System;
using System.Collections.Generic;
using Domain;

namespace IDataAccess
{
    public interface IDrugRepository : IBaseRepository<Drug>
    {
        void Delete(int drugId);
    }
}
