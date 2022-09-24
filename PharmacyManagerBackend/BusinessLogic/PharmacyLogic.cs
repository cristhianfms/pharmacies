using System;
using Domain;
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
        return _pharmacyRepository.Create(pharmacy);
    }

    public virtual Pharmacy GetPharmacyByName(string pharmacyName)
    {
        return _pharmacyRepository.GetFirst(f => pharmacyName.Equals(pharmacyName));
    }
}

