using Domain.AuthDomain;
using IAuthLogic;
using IDataAccess;
using System.Text.RegularExpressions;

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
            IsEndpointMatch(p.Endpoint, endpoint));

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

    private bool IsEndpointMatch(string endpointRegex, string endpoint)
    {
        Regex regex = new Regex(endpointRegex);

        return regex.IsMatch(endpoint);
    }
}