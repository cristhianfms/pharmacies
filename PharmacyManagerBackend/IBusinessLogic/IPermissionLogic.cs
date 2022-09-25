using Domain;

namespace IBusinessLogic;

public interface IPermissionLogic
{
    bool HasPermission(User user, string endpoint);
}