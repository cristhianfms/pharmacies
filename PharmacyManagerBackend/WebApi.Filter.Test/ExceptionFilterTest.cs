using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using WebApi.Filter.Models;

namespace WebApi.Filter.Test;

[TestClass]
public class ExceptionFilterTest
{
    private ExceptionFilter _exceptionFilter;

    [TestInitialize]
    public void Initialize()
    {
        _exceptionFilter = new ExceptionFilter();
    }

    [TestMethod]
    public void NotFoundExceptionTest()
    {
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };
        ExceptionContext exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata> { })
        {
            Exception = new ResourceNotFoundException("Resource not found")
        };

        _exceptionFilter.OnException(exceptionContext);
        var result = exceptionContext.Result as NotFoundObjectResult;
        var resultModel = result.Value as ExceptionModel;

        Assert.AreEqual(404, result.StatusCode);
        Assert.AreEqual("Resource not found", resultModel.Message);
    }
    
    [TestMethod]
    public void ValidationExceptionTest()
    {
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };
        ExceptionContext exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata> { })
        {
            Exception = new ValidationException("Not valid entity")
        };

        _exceptionFilter.OnException(exceptionContext);
        var result = exceptionContext.Result as BadRequestObjectResult;
        var resultModel = result.Value as ExceptionModel;

        Assert.AreEqual(400, result.StatusCode);
        Assert.AreEqual("Not valid entity", resultModel.Message);
    }
    
    [TestMethod]
    public void GenericExceptionTest()
    {
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };
        ExceptionContext exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata> { })
        {
            Exception = new Exception("")
        };

        _exceptionFilter.OnException(exceptionContext);
        var result = exceptionContext.Result as ObjectResult;
        var resultModel = result.Value as ExceptionModel;

        Assert.AreEqual(500, result.StatusCode);
        Assert.AreEqual("Internal server error", resultModel.Message);
    }
}