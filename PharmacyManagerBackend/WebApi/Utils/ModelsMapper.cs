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

        public static Session ToEntity(SessionRequestModel sessionPostModel)
        {
            User user = new User
            {
                UserName = sessionPostModel.UserName,
                Password = sessionPostModel.Password
            };

            return new Session
            {
                User = user
            };
        }

        public static SessionResponseModel ToModel(Session session)
        {
            return new SessionResponseModel
            {
                Token = session.Token
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