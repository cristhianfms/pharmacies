using System.Security.Authentication;
using Domain;
using Domain.AuthDomain;
using Exceptions;
using IAuthLogic;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using WebApi.Filter.Models;

namespace WebApi.Filter;

public class AuthorizationAttributeFilter : Attribute, IAuthorizationFilter
{
    private const string UnauthorizedError = "permissionError";
    private const int UnauthorizedCode = 401;
    private const string ForbiddenError = "forbidenError";
    private const int ForbiddenCode = 403;

    private readonly ISessionLogic _sessionLogic;
    private readonly IPermissionLogic _permissionLogic;
    private readonly Context _context;

    public AuthorizationAttributeFilter(ISessionLogic sessionsLogic, 
        IPermissionLogic permissionLogic,
        Context currentContext)
    {
        this._sessionLogic = sessionsLogic;
        this._permissionLogic = permissionLogic;
        this._context = currentContext;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var tokenRow = context.HttpContext.Request.Headers["Authorization"];
        var endpoint = context.HttpContext.Request.Method + context.HttpContext.Request.Path;
        if (String.IsNullOrEmpty(tokenRow))
        {
            ExceptionModel exceptionModel = new ExceptionModel()
            {
                Error = UnauthorizedError,
                Message = "Missing Authorization token"
            };
            context.Result = new ObjectResult(exceptionModel)
            {
                StatusCode = UnauthorizedCode
            };
            return;
        }
        else
        {
            Session session;
            
            if (!Guid.TryParse(tokenRow, out var newGuid))
            {
                ExceptionModel exceptionModel = new ExceptionModel()
                {
                    Error = UnauthorizedError,
                    Message = "Invalid token"
                };
                context.Result = new ObjectResult(exceptionModel)
                {
                    StatusCode = UnauthorizedCode
                };
                return;
            }
            
            try
            {
                session = _sessionLogic.Get(newGuid);
            }
            catch (ResourceNotFoundException)
            {
                ExceptionModel exceptionModel = new ExceptionModel()
                {
                    Error = UnauthorizedError,
                    Message = "Invalid token"
                };
                context.Result = new ObjectResult(exceptionModel)
                {
                    StatusCode = UnauthorizedCode
                };
                return;
            }

            User loggedUser = session.User;
            bool userHasPermission = _permissionLogic.HasPermission(loggedUser.Role.Name, endpoint);

            if (!userHasPermission)
            {
                ExceptionModel exceptionModel = new ExceptionModel()
                {
                    Error = ForbiddenError,
                    Message = "session doesn't have permission"
                };
                context.Result = new ObjectResult(exceptionModel)
                {
                    StatusCode = ForbiddenCode
                };
            }

            _context.CurrentUser = loggedUser;
        }
    }
}