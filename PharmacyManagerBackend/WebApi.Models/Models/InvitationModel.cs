namespace WebApi.Models;

public class InvitationModel
{
    public string UserName { get; set; }
    public string RoleName { get; set; }
    public string? InvitationCode { get; set; }
    public string PharmacyName { get; set; }
}

