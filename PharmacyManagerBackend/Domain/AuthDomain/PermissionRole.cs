namespace Domain.AuthDomain;
public class PermissionRole
{
    public int RoleId { get; set; }
    public virtual Role Role { get; set; }
    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}
