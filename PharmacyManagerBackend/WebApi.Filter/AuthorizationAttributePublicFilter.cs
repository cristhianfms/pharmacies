using Domain;
using IAuthLogic;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filter;

public class AuthorizationAttributePublicFilter : AuthorizationAttributeFilter
{
    public AuthorizationAttributePublicFilter(
        ISessionLogic sessionsLogic, 
        IPermissionLogic permissionLogic, 
        Context currentContext) : base(sessionsLogic, permissionLogic, currentContext)
    {
        
    }
    public override void OnAuthorization(AuthorizationFilterContext context)
    {
        var tokenRow = context.HttpContext.Request.Headers["Authorization"];
        if (!String.IsNullOrEmpty(tokenRow))
        {
            base.OnAuthorization(context);
        }
        else
        {
            var publico = true;
        }
    }
}