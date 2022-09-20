using System;
using System.Collections.Generic;
using Domain;
using IBusinessLogic;

namespace BusinessLogic;

public class DrugLogic : IDrugLogic
{
    public virtual Drug Create(Drug drug)
    {
        return null;
    }

    public virtual void Delete(Drug drug)
    {

    }

    public virtual IEnumerable<Drug> GetAllDrugs()
    {
        return null;
    }

    public virtual Drug GetDrug(Drug drug)
    {
        return null;
    }
}

