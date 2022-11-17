using System;
using System.Collections.Generic;
using Domain;
using Exceptions;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic;

public class PharmacyLogic : IPharmacyLogic
{
    private IPharmacyRepository _pharmacyRepository;

    public PharmacyLogic(IPharmacyRepository pharmacyRepository)
    {
        this._pharmacyRepository = pharmacyRepository;
    }

    public Pharmacy Create(Pharmacy pharmacy)
    {
        if (ExistsPharmacy(pharmacy.Name))
        {
            throw new ValidationException("Pharmacy name already exists");
        }
        else
        {
            Pharmacy createdPharmacy = this._pharmacyRepository.Create(pharmacy);
            return createdPharmacy;
        }
    }

    private bool ExistsPharmacy(string name)
    {
        try
        {
            Pharmacy pharmacy = this._pharmacyRepository.GetFirst(p => p.Name == name);

            return true;
        }
        catch (ResourceNotFoundException)
        {
            return false;
        }
    }

    public IEnumerable<Pharmacy> GetAll()
    {
        IEnumerable<Pharmacy> pharmacies = _pharmacyRepository.GetAll();
        return pharmacies;
    }

    public virtual Pharmacy GetPharmacyByName(string pharmacyName)
    {
        Pharmacy fetchedPharmacy = this._pharmacyRepository.GetFirst(f => f.Name.Equals(pharmacyName));
        return fetchedPharmacy;
    }

    public virtual void ExistsDrug(string drugCode, int pharmacyId)
    {
        Pharmacy pharmacy = this._pharmacyRepository.GetFirst(p => p.Id == pharmacyId);

        if (pharmacy == null || !pharmacy.Drugs.Exists(d => d.DrugCode == drugCode))
        {
            throw new ResourceNotFoundException("resource does not exist");
        }

    }

    public virtual void DrugCodeRepeatedInPharmacy(string drugCode, int pharmacyId)
    {
        Pharmacy pharmacy = this._pharmacyRepository.GetFirst(p => p.Id == pharmacyId);

        if (pharmacy.Drugs.Exists(d => d.DrugCode == drugCode))
        {
            throw new ResourceNotFoundException("The drug code already exists in this pharmacy");
        }
    }

    public virtual void UpdatePharmacy(Pharmacy pharmacy)
    {
        this._pharmacyRepository.Update(pharmacy);
    }
}

