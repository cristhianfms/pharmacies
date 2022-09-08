using WebApi.Models;

namespace WebApi.Test.Utils
{
    public static class ModelsComparer 
    {
        public static bool PharmacyCompare(PharmacyModel pharmacy1, PharmacyModel pharmacy2)
        {
            return pharmacy1.Name.Equals(pharmacy2.Name) && pharmacy1.Address.Equals(pharmacy2.Address);
        }

    }
}