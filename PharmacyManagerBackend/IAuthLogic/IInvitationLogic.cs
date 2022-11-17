using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Domain.Dto;

namespace IAuthLogic;

public interface IInvitationLogic
{
    Invitation Create(InvitationDto invitation);
    InvitationDto Update(string invitationCode, InvitationDto invitationDto);
    IEnumerable<Invitation> GetInvitations(QueryInvitationDto queryInvitationDto);
}

