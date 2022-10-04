using Domain;

namespace IAuthLogic;

public interface IPermissionLogic
{
    bool HasPermission(string userRole, string endpoint);
}