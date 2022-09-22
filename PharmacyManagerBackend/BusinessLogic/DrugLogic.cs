using Exceptions;
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
            ExistsDrug(drug);
            return _drugRepository.Create(drug);
        }

        private void ExistsDrug(Drug drug)
        {
            bool drugExist = true;
            try
            {
                Drug drug1 = _drugRepository.GetFirst(d => d.Equals(drug));
            }
            catch (ResourceNotFoundException e)
            {
                drugExist = false;
            }

            if (drugExist)
            {
                throw new ValidationException("username already exists");
            }

        }

        public virtual void Delete(int drugId)
        {
            _drugRepository.Delete(drugId);
        }

        public virtual IEnumerable<Drug> GetAllDrugs()
        {
            return _drugRepository.GetAll();
        }

        public virtual Drug GetDrug(Drug drug)
        {
            return _drugRepository.GetFirst(i => i.Id == drug.Id);
        }
    }
}
