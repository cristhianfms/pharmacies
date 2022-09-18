using Domain;
namespace WebApi.Models
{
    public class InvitationModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Code { get; set; }
        public string PharmacyName { get; set; }
    }
}
