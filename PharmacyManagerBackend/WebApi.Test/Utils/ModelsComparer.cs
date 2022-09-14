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

    }
}