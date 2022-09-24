using Domain;
namespace WebApi.Models;

public class InvitationConfirmedModel
{
    public string UserName { get; set; }
    public string RoleName { get; set; }
    public string PharmacyName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}

