using Domain.Dtos;
using IAuthLogic;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Utils;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    private ISessionLogic _sessionLogic;

    public SessionsController(ISessionLogic sessionLogic)
    {
        this._sessionLogic = sessionLogic;
    }

    [HttpPost]
    public IActionResult CreateSession([FromBody] CredentialsModel credentialsModel)
    {
        CredentialsDto credentials = ModelsMapper.ToEntity(credentialsModel);
        TokenDto token = _sessionLogic.Create(credentials);
        TokenModel tokenModel = ModelsMapper.ToModel(token);

        return Ok(tokenModel);
    }
}
