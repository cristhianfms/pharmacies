namespace WebApi.Models;

public class InvitationResponseModel
{
    public string UserName { get; set; }
    public string RoleName { get; set; }
    public string InvitationCode { get; set; }
    public string? PharmacyName { get; set; }
    
    public override bool Equals(object obj)
    {
        return obj is InvitationResponseModel invitation &&
               invitation.UserName == UserName &&
               invitation.RoleName == RoleName &&
               invitation.InvitationCode == InvitationCode &&
               invitation.PharmacyName == PharmacyName;
    }
}

