using Exceptions;
using System.Collections.Generic;
using Domain;
using IBusinessLogic;
using IDataAccess;
using System;

namespace BusinessLogic
{
    public class DrugLogic : IDrugLogic
    {
        private IDrugRepository _drugRepository;
        private IDrugInfoRepository _drugInfoRepository;

        public DrugLogic(IDrugRepository drugRepository, IDrugInfoRepository drugInfoRepository)
        {
            this._drugRepository = drugRepository;
            this._drugInfoRepository = drugInfoRepository;
        }

        public virtual Drug Create(Drug drug)
        {
            ExistsDrug(drug);
            return _drugRepository.Create(drug);
        }

        public virtual DrugInfo Create(DrugInfo drugInfo)
        {
            return _drugInfoRepository.Create(drugInfo);
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
                throw new ValidationException("Drug already exists");
            }

        }

        public virtual void Delete(int drugId)
        {
            _drugRepository.Delete(drugId);
        }

        public void AddStock(List<SolicitudeItem> drugsToAddStock)
        {
            foreach (var drugSolicitude in drugsToAddStock)
            {
                Drug drugToUpdate = _drugRepository.GetFirst(d=> d.DrugCode == drugSolicitude.DrugCode);
                drugToUpdate.Stock = drugToUpdate.Stock + drugSolicitude.DrugQuantity;
                _drugRepository.Update(drugToUpdate);
            }
        }
    }
}
