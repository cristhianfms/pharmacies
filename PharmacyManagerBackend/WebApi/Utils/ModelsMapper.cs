using Domain;
using WebApi.Models;

namespace WebApi.Utils
{
    public static class ModelsMapper
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

        public static User ToEntity(UserModel userModel)
        {
            return new User
            {
                UserName = userModel.UserName,
                Email    = userModel.Email,
                Address = userModel.Address
            };
        }
        public static UserModel ToModel(User user)
        {
            return new UserModel
            {
                UserName = user.UserName,
                Email    = user.Email,
                Address = user.Address
            };
        }

    }
}