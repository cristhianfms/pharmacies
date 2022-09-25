using Domain;
using Domain.AuthDomain;
using IBusinessLogic;
using IDataAccess;
using System.Linq;

namespace AuthLogic;

public class PermissionLogic : IPermissionLogic
{
    private readonly IPermissionRepository _permissionRepository;
    public PermissionLogic(IPermissionRepository permissionRepository)
    {
        this._permissionRepository = permissionRepository;
    }

    public bool HasPermission(string userRole, string endpoint)
    {
        Permission permission = _permissionRepository.GetFirst(p =>
            endpoint.Equals(p.Endpoint));

        bool hasPermission;
        PermissionRole permissionRole;
        try
        {
            permission.PermissionRoles.First(pr => pr.Role.Name == userRole);
            hasPermission = true;
        }
        catch (InvalidOperationException)
        {
            hasPermission = false;
        }

        return hasPermission;
    }
}