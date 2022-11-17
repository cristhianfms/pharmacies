using System;
using Exceptions;

namespace Domain.Dto;

public class InvitationDto
{
    public string UserName { get; set; }
    public string Code { get; set; }
    public string RoleName { get; set; }
    public string PharmacyName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Password { get; set; }
    public bool Used { get; set; }
    
    public override bool Equals(object? obj)
    {
        return obj is InvitationDto invitationDto&&
               UserName == invitationDto.UserName && 
               Code == invitationDto.Code && 
               RoleName == invitationDto.RoleName && 
               PharmacyName == invitationDto.PharmacyName && 
               Email == invitationDto.Email && 
               Address == invitationDto.Address && 
               Password == invitationDto.Password;
    }
}
