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

        public virtual void Add(Drug drug)
        {
            //_drugRepository.Create(drug);
        }

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
}
