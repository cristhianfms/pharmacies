using System;
using System.Collections.Generic;
using IBusinessLogic;
using Domain;
using IDataAccess;
using Exceptions;
using Domain.Dtos;

namespace BusinessLogic
{
    public class InvitationLogic : IInvitationLogic
    {
        private IInvitationRepository _invitationRepository;
        private UserLogic _userLogic;
        private RoleLogic _roleLogic;

        public InvitationLogic(){}

        public InvitationLogic(IInvitationRepository invitationRepository, UserLogic userLogic, RoleLogic roleLogic)
        {
            this._invitationRepository = invitationRepository;
            this._userLogic = userLogic;
        }

        public virtual Invitation Create(InvitationDto invitationDto)
        {
            checkIfUserNameIsRepeated(invitationDto.UserName);
            
            Invitation invitationToCreate = new Invitation(){
                UserName = invitationDto.UserName,
                Role = new Role(){
                    Name = invitationDto.RoleName
                },
                Pharmacy = new Pharmacy(){
                    Name = invitationDto.PharmacyName
                }
            };

            string codeGenerated = generateNewInvitationCode();
            invitationToCreate.Code = codeGenerated;

            Invitation createdInvitation = _invitationRepository.Create(invitationToCreate);

            return createdInvitation;
        }

        public virtual Invitation GetInvitationByCode(string invitationCode)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(int id)
        {
            throw new NotImplementedException();
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
                _invitationRepository.GetInvitationByCode(code);
            }
            catch (ResourceNotFoundException e)
            {
                invitationExists = false;
            }

            return invitationExists;
        }
    }
}
