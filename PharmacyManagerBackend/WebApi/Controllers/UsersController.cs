using BusinessLogic;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    
    public class UsersController : ControllerBase
    {
        private UserLogic _userLogic;

        public UsersController(UserLogic userLogic)
        {
            this._userLogic = userLogic;
        }

        [HttpPost]
        public IActionResult Create(UserModel userModel)
        {
            User user = ModelsMapper.ToEntity(userModel);
            User userCreated = _userLogic.Create(user);
            UserModel userCreatedModel = ModelsMapper.ToModel(userCreated);
            
            return Ok(userCreatedModel);
        }
    }
}
