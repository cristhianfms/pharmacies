using BusinessLogic;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserLogic _userLogic;

        public UserController(UserLogic userLogic)
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
