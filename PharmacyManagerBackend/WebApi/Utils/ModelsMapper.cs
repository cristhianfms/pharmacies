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
        public static Invitation ToEntity(InvitationModel invitationModel)
        {
            return new Invitation
            {
                Id = invitationModel.Id,
                UserName = invitationModel.UserName,
                Role = invitationModel.Role,
                Code = invitationModel.Code
            };
        }
        public static InvitationModel ToModel(Invitation invitation)
        {
            return new InvitationModel
            {
                Id = invitation.Id,
                UserName = invitation.UserName,
                Role = invitation.Role,
                Code = invitation.Code
            };
        }
    }
}