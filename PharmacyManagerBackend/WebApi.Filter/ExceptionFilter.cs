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
    private const string AuthenticationError = "authenticationError";
    private const int InternalErrorCode = 500;
    private const int UnauthorizedCode = 401;

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
                Error = AuthenticationError,
                Message = e.Message
            };
            context.Result = new ObjectResult(exceptionModel)
            {
                StatusCode = UnauthorizedCode
            };
        }
        catch (Exception e)
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