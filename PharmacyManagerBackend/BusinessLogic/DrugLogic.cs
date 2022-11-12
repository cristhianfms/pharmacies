using Exceptions;
using System.Collections.Generic;
using Domain;
using IBusinessLogic;
using IDataAccess;
using System;
using Domain.Dtos;

namespace BusinessLogic
{
    public class DrugLogic : IDrugLogic
    {
        private IDrugRepository _drugRepository;
        private IDrugInfoRepository _drugInfoRepository;
        private PharmacyLogic _pharmacyLogic;
        private Context _context;

        public DrugLogic(IDrugRepository drugRepository,
            IDrugInfoRepository drugInfoRepository,
            PharmacyLogic pharmacyLogic,
            Context currentContext)
        {
            this._drugRepository = drugRepository;
            this._drugInfoRepository = drugInfoRepository;
            this._pharmacyLogic = pharmacyLogic;
            this._context = currentContext;
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

        public IEnumerable<Drug> GetAll(QueryDrugDto queryDrugDto)
        {
            IEnumerable<Drug> drugs = new List<Drug>();

            if (_context.CurrentUser == null)
            {
                if (queryDrugDto.DrugName == null &&
                    queryDrugDto.WithStock == false)
                    drugs = _drugRepository.GetAll();

                if (queryDrugDto.DrugName == null &&
                    queryDrugDto.WithStock == true)
                    drugs = _drugRepository.GetAll(d => d.Stock > 0);

                if (queryDrugDto.DrugName != null &&
                    queryDrugDto.WithStock == false)
                    drugs = _drugRepository.GetAll(d => d.DrugInfo.Name == queryDrugDto.DrugName);

                if (queryDrugDto.DrugName != null &&
                    queryDrugDto.WithStock == true)
                    drugs = _drugRepository.GetAll(d => d.DrugInfo.Name == queryDrugDto.DrugName && d.Stock > 0);
            }
            else
            {
                if (queryDrugDto.DrugName == null &&
                    queryDrugDto.WithStock == false)
                    drugs = _drugRepository.GetAll(d => d.Pharmacy.Name == _context.CurrentUser.Pharmacy.Name);

                if (queryDrugDto.DrugName == null &&
                    queryDrugDto.WithStock == true)
                    drugs = _drugRepository.GetAll(d => d.Stock > 0 && d.Pharmacy.Name == _context.CurrentUser.Pharmacy.Name);

                if (queryDrugDto.DrugName != null &&
                    queryDrugDto.WithStock == false)
                    drugs = _drugRepository.GetAll(d => d.DrugInfo.Name == queryDrugDto.DrugName && d.Pharmacy.Name == _context.CurrentUser.Pharmacy.Name);

                if (queryDrugDto.DrugName != null &&
                    queryDrugDto.WithStock == true)
                    drugs = _drugRepository.GetAll(d => d.DrugInfo.Name == queryDrugDto.DrugName && d.Stock > 0 && d.Pharmacy.Name == _context.CurrentUser.Pharmacy.Name);
            }


            return drugs;
        }

        private void DrugCodeNotRepeatedInPharmacy(string drugCode, Pharmacy pharmacy)
        {
            if (pharmacy.Drugs.Exists(d => d.DrugCode == drugCode && d.IsActive))
                throw new ValidationException("The drug code already exists in this pharmacy");
        }

        public void Delete(int drugId)
        {
            Drug drug = Get(drugId);
            Pharmacy pharmacy = _pharmacyLogic.GetPharmacyByName(_context.CurrentUser.Pharmacy.Name);
            pharmacy.Drugs = ChangeDrugToDeactivated(pharmacy, drug);
            _pharmacyLogic.UpdatePharmacy(pharmacy);
        }

        private List<Drug> ChangeDrugToDeactivated(Pharmacy pharmacy, Drug drug)
        {
            List<Drug> drugs = pharmacy.Drugs;

            foreach (var d in drugs)
            {
                if (d.Equals(drug))
                    d.IsActive = false;
            }

            return drugs;
        }
        public virtual void AddStock(IEnumerable<SolicitudeItem> drugsToAddStock)
        {
            foreach (var drugSolicitude in drugsToAddStock)
            {
                Drug drugToUpdate = _drugRepository.GetFirst(d => d.DrugCode == drugSolicitude.DrugCode && d.IsActive);
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

        public virtual void DrugIsActive(string drugCode)
        {
            try
            {
                Drug drug = _drugRepository.GetFirst(d => d.DrugCode == drugCode);
                if (!drug.IsActive)
                    throw new ResourceNotFoundException("resource does not exist");
            }
            catch (InvalidOperationException)
            {
                throw new ResourceNotFoundException("resource does not exist");
            }
        }
    }
}

