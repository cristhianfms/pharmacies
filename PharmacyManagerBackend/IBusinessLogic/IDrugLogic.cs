using System;
using System.Collections.Generic;
using Domain;

namespace IBusinessLogic
{
    public interface IDrugLogic
    {
        Drug Create(Drug drug);
        Drug Get(int drugId);
        void Delete(int drugId);
        void AddStock(IEnumerable<SolicitudeItem> items);
        void SetContext(User currentUser);
        IEnumerable<Drug> GetAll();

    }
}

