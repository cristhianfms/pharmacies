using System;
using System.Collections.Generic;
using Domain;

namespace IDataAccess
{
    public interface IDrugRepository : IBaseRepository<Drug>
    {
        void Update(Drug drug);
        void Delete(Drug drug);
    }
}
