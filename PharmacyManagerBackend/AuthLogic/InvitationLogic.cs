using System;
using System.Collections.Generic;
using BusinessLogic;
using IBusinessLogic;
using Domain;
using IDataAccess;
using Exceptions;
using Domain.Dtos;

namespace AuthLogic
{

    public class InvitationLogic : IInvitationLogic
    {
        private IInvitationRepository _invitationRepository;
        private UserLogic _userLogic;
        private RoleLogic _roleLogic;
        private PharmacyLogic _pharmacyLogic;
        public InvitationLogic(IInvitationRepository invitationRepository, UserLogic userLogic, RoleLogic roleLogic, PharmacyLogic pharmacyLogic)
        {
            this._invitationRepository = invitationRepository;
            this._userLogic = userLogic;
            this._pharmacyLogic = pharmacyLogic;
            this._roleLogic = roleLogic;
        }

        public virtual Invitation Create(InvitationDto invitationDto)
        {
            checkIfUserNameIsRepeated(invitationDto.UserName);
            Pharmacy pharmacy = getExistantPharmacy(invitationDto.PharmacyName);
            Role role = getExistantRole(invitationDto.RoleName);

            Invitation invitationToCreate = new Invitation()
            {
                UserName = invitationDto.UserName,
                Role = role,
                Pharmacy = pharmacy
            };

            string codeGenerated = generateNewInvitationCode();
            invitationToCreate.Code = codeGenerated;

            Invitation createdInvitation = _invitationRepository.Create(invitationToCreate);

            return createdInvitation;
        }

        public InvitationDto Update(string invitationCode, InvitationDto invitationDto)
        {
            Invitation invitation = getCreatedInvitation(invitationCode);
            checkInvitationUserName(invitation, invitationDto.UserName);

            User userToCreate = new User()
            {
                UserName = invitation.UserName,
                Role = invitation.Role,
                Email = invitationDto.Email,
                Address = invitationDto.Address,
                Password = invitationDto.Password,
                RegistrationDate = DateTime.Now,
                Pharmacy = invitation.Pharmacy
            };

            User createdUser = _userLogic.Create(userToCreate);
            _invitationRepository.Delete(invitation);

            InvitationDto invitationDtoToReturn = new InvitationDto()
            {
                UserName = userToCreate.UserName,
                RoleName = invitation.Role.Name,
                PharmacyName = invitation.Pharmacy?.Name,
                Email = createdUser.Email,
                Address = createdUser.Address,
            };

            return invitationDtoToReturn;
        }

        private Pharmacy getExistantPharmacy(string pharmacyName)
        {
            Pharmacy pharmacy;
            try
            {
                pharmacy = _pharmacyLogic.GetPharmacyByName(pharmacyName);
            }
            catch (ResourceNotFoundException e)
            {
                throw new ValidationException("pharmacy doesn't exist");
            }

            return pharmacy;
        }

        private Role getExistantRole(string roleName)
        {
            Role role;
            try
            {
                role = _roleLogic.GetRoleByName(roleName);
            }
            catch (ResourceNotFoundException e)
            {
                throw new ValidationException("role doesn't exist");
            }
            return role;
        }

        public virtual Invitation GetInvitationByCode(string invitationCode)
        {
            return _invitationRepository.GetFirst(i => i.Code == invitationCode);
        }

        private void checkIfUserNameIsRepeated(string userName)
        {
            bool userExist = true;
            try
            {
                User user = _userLogic.GetUserByUserName(userName);
            }
            catch (ResourceNotFoundException e)
            {
                userExist = false;
            }

            if (userExist)
            {
                throw new ValidationException("username already exists");
            }
        }

        private string generateNewInvitationCode()
        {
            Random generator = new Random();
            String code;
            do
            {
                code = generator.Next(0, 1000000).ToString("D6");
            } while (isExistantCode(code));

            return code;
        }

        private bool isExistantCode(string code)
        {
            bool invitationExists = true;
            try
            {
                _invitationRepository.GetFirst(i => i.Code == code);
            }
            catch (ResourceNotFoundException e)
            {
                invitationExists = false;
            }

            return invitationExists;
        }

        private Invitation getCreatedInvitation(string invitationCode)
        {
            Invitation invitation;
            try
            {
                invitation = _invitationRepository.GetFirst(i => i.Code == invitationCode);
            }
            catch (ResourceNotFoundException e)
            {
                throw new ValidationException("invalid invitation code");
            }

            return invitation;
        }

        private void checkInvitationUserName(Invitation invitation, string userName)
        {
            if (invitation.UserName != userName)
            {
                throw new ValidationException("invalid invitation code");
            }
        }
    }
}