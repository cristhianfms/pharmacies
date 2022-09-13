using WebApi.Models;

namespace WebApi.Test.Utils
{
    public static class ModelsComparer 
    {
        public static bool PharmacyCompare(PharmacyModel pharmacy1, PharmacyModel pharmacy2)
        {
            return pharmacy1.Name.Equals(pharmacy2.Name) && pharmacy1.Address.Equals(pharmacy2.Address);
        }
        public static bool UserCompare(UserModel user1, UserModel user2)
        {
            return user1.UserName.Equals(user2.UserName) 
                && user1.Email.Equals(user2.Email) 
                && user1.Address.Equals(user2.Address);
        }

    }
}