
using Domain;
using Domain.AuthDomain;
using Exceptions;
using IAuthLogic;
using IBusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using WebApi.Filter.Models;

namespace WebApi.Filter.Test;

[TestClass]
public class AuthorizationAttributeFilterTest
{
    private AuthorizationAttributeFilter _authFilter;
    private Mock<ISessionLogic> _sessionLogicMock;
    private Mock<IPermissionLogic> _permissionLogicMock;
    private Mock<ISolicitudeLogic> _solicitudeLogicMock;
    private Mock<IDrugLogic> _drugLogicMock;
    private Mock<IPurchaseLogic> _purchaseLogicMock;
    private Mock<Context> _context;

    [TestInitialize]
    public void Initialize()
    {
        this._sessionLogicMock = new Mock<ISessionLogic>(MockBehavior.Strict);
        this._permissionLogicMock = new Mock<IPermissionLogic>(MockBehavior.Strict);
        this._context = new Mock<Context>(MockBehavior.Strict);
        _authFilter = new AuthorizationAttributeFilter(this._sessionLogicMock.Object,
        this._permissionLogicMock.Object,
        this._context.Object);
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
        httpContextMock.Setup(a => a.Request.Headers["Authorization"]).Returns(token.ToString());
        ActionContext actionContext =
            new ActionContext(httpContextMock.Object, new RouteData(), new ActionDescriptor());
        AuthorizationFilterContext authFilterContext =
            new AuthorizationFilterContext(actionContext, new List<IFilterMetadata> { });
        _sessionLogicMock.Setup(m => m.Get(token)).Returns(sessionRepository);
        _permissionLogicMock.Setup(m => m.HasPermission(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        _authFilter.OnAuthorization(authFilterContext);

        Assert.IsNull(authFilterContext.Result);
        _sessionLogicMock.VerifyAll();
        _permissionLogicMock.VerifyAll();
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
        httpContextMock.Setup(a => a.Request.Headers["Authorization"]).Returns(token.ToString());
        ActionContext actionContext =
            new ActionContext(httpContextMock.Object, new RouteData(), new ActionDescriptor());
        AuthorizationFilterContext authFilterContext =
            new AuthorizationFilterContext(actionContext, new List<IFilterMetadata> { });
        _sessionLogicMock.Setup(m => m.Get(token)).Throws(new ResourceNotFoundException(""));

        _authFilter.OnAuthorization(authFilterContext);

        var result = authFilterContext.Result as ObjectResult;

        Assert.AreEqual(401, result.StatusCode);
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
        httpContextMock.Setup(a => a.Request.Headers["Authorization"]).Returns(token.ToString());
        ActionContext actionContext =
            new ActionContext(httpContextMock.Object, new RouteData(), new ActionDescriptor());
        AuthorizationFilterContext authFilterContext =
            new AuthorizationFilterContext(actionContext, new List<IFilterMetadata> { });
        _sessionLogicMock.Setup(m => m.Get(token)).Returns(sessionRepository);
        _permissionLogicMock.Setup(m => m.HasPermission(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

        _authFilter.OnAuthorization(authFilterContext);

        var result = authFilterContext.Result as ObjectResult;

        Assert.AreEqual(403, result.StatusCode);
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

        var result = authFilterContext.Result as ObjectResult;

        Assert.AreEqual(401, result.StatusCode);
    }

    [TestMethod]
    public void BadFormatoOfTokenTest()
    {
        string token = "asdf";
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(a => a.Request.Headers["Authorization"]).Returns(token);
        ActionContext actionContext =
            new ActionContext(httpContextMock.Object, new RouteData(), new ActionDescriptor());
        AuthorizationFilterContext authFilterContext =
            new AuthorizationFilterContext(actionContext, new List<IFilterMetadata> { });

        _authFilter.OnAuthorization(authFilterContext);

        var result = authFilterContext.Result as ObjectResult;

        Assert.AreEqual(401, result.StatusCode);
    }
}