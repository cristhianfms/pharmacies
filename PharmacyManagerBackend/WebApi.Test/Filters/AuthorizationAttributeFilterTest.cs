using Domain;
using Domain.AuthDomain;
using Exceptions;
using IBusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using WebApi.Filters;

namespace WebApi.Test.Filters;

public class AuthorizationAttributeFilterTest
{
    private AuthorizationAttributeFilter _authFilter;
    private Mock<ISessionLogic> _sessionLogic;
    private Mock<IPermissionLogic> _permissionLogic;

    [TestInitialize]
    public void Initialize()
    {
        this._sessionLogic = new Mock<ISessionLogic>(MockBehavior.Strict);
        this._permissionLogic = new Mock<IPermissionLogic>(MockBehavior.Strict);
        _authFilter = new AuthorizationAttributeFilter(this._sessionLogic.Object, this._permissionLogic.Object);
    }

    [TestMethod]
    public void TokenOk()
    {
        Guid token = Guid.NewGuid();
        User user = new User
        {
            Role = new Role() { Name = "Admin" },
            Id = 1
        };
        Session sessionRepository = new Session()
        {
            Id = 1,
            Token = token,
            User = user
        };
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(a => a.Request.Headers["Authorization"]).Returns("token");
        ActionContext actionContext =
            new ActionContext(httpContextMock.Object, new RouteData(), new ActionDescriptor());
        AuthorizationFilterContext authFilterContext =
            new AuthorizationFilterContext(actionContext, new List<IFilterMetadata> { });
        _sessionLogic.Setup(m => m.Get("token")).Returns(sessionRepository);
        _permissionLogic.Setup(m => m.HasPermission(It.IsAny<User>(), "Admin")).Returns(true);

        _authFilter.OnAuthorization(authFilterContext);

        Assert.IsNull(authFilterContext.Result);
        _sessionLogic.VerifyAll();
    }

    [TestMethod]
    public void BadTokenTest()
    {
        Guid token = Guid.NewGuid();
        User user = new User
        {
            Role = new Role() { Name = "Admin" },
            Id = 1
        };
        Session sessionRepository = new Session()
        {
            Id = 1,
            Token = token,
            User = user
        };
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(a => a.Request.Headers["Authorization"]).Returns("token");
        ActionContext actionContext =
            new ActionContext(httpContextMock.Object, new RouteData(), new ActionDescriptor());
        AuthorizationFilterContext authFilterContext =
            new AuthorizationFilterContext(actionContext, new List<IFilterMetadata> { });
        _sessionLogic.Setup(m => m.Get("token")).Throws(new ResourceNotFoundException(""));

        _authFilter.OnAuthorization(authFilterContext);

        var result = authFilterContext.Result as ForbidResult;
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void TokenWithoutPermissionTest()
    {
        Guid token = Guid.NewGuid();
        User user = new User
        {
            Role = new Role() { Name = "Admin" },
            Id = 1
        };
        Session sessionRepository = new Session()
        {
            Id = 1,
            Token = token,
            User = user
        };
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(a => a.Request.Headers["Authorization"]).Returns("token");
        ActionContext actionContext =
            new ActionContext(httpContextMock.Object, new RouteData(), new ActionDescriptor());
        AuthorizationFilterContext authFilterContext =
            new AuthorizationFilterContext(actionContext, new List<IFilterMetadata> { });
        _sessionLogic.Setup(m => m.Get("token")).Returns(sessionRepository);
        _permissionLogic.Setup(m => m.HasPermission(It.IsAny<User>(), "Admin")).Returns(false);

        _authFilter.OnAuthorization(authFilterContext);

        var result = authFilterContext.Result as UnauthorizedResult;
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void NullTokenTest()
    {
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(a => a.Request.Headers["Authorization"]).Returns((String)null);
        ActionContext actionContext =
            new ActionContext(httpContextMock.Object, new RouteData(), new ActionDescriptor());
        AuthorizationFilterContext authFilterContext =
            new AuthorizationFilterContext(actionContext, new List<IFilterMetadata> { });

        _authFilter.OnAuthorization(authFilterContext);

        var result = authFilterContext.Result as ForbidResult;
        Assert.IsNotNull(result);
    }
}