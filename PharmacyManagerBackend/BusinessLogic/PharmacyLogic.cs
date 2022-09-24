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

    //TODO:
    public Pharmacy Create(Pharmacy pharmacy)
    {
        throw new NotImplementedException();
    }

    public virtual Pharmacy GetPharmacyByName(string pharmacyName)
    {
        return _pharmacyRepository.GetFirst(f => pharmacyName.Equals(pharmacyName));
    }
}

