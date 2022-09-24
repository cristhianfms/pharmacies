using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Utils;
using System.Collections.Generic;
using IBusinessLogic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrugController : ControllerBase
    {
        private IDrugLogic _drugLogic;

        public DrugController(IDrugLogic drugLogic)
        {
            this._drugLogic = drugLogic;
        }

        [HttpPost]
        public IActionResult Create([FromBody] DrugResponseModel drugModel)
        {
            Drug drug = ModelsMapper.ToEntity(drugModel);
            DrugInfo drugInfo = ModelsMapper.ToEntityAsociated(drugModel);
            Drug drugCreated = _drugLogic.Create(drug);
            _drugLogic.Create(drugInfo);
            DrugModel drugCreatedModel = ModelsMapper.ToModel(drugCreated);
            return Ok(drugCreatedModel);
        }

        [HttpDelete]
        public IActionResult Delete(int drugId)
        {
            _drugLogic.Delete(drugId);
            return Ok("Se elimino correctamente");
        }

    }

}
