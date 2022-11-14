using WebApi.Models;

namespace WebApi.Test.Utils;

public static class ModelsComparer
{
    public static bool PharmacyCompare(PharmacyModel pharmacy1, PharmacyModel pharmacy2)
    {
        return pharmacy1.Name.Equals(pharmacy2.Name) && pharmacy1.Address.Equals(pharmacy2.Address);
    }

    public static bool DrugCompare(DrugRequestModel drug1, DrugRequestModel drug2)
    {
        return drug1.Id == drug2.Id && drug1.DrugCode.Equals(drug2.DrugCode);
    }
}
