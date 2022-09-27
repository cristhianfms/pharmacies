using AuthLogic;
using Domain;
using Domain.AuthDomain;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace BusinessLogic.Test;

[TestClass]
public class PermissionLogicTest
{
    private PermissionLogic _permissionLogic;
    private Mock<IPermissionRepository> _permissionRepository;
    
    [TestInitialize]
    public void Initialize()
    {
        this._permissionRepository = new Mock<IPermissionRepository>(MockBehavior.Strict);
        this._permissionLogic = new PermissionLogic(this._permissionRepository.Object);
    }
    
    [TestMethod]
    public void HasPermissionOk()
    {
        string endpoint = "put/api/pharmacies";
        string roleName = "Admin";
        Role role = new Role
        {
            Id = 1,
            Name = roleName
        };
        Permission permission = new Permission
        {
            Endpoint = endpoint
        };
        List<PermissionRole> permissionRoles = new List<PermissionRole>{
            new PermissionRole {
                Role = role,
                Permission = permission
            }
        };
        permission.PermissionRoles = permissionRoles;
        _permissionRepository.Setup(m => m.GetFirst(It.IsAny<Func<Permission, bool>>())).Returns(permission);

        bool actualResult = _permissionLogic.HasPermission(roleName, endpoint);
        
        Assert.IsTrue(actualResult);
        _permissionRepository.VerifyAll();
    }
}