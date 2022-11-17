using Domain;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic;

public class RoleLogic : IRoleLogic
{
    private readonly IRoleRepository _roleRepository;
    public RoleLogic() { }
    public RoleLogic(IRoleRepository roleRepository)
    {
        this._roleRepository = roleRepository;
    }

    public virtual Role GetRoleByName(string roleName)
    {
        Role fetchedRole = _roleRepository.GetFirst(r => r.Name == roleName);
        return fetchedRole;
    }
}

