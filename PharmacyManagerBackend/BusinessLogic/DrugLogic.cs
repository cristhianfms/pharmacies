using System;
using System.Collections.Generic;
using Domain;
using IBusinessLogic;

namespace BusinessLogic
{
    public class DrugLogic : IDrugLogic
    {
        public virtual Drug Create(Drug drug)
        {
            return null;
        }

        public virtual void DeleteDrug(Drug drug)
        {
            
        }

        public virtual IEnumerable<Drug> GetAllDrugs()
        {
            throw new NotImplementedException();
        }
    }
}
