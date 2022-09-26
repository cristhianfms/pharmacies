using System;
using System.Collections.Generic;
using Domain;

namespace IBusinessLogic
{
    public interface IDrugLogic
    {
        Drug Create(Drug drug);
        DrugInfo Create(DrugInfo drugInfo);
        void Delete(int drugId);
        void AddStock(List<SolicitudeItem> items);
            
    }
}

