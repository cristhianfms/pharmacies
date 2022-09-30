﻿using Exceptions;
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

        public Drug Create(Drug drug)
        {
            _drugInfoRepository.Create(drug.DrugInfo);
            return _drugRepository.Create(drug);
        }

        public Drug Get(int drugId)
        {
            Drug drug = FindDrug(drugId);
            if(drug != null)
                return drug;
            return new Drug();

        }

        private Drug FindDrug(int drugId)
        {
            try
            {
                Drug drug1 = _drugRepository.GetFirst(d => d.Id == drugId);
                return drug1;
            }
            catch (InvalidOperationException e)
            {
                return null;
            }

        }

        public void Delete(int drugId)
        {

            Drug drug = FindDrug(drugId);

            if (drug == null)
                throw new NullReferenceException("No existe la medicina");

            _drugRepository.Delete(drug);

        }
        public void AddStock(List<SolicitudeItem> drugsToAddStock)
        {
            foreach (var drugSolicitude in drugsToAddStock)
            {
                Drug drugToUpdate = _drugRepository.GetFirst(d => d.DrugCode == drugSolicitude.DrugCode);
                drugToUpdate.Stock = drugToUpdate.Stock + drugSolicitude.DrugQuantity;
                _drugRepository.Update(drugToUpdate);
            }
        }
    }
}

