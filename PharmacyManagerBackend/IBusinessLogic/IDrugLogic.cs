using System;
using System.Collections.Generic;
using Domain;
using Domain.Dtos;

namespace IBusinessLogic
{
    public interface IDrugLogic
    {
        Drug Create(Drug drug);
        Drug Get(int drugId);
        void Delete(int drugId);
        void AddStock(IEnumerable<SolicitudeItem> items);
        IEnumerable<Drug> GetAll(QueryDrugDto queryDrugDto);

    }
}

