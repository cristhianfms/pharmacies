using Domain;

namespace IBusinessLogic;

public interface IPermissionLogic
{
    bool HasPermission(string userRole, string endpoint);
}