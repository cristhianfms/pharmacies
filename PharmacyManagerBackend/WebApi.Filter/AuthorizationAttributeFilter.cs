using Domain;
using Domain.AuthDomain;
using Exceptions;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filter;

public class AuthorizationAttributeFilter : Attribute, IAuthorizationFilter
{
    private readonly ISessionLogic _sessionLogic;
    private readonly IPermissionLogic _permissionLogic;
    private readonly ISolicitudeLogic _solicitudeLogic;

    public AuthorizationAttributeFilter(ISessionLogic sessionsLogic, IPermissionLogic permissionLogic, ISolicitudeLogic solicitudeLogic)
    {
        this._sessionLogic = sessionsLogic;
        this._permissionLogic = permissionLogic;
        this._solicitudeLogic = solicitudeLogic;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var tokenRow = context.HttpContext.Request.Headers["Authorization"];
        var endpoint = context.HttpContext.Request.Method + context.HttpContext.Request.Path;
        if (String.IsNullOrEmpty(tokenRow))
        {
            context.Result = new ForbidResult("Missing Authorization token");
            return;
        }
        else
        {
            Session session;
            Guid token = Guid.Parse(tokenRow);
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
            bool userHasPermission = _permissionLogic.HasPermission(loggedUser.Role.Name, endpoint);

            if (!userHasPermission)
            {
                context.Result = new UnauthorizedResult();
            }
            _solicitudeLogic.SetContext(loggedUser);

        }

    }
}