using Domain;
using Domain.Dtos;
using WebApi.Models;

namespace WebApi.Utils;
public class InvitationModelsMapper
{
    public static InvitationDto ToEntity(InvitationModel invitationModel)
    {
        return new InvitationDto
        {
            UserName = invitationModel.UserName,
            RoleName = invitationModel.RoleName,
            PharmacyName = invitationModel.PharmacyName
        };
    }

    public static InvitationModel ToModel(Invitation invitation)
    {
        return new InvitationModel
        {
            Id = invitation.Id,
            UserName = invitation.UserName,
            RoleName = invitation.Role.Name,
            Code = invitation.Code,
            PharmacyName = invitation.Pharmacy.Name
        };
    }

    public static InvitationDto ToEntity(InvitationPutModel invitationPutModel)
    {
        return new InvitationDto
        {
            UserName = invitationPutModel.UserName,
            Code = invitationPutModel.InvitationCode,
            Email = invitationPutModel.Email,
            Address = invitationPutModel.Address,
            Password = invitationPutModel.Password
        };
    }

    public static InvitationConfirmedModel ToModel(InvitationDto invitationDto)
    {
        return new InvitationConfirmedModel
        {
            UserId = invitationDto.UserId,
            UserName = invitationDto.UserName,
            RoleName = invitationDto.RoleName,
            PharmacyName = invitationDto.PharmacyName,
            Email = invitationDto.Email,
            Address = invitationDto.Address
        };
    }
}
