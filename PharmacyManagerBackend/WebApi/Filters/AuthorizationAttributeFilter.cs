using Domain;
using Domain.AuthDomain;
using Exceptions;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

public class AuthorizationAttributeFilter
{
    private readonly ISessionLogic _sessionLogic;
    private readonly IPermissionLogic _permissionLogic;

    public AuthorizationAttributeFilter(ISessionLogic sessionsLogic, IPermissionLogic permissionLogic)
    {
        this._sessionLogic = sessionsLogic;
        this._permissionLogic = permissionLogic;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"];
        var endpoint = context.HttpContext.Request.Method + context.HttpContext.Request.Path;
        if (String.IsNullOrEmpty(token))
        {
            context.Result = new ForbidResult("Missing Authorization token");
            return;
        }
        else
        {
            Session session;

            try
            {
                session = _sessionLogic.Get(token);
            }
            catch (ResourceNotFoundException)
            {
                context.Result = new ForbidResult("Invalid token");
                return;
            }

            User loggedUser = session.User;
            bool userHasPermission = _permissionLogic.HasPermission(loggedUser, endpoint);

            if (!userHasPermission)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}