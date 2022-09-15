using BusinessLogic;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrugController : ControllerBase
    {
        private DrugLogic _drugLogic;

        public DrugController(DrugLogic drugLogic)
        {
            this._drugLogic = drugLogic;
        }

        [HttpPost]
        public IActionResult Create(DrugModel drugModel)
        {
            Drug drug = ModelsMapper.ToEntity(drugModel);
            Drug drugCreated = _drugLogic.Create(drug);
            DrugModel drugCreatedModel = ModelsMapper.ToModel(drugCreated);

            return Ok(drugCreatedModel);
        }
    }
}