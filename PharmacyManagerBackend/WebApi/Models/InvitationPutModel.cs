using Domain;
namespace WebApi.Models;

public class InvitationPutModel
{
    public string UserName { get; set; }
    public string InvitationCode { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Password { get; set; }
}

