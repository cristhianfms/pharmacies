using System;
using System.Collections.Generic;
using IBusinessLogic;
using Domain;
using IDataAccess;
using Exceptions;

namespace BusinessLogic
{
    public class InvitationLogic : IInvitationLogic
    {
        private IBaseRepository<Invitation> _invitationRepository;
        private UserLogic _userLogic;

        public InvitationLogic(IBaseRepository<Invitation> invitationRepository, UserLogic userLogic)
        {
            this._invitationRepository = invitationRepository;
            this._userLogic = userLogic;
        }

        public virtual Invitation Create(Invitation invitation)
        {
            invitation.CheckIsValid();
            checkIfUserNameIsRepeated(invitation.UserName);

            Invitation createdInvitation = _invitationRepository.Create(invitation);

            return createdInvitation;
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
    }
}
