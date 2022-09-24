using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Domain.Dtos;

namespace IBusinessLogic;

public interface IInvitationLogic
{
    Invitation Create(InvitationDto invitation);
    InvitationDto Update(string invitationCode, InvitationDto invitationDto);
}

