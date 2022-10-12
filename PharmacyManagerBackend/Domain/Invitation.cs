using Exceptions;

namespace Domain;
public class Invitation
{
    private string userName;
    public int Id { get; set; }
    public string UserName
    {
        get { return userName; }
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ValidationException("Name can't be empty");
            }
            userName = value;
        }
    }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public string Code { get; set; }
    public int? PharmacyId { get; set; }
    public Pharmacy? Pharmacy { get; set; }
    public Boolean Used{ get; set; }
}