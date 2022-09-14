using BusinessLogic;
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
        public IActionResult Create(SessionRequestModel sessionPostModel)
        {
            Session session = ModelsMapper.ToEntity(sessionPostModel);
            Session sessionCreated = _sessionLogic.Create(session);
            SessionResponseModel sessionCreatedModel = ModelsMapper.ToModel(sessionCreated);
            
            return Ok(sessionCreatedModel);
        }
    }
}