using System;
using Domain;

namespace IBusinessLogic;

public interface IPharmacyLogic
{
    Pharmacy Create(Pharmacy pharmacy);
    bool ExistsDrug(string drugCode, int pharmacyId);
}

