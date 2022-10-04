using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using IBusinessLogic;
using WebApi.Filter;
using WebApi.Models;
using WebApi.Models.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrugsController : ControllerBase
    {
        private IDrugLogic _drugLogic;

        public DrugsController(IDrugLogic drugLogic)
        {
            this._drugLogic = drugLogic;
        }

        [HttpPost]
        [ServiceFilter(typeof(AuthorizationAttributeFilter))]
        public IActionResult Create([FromBody] DrugModel drugModel)
        {
            Drug drug = ModelsMapper.ToEntity(drugModel);
            Drug drugCreated = _drugLogic.Create(drug);
            DrugModel drugCreatedModel = ModelsMapper.ToModel(drugCreated);
            return Ok(drugCreatedModel);
        }

        [HttpDelete("{drugId}")]
        [ServiceFilter(typeof(AuthorizationAttributeFilter))]
        public IActionResult Delete(int drugId)
        {
            _drugLogic.Delete(drugId);
            return Ok("Se elimino correctamente");
        }

        [HttpGet("{drugId}")]
        [ServiceFilter(typeof(AuthorizationAttributeFilter))]
        public IActionResult Get(int drugId)
        {
            Drug drug = _drugLogic.Get(drugId);

            DrugModel drugModel = ModelsMapper.ToModel(drug);
            return Ok(drugModel);
        }
    }

}

