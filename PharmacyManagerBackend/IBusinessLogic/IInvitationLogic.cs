using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace IBusinessLogic
{
    public interface IInvitationLogic
    {
        Invitation Create(Invitation invitation);
    }
}
