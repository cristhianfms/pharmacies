using System;
using System.Collections.Generic;
using Domain;

namespace IBusinessLogic
{
    public interface IDrugLogic
    {
        Drug Create(Drug drug);
        void DeleteDrug(Drug drug);
    }
}
