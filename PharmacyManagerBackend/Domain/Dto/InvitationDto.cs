using System;
using Exceptions;

namespace Domain.Dtos;

public class InvitationDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Code { get; set; }
    public string RoleName { get; set; }
    public string PharmacyName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Password { get; set; }
}
