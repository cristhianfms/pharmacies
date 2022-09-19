using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Dtos;
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

        public static CredentialsDto ToEntity(CredentialsModel credentialsModel)
        {
            return new CredentialsDto
            {
                UserName = credentialsModel.UserName,
                Password = credentialsModel.Password
            };
        }

        public static TokenModel ToModel(TokenDto tokenDto)
        {
            return new TokenModel
            {
                Token = tokenDto.Token
            };
        }

        public static UserDto ToEntity(UserRequestModel userRequestModel)
        {
            return new UserDto
            {
                UserName = userRequestModel.UserName,
                InvitationCode = userRequestModel.InvitationCode,
                Email = userRequestModel.Email,
                Address = userRequestModel.Address,
                Password = userRequestModel.Password
            };
        }
        public static UserResponseModel ToModel(User user)
        {
            return new UserResponseModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role.Name,
                Email = user.Email,
                Address = user.Address,
                PharmacyName = user.Pharmacy.Name
            };
        }
    }
}