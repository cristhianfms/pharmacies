namespace Domain.AuthDomain;
public class Permission
{
    public int Id { get; set; }
    public string Endpoint { get; set; }
    public virtual ICollection<PermissionRole> PermissionRoles { get; set; }
}