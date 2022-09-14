using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class UsersController : ControllerBase
    {
        private IUserLogic _userLogic;

        public UsersController(IUserLogic userLogic)
        {
            this._userLogic = userLogic;
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserModel userModel)
        {
            User user = ModelsMapper.ToEntity(userModel);
            User userCreated = _userLogic.Create(user);
            UserModel userCreatedModel = ModelsMapper.ToModel(userCreated);
            
            return Ok(userCreatedModel);
        }
    }
}
