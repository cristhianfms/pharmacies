using Domain;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic;

public class RoleLogic
{
    private readonly IRoleRepository _roleRepository;
    public RoleLogic() { }
    public RoleLogic(IRoleRepository roleRepository)
    {
        this._roleRepository = roleRepository;
    }

    public virtual Role GetRoleByName(string roleName)
    {
        return _roleRepository.GetFirst(r => r.Name == roleName);
    }
}

