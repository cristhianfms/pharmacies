﻿using System;
using System.Collections.Generic;
using IBusinessLogic;
using Domain;
using IDataAccess;
using Exceptions;
using Domain.Dtos;
using IAuthLogic;

namespace AuthLogic
{

    public class InvitationLogic : IInvitationLogic
    {
        private IInvitationRepository _invitationRepository;
        private IUserLogic _userLogic;
        private IRoleLogic _roleLogic;
        private IPharmacyLogic _pharmacyLogic;
        public InvitationLogic(IInvitationRepository invitationRepository, IUserLogic userLogic, IRoleLogic roleLogic, IPharmacyLogic pharmacyLogic)
        {
            this._invitationRepository = invitationRepository;
            this._userLogic = userLogic;
            this._pharmacyLogic = pharmacyLogic;
            this._roleLogic = roleLogic;
        }

        public virtual Invitation Create(InvitationDto invitationDto)
        {
            Invitation createdInvitation;
            Invitation? existentInvitation = getInvitationForUser(invitationDto.UserName);
            if (existentInvitation == null)
            {
                checkIfUserNameIsRepeated(invitationDto.UserName);
                Role role = getExistantRole(invitationDto.RoleName);
                Invitation invitationToCreate = new Invitation()
                {
                    UserName = invitationDto.UserName,
                    Role = role
                };

                if (!role.Name.Equals(Role.ADMIN))
                {
                    Pharmacy pharmacy = getExistantPharmacy(invitationDto.PharmacyName);
                    invitationToCreate.Pharmacy = pharmacy;
                }

                string codeGenerated = generateNewInvitationCode();
                invitationToCreate.Code = codeGenerated;

                createdInvitation = _invitationRepository.Create(invitationToCreate);
            }
            else
            {
                createdInvitation = existentInvitation;
            }

            return createdInvitation;
        }

        public InvitationDto Update(string invitationCode, InvitationDto invitationDto)
        {
            Invitation invitation = getCreatedInvitation(invitationCode);
            checkInvitationUserName(invitation, invitationDto.UserName);
            checkIfUserEmailIsRepeated(invitationDto.Email);
            
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
                User user = _userLogic.GetFirst(u => u.UserName == userName);
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
        
        private Invitation getInvitationForUser(string userName)
        {
            Invitation invitation;
            try
            {
                invitation = _invitationRepository.GetFirst(i => i.UserName == userName);
            }
            catch (ResourceNotFoundException e)
            {
                invitation = null;
            }

            return invitation;
        }
        
        private void checkIfUserEmailIsRepeated(string email)
        {
            bool userExist = true;
            try
            {
                User user = _userLogic.GetFirst(u => u.Email == email);
            }
            catch (ResourceNotFoundException e)
            {
                userExist = false;
            }

            if (userExist)
            {
                throw new ValidationException("email already registered");
            }
        }
    }
}