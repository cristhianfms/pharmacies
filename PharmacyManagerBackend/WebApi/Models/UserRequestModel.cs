using System;

namespace WebApi.Models;

public class UserRequestModel
{
    public string UserName { get; set; }
    public string InvitationCode { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}

