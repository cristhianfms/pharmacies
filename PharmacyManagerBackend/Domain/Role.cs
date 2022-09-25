using Domain.AuthDomain;
using Exceptions;

namespace Domain;
public class Role
{
    private string name;
    public int Id { get; set; }
    public virtual ICollection<PermissionRole> PermissionRoles { get; set; }
    public string Name
    {
        get { return name; }
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ValidationException("Role name can't be empty");
            }
            name = value;
        }
    }
}