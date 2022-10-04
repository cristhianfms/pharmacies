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
        private Context _context;

        public DrugLogic(IDrugRepository drugRepository, IDrugInfoRepository drugInfoRepository, IPharmacyRepository pharmacyRepository)
        {
            this._drugRepository = drugRepository;
            this._drugInfoRepository = drugInfoRepository;
            this._pharmacyRepository = pharmacyRepository;
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
            //int pharmacyId = _context.CurrentUser.Pharmacy.Id;
            Pharmacy pharmacy = FindPharmacy(drug.PharmacyId);
            DrugCodeNotRepeatedInPharmacy(drug.DrugCode, pharmacy);
            //EmployeeBelongsInPharmacy(pharmacy);
            //drug.PharmacyId = pharmacyId;
            _drugInfoRepository.Create(drug.DrugInfo);
            Drug drugCreated = _drugRepository.Create(drug);
            pharmacy.Drugs.Add(drugCreated);
            _pharmacyRepository.Update(pharmacy);
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


        private void DrugCodeNotRepeatedInPharmacy(string drugCode, Pharmacy pharmacy)
        {
            if (pharmacy.Drugs.Exists(d => d.DrugCode.Equals(drugCode)))
                throw new ValidationException("The drug code already exists in this pharmacy");
        }

        public void Delete(int drugId)
        {
            //int pharmacyId = _context.CurrentUser.Pharmacy.Id;
            Drug drug = Get(drugId);
            Pharmacy pharmacy = FindPharmacy(drug.PharmacyId);
            pharmacy.Drugs.Remove(drug);
            _pharmacyRepository.Update(pharmacy);
            DrugInfo drugInfo = FindDrugInfo(drug.DrugInfoId);
            _drugInfoRepository.Delete(drugInfo);
        }

        private Pharmacy FindPharmacy(int pharmacyId)
        {
            try
            {
                Pharmacy pharmacy = _pharmacyRepository.GetFirst(p => p.Id == pharmacyId);
                return pharmacy;
            }
            catch (ResourceNotFoundException e)
            {
                throw new ResourceNotFoundException("Pharmacy does not exist");
            }
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
            Drug drugToUpdate = Get(drugId);
            drugToUpdate.Stock = drug.Stock;

            _drugRepository.Update(drugToUpdate);

            return drugToUpdate;
        }
    }
}

