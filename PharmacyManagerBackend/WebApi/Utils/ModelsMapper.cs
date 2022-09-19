using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Dtos;
using System.Collections.Generic;
using System.Linq;
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
                Role = new Role()
                {
                    Name = invitationModel.RoleName
                },
                Code = invitationModel.Code,
                Pharmacy = new Pharmacy()
                {
                    Name = invitationModel.PharmacyName
                }
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
        public static Solicitude ToEntity(SolicitudeRequestModel solicitudeRequestModel)
        {
            List<SolicitudeItem> solicitudeItems = solicitudeRequestModel.SolicitudeItems.Select(i => ToEntity(i)).ToList();
            return new Solicitude()
            {

                Items = solicitudeItems,
            };
        }

        private static SolicitudeItem ToEntity (SolicitudeItemModel solicitudeItemModel)
        {
            return new SolicitudeItem()
            {
                DrugQuantity = solicitudeItemModel.DrugQuantity,
                DrugCode = solicitudeItemModel.DrugCode,
            };
        }

        public static SolicitudeResponseModel ToModel(Solicitude solicitude)
        {
            List<SolicitudeItemModel> solicitudeItems = solicitude.Items.Select(i => ToModel(i)).ToList();
            return new SolicitudeResponseModel()
            {
                Id = solicitude.Id,
                State = solicitude.State,
                Date = solicitude.Date,
                EmployeeUserName = solicitude.Employee.UserName,
                SolicitudeItems = solicitudeItems
            };
        }

        private static SolicitudeItemModel ToModel(SolicitudeItem solicitudeItem)
        {
            return new SolicitudeItemModel()
            {
                DrugQuantity = solicitudeItem.DrugQuantity,
                DrugCode = solicitudeItem.DrugCode
            };
        }
        public static List <SolicitudeResponseModel> ToModelList(List<Solicitude> solicitudes)
        {
            List <SolicitudeResponseModel> solicitudeResponseModels = new List <SolicitudeResponseModel>();
            foreach (Solicitude _solicitude in solicitudes)
            {
               solicitudeResponseModels.Add(ToModel(_solicitude));
            }
            return solicitudeResponseModels;
        }
    }
}
