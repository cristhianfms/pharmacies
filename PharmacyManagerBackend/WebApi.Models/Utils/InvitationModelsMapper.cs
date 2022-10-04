using Domain;
using Domain.Dtos;


namespace WebApi.Models.Utils;
public class InvitationModelsMapper
{
    public static InvitationDto ToEntity(InvitationRequestModel invitationModel)
    {
        return new InvitationDto
        {
            UserName = invitationModel.UserName,
            RoleName = invitationModel.RoleName,
            PharmacyName = invitationModel.PharmacyName
        };
    }

    public static InvitationResponseModel ToModel(Invitation invitation)
    {
        return new InvitationResponseModel
        {
            UserName = invitation.UserName,
            RoleName = invitation.Role.Name,
            InvitationCode = invitation.Code,
            PharmacyName = invitation.Pharmacy.Name
        };
    }

    public static InvitationDto ToEntity(InvitationPutModel invitationPutModel)
    {
        return new InvitationDto
        {
            UserName = invitationPutModel.UserName,
            Email = invitationPutModel.Email,
            Address = invitationPutModel.Address,
            Password = invitationPutModel.Password
        };
    }

    public static InvitationConfirmedModel ToModel(InvitationDto invitationDto)
    {
        return new InvitationConfirmedModel
        {
            UserName = invitationDto.UserName,
            RoleName = invitationDto.RoleName,
            PharmacyName = invitationDto.PharmacyName,
            Email = invitationDto.Email,
            Address = invitationDto.Address
        };
    }
}