using System;
using Domain;

namespace IBusinessLogic;

public interface IPharmacyLogic
{
    Pharmacy Create(Pharmacy pharmacy);
    void ExistsDrug(string drugCode, int pharmacyId);
    Pharmacy GetPharmacyByName(string pharmacyName);
    IEnumerable<Pharmacy> GetAll();
}

