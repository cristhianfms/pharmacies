using System;
using System.Collections.Generic;
using Domain;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic
{
    public class DrugLogic : IDrugLogic
    {
        private IDrugRepository _drugRepository;

        public DrugLogic(IDrugRepository drugRepository)
        {
            this._drugRepository = drugRepository;
        }

        public virtual Drug Create(Drug drug)
        {
            return _drugRepository.Create(drug);
        }

        public virtual void Delete(int drugId)
        {
            _drugRepository.Delete(drugId);
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
}
