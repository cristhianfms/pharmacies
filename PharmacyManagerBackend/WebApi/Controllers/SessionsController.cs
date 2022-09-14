using BusinessLogic;
using BusinessLogic.Dtos;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SessionsController : ControllerBase 
    {
        private SessionLogic _sessionLogic;

        public SessionsController(SessionLogic sessionLogic)
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
}