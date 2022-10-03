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
        private IPharmacyRepository _pharmacyRepository;

        public DrugLogic(IDrugRepository drugRepository, IDrugInfoRepository drugInfoRepository, IPharmacyRepository pharmacyRepository)
        {
            this._drugRepository = drugRepository;
            this._drugInfoRepository = drugInfoRepository;
            this._pharmacyRepository = pharmacyRepository;
        }

        public Drug Create(Drug drug, int pharmacyId)
        {
            Pharmacy pharmacy = this._pharmacyRepository.GetFirst(p => p.Id == pharmacyId);
            PharmacyExists(pharmacy);
            DrugCodeNotRepeatedInPharmacy(drug.DrugCode, pharmacy);
            _drugInfoRepository.Create(drug.DrugInfo);
            Drug drugCreated = _drugRepository.Create(drug);
            pharmacy.Drugs.Add(drugCreated);
            _pharmacyRepository.Update(pharmacy);
            return _drugRepository.Create(drugCreated);
        }

        private void PharmacyExists(Pharmacy pharmacy)
        {
            if (pharmacy == null)
                throw new ValidationException("The pharmacy does not exist.");
        }

        public Drug Get(int drugId)
        {
            try
            {
                Drug myDrug = _drugRepository.GetFirst(d => d.Id == drugId);
                return myDrug;
            }
            catch (Exception e)
            {
                throw new ValidationException("Drug not found");
            }
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
                throw new ValidationException("Drug not found");
            }

        }

        private void DrugCodeNotRepeatedInPharmacy(string drugCode, Pharmacy pharmacy)
        {
            if (pharmacy.Drugs.Exists(d => d.DrugCode.Equals(drugCode)))
                throw new ValidationException("The drug code already exists in this pharmacy");
        }

        public void Delete(int drugId)
        {
            Drug drug = FindDrug(drugId);

            if (drug == null)
                throw new ValidationException("Drug does not exist");

            _drugRepository.Delete(drug);

        }

        public virtual void AddStock(List<SolicitudeItem> drugsToAddStock)
        {
            foreach (var drugSolicitude in drugsToAddStock)
            {
                Drug drugToUpdate = _drugRepository.GetFirst(d => d.DrugCode == drugSolicitude.DrugCode);
                drugToUpdate.Stock = drugToUpdate.Stock + drugSolicitude.DrugQuantity;
                _drugRepository.Update(drugToUpdate);
            }
        }

        public virtual Drug Update(int drugId, Drug drug)
        {
            Drug drugToUpdate = _drugRepository.GetFirst(d => d.Id == drugId);
            drugToUpdate.Stock = drug.Stock;

            _drugRepository.Update(drugToUpdate);

            return drugToUpdate;
        }
    }
}

