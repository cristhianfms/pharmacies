using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Domain.Dtos;

namespace IAuthLogic;

public interface IInvitationLogic
{
    Invitation Create(InvitationDto invitation);
    InvitationDto Update(string invitationCode, InvitationDto invitationDto);
}

