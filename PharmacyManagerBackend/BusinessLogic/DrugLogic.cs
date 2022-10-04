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
        private PharmacyLogic _pharmacyLogic;
        private Context _context;

        public DrugLogic(IDrugRepository drugRepository, IDrugInfoRepository drugInfoRepository, PharmacyLogic pharmacyLogic)
        {
            this._drugRepository = drugRepository;
            this._drugInfoRepository = drugInfoRepository;
            this._pharmacyLogic = pharmacyLogic;
        }

        public void SetContext(User currentUser)
        {
            _context = new Context()
            {
                CurrentUser = currentUser
            };
        }

        public Drug Create(Drug drug)
        {
            Pharmacy pharmacy = _pharmacyLogic.GetPharmacyByName(_context.CurrentUser.Pharmacy.Name);
            EmployeeBelongsInPharmacy(pharmacy);
            drug.PharmacyId = pharmacy.Id;
            DrugCodeNotRepeatedInPharmacy(drug.DrugCode, pharmacy);
            _drugInfoRepository.Create(drug.DrugInfo);
            Drug drugCreated = _drugRepository.Create(drug);
            pharmacy.Drugs.Add(drugCreated);
            _pharmacyLogic.UpdatePharmacy(pharmacy);
            return drugCreated;
        }

        private void EmployeeBelongsInPharmacy(Pharmacy pharmacy)
        {
            if (pharmacy.Employees.Find(e => e.Id == _context.CurrentUser.Id) == null)
                throw new ValidationException("The employee does not belong in the pharmacy");
        }

        public Drug Get(int drugId)
        {
            try
            {
                Drug drug = _drugRepository.GetFirst(d => d.Id == drugId);
                return drug;
            }
            catch (ResourceNotFoundException e)
            {
                throw new ResourceNotFoundException("Drug not found");
            }
        }

        public IEnumerable<Drug> GetAll()
        {
            return _drugRepository.GetAll();
        }

        private void DrugCodeNotRepeatedInPharmacy(string drugCode, Pharmacy pharmacy)
        {
            if (pharmacy.Drugs.Exists(d => d.DrugCode == drugCode))
                throw new ValidationException("The drug code already exists in this pharmacy");
        }

        public void Delete(int drugId)
        {
            Drug drug = Get(drugId);
            Pharmacy pharmacy = _pharmacyLogic.GetPharmacyByName(_context.CurrentUser.Pharmacy.Name);
            pharmacy.Drugs.Remove(drug);
            _pharmacyLogic.UpdatePharmacy(pharmacy);
            DrugInfo drugInfo = FindDrugInfo(drug.DrugInfoId);
            _drugInfoRepository.Delete(drugInfo);
        }

        private DrugInfo FindDrugInfo(int drugInfoId)
        {
            try
            {
                DrugInfo drugInfo = _drugInfoRepository.GetFirst(di => di.Id == drugInfoId);
                return drugInfo;
            }
            catch (ResourceNotFoundException e)
            {
                throw new ResourceNotFoundException("There is no info on the drug available");
            }
        }

        public virtual void AddStock(IEnumerable<SolicitudeItem> drugsToAddStock)
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
            Drug drugToUpdate = Get(drugId);
            drugToUpdate.Stock = drug.Stock;

            _drugRepository.Update(drugToUpdate);

            return drugToUpdate;
        }
    }
}

