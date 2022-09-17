using System;
using System.Collections.Generic;
using IBusinessLogic;
using Domain;
using IDataAccess;

namespace BusinessLogic
{
    public class InvitationLogic : IInvitationLogic
    {
        IBaseRepository<Invitation> _invitationRepository;

        public InvitationLogic(IBaseRepository<Invitation> invitationRepository)
        {
            this._invitationRepository = invitationRepository;
        }

        public virtual Invitation Create(Invitation invitation)
        {
            Invitation createdInvitation = _invitationRepository.Create(invitation);

            return createdInvitation;
        }
    }
}
