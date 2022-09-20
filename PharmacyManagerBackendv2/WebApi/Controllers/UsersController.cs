using Domain;
using Domain.Dtos;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Controllers;

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
    public IActionResult Create([FromBody] UserRequestModel userRequestModel)
    {
        UserDto userToCreate = ModelsMapper.ToEntity(userRequestModel);
        User userCreated = _userLogic.Create(userToCreate);
        UserResponseModel userCreatedModel = ModelsMapper.ToModel(userCreated);

        return Ok(userCreatedModel);
    }
}

