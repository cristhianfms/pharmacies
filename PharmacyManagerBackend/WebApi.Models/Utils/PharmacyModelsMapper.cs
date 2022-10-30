using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models.Utils
{
    public static class PharmacyModelsMapper
    {
        public static Pharmacy ToEntity(PharmacyModel pharmacyModel)
        {
            return new Pharmacy
            {
                Name = pharmacyModel.Name,
                Address = pharmacyModel.Address
            };
        }

        public static PharmacyModel ToModel(Pharmacy pharmacy)
        {
            return new PharmacyModel
            {
                Name = pharmacy.Name,
                Address = pharmacy.Address
            };
        }

        public static IEnumerable<PharmacyModel> ToModelList(IEnumerable<Pharmacy> pharmacies)
        {
            List<PharmacyModel> pharmaciesModel = new List<PharmacyModel>();
            foreach (Pharmacy _pharmacy in pharmacies)
            {
                pharmaciesModel.Add(ToModel(_pharmacy));
            }
            return pharmaciesModel;
        }
    }
}
