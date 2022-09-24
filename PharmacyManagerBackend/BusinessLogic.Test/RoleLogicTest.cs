using System;
using Domain;
using IDataAccess;
using Moq;

namespace BusinessLogic.Test;

[TestClass]
public class RoleLogicTest
{
    private RoleLogic _roleLogic;
    private Mock<IRoleRepository> _roleRepository;

    [TestInitialize]
    public void Initialize()
    {
        this._roleRepository = new Mock<IRoleRepository>(MockBehavior.Strict);
        _roleLogic = new RoleLogic(this._roleRepository.Object);
    }

    [TestMethod]
    public void GetRoleByNameOk()
    {
        string roleName = "Employee";
        Role roleRepository = new Role()
        {
            Name = roleName
        };

        _roleRepository.Setup(m => m.GetFirst(It.IsAny<Func<Role, bool>>())).Returns(roleRepository);


        Role roleReturned = _roleLogic.GetRoleByName(roleName);

        Assert.AreEqual(roleRepository, roleReturned);
    }
}

