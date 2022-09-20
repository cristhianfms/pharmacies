using System;
using Exceptions;

namespace Domain.Dtos;

public class InvitationDto
{
    public string UserName { get; set; }
    public string RoleName { get; set; }
    public string PharmacyName { get; set; }
}
