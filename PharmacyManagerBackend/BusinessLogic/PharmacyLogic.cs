using System;
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
            return this._pharmacyRepository.Create(pharmacy);
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

    public virtual Pharmacy GetPharmacyByName(string pharmacyName)
    {
        return this._pharmacyRepository.GetFirst(f => f.Name.Equals(pharmacyName));
    }

    public virtual void ExistsDrug(string drugCode, int pharmacyId)
    {
        Pharmacy pharmacy = this._pharmacyRepository.GetFirst(p => p.Id == pharmacyId);
        
        if (pharmacy == null || !pharmacy.Drugs.Exists(d => d.DrugCode == drugCode))
        {
            throw new ResourceNotFoundException("resource does not exist");
        }

    }
}

