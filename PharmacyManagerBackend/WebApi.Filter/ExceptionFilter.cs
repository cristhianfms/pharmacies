using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Filter.Models;

namespace WebApi.Filter;

public class ExceptionFilter : IExceptionFilter
{
    private const string InternalServerError = "internalServerError";
    private const string InternalServerErrorMessage = "Internal server error";
    private const string ValidationError = "validationError";
    private const string ResourceNotFoundError = "notFoundError";
    private const string ForbiddenError = "forbidenError";
    private const string AuthorizationError = "authorizationERror";
    private const int UnauthorizedCode = 403;
    private const int ForbiddenCode = 401;
    private const int InternalErrorCode = 500;

    public void OnException(ExceptionContext context)
    {
        try
        {
            throw context.Exception;
        }
        catch (ResourceNotFoundException e)
        {
            ExceptionModel exceptionModel = new ExceptionModel()
            {
                Error = ResourceNotFoundError,
                Message = e.Message
            };
            context.Result = new NotFoundObjectResult(exceptionModel);
        }
        catch (ValidationException e)
        {
            ExceptionModel exceptionModel = new ExceptionModel()
            {
                Error = ValidationError,
                Message = e.Message
            };
            context.Result = new BadRequestObjectResult(exceptionModel);
        }
        catch (AuthenticationException e)
        {
            ExceptionModel exceptionModel = new ExceptionModel()
            {
                Error = ForbiddenError,
                Message = e.Message
            };
            context.Result = new ObjectResult(exceptionModel)
            {
                StatusCode = ForbiddenCode
            };
        }
        catch (AuthorizationException e)
        {
            ExceptionModel exceptionModel = new ExceptionModel()
            {
                Error = AuthorizationError,
                Message = e.Message
            };
            context.Result = new ObjectResult(exceptionModel)
            {
                StatusCode = UnauthorizedCode
            };
        }
        catch (Exception)
        {
            ExceptionModel exceptionModel = new ExceptionModel()
            {
                Error = InternalServerError,
                Message = InternalServerErrorMessage
            };
            context.Result = new ObjectResult(exceptionModel)
            {
                StatusCode = InternalErrorCode
            };
        }
    }
}