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
        return this._pharmacyRepository.Create(pharmacy);
    }

    public virtual Pharmacy GetPharmacyByName(string pharmacyName)
    {
        return this._pharmacyRepository.GetFirst(f => pharmacyName.Equals(pharmacyName));
    }

    public virtual void ExistsDrug(string drugCode, int pharmacyId)
    {
        Pharmacy pharmacy = this._pharmacyRepository.GetFirst(p => p.Id == pharmacyId && p.Drugs.Exists(d=>d.DrugCode == drugCode));


    }

    public virtual Drug GetDrug(int pharmacyId, string drugDrugCode)
    {
        throw new NotImplementedException();
    }
}

