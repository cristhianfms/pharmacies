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
    public class DrugsController : ControllerBase
    {
        private IDrugLogic _drugLogic;

        public DrugsController(IDrugLogic drugLogic)
        {
            this._drugLogic = drugLogic;
        }

        [HttpPost]
        public IActionResult Create([FromBody] DrugModel drugModel)
        {
            Drug drug = ModelsMapper.ToEntity(drugModel);
            Drug drugCreated = _drugLogic.Create(drug, drugModel.PharmacyId);
            DrugModel drugCreatedModel = ModelsMapper.ToModel(drugCreated);
            return Ok(drugCreatedModel);
        }

        [HttpDelete]
        public IActionResult Delete(int drugId)
        {
            _drugLogic.Delete(drugId);
            return Ok("Se elimino correctamente");
        }

        [HttpGet]
        public IActionResult Get(int drugId)
        {
            Drug drug = _drugLogic.Get(drugId);

            DrugModel drugModel = ModelsMapper.ToModel(drug);
            return Ok(drugModel);
        }
    }

}

