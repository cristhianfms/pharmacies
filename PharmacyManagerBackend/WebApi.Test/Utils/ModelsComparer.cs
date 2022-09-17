using WebApi.Models;

namespace WebApi.Test.Utils
{
    public static class ModelsComparer 
    {
        public static bool PharmacyCompare(PharmacyModel pharmacy1, PharmacyModel pharmacy2)
        {
            return pharmacy1.Name.Equals(pharmacy2.Name) && pharmacy1.Address.Equals(pharmacy2.Address);
        }
        
        public static bool InvitationCompare(InvitationModel invitation1, InvitationModel invitation2)
        {
            return invitation1.Id == invitation2.Id && invitation1.UserName.Equals(invitation2.UserName);
        }
        
        public static bool UserCompare(UserModel user1, UserModel user2)
        {
            return user1.UserName.Equals(user2.UserName) 
                && user1.Email.Equals(user2.Email) 
                && user1.Address.Equals(user2.Address);
        }

        public static bool DrugCompare(DrugModel drug1, DrugModel drug2)
        {
            return drug1.Id == drug2.Id && drug1.DrugCode.Equals(drug2.DrugCode);
        }
    }
}