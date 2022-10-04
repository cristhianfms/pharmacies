using Domain;

namespace IBusinessLogic
{
    public interface IRoleLogic
    {
        Role GetRoleByName(string roleName);
    }
}