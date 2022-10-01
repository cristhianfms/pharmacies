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
        private IPharmacyLogic _pharmacyLogic;

        public DrugsController(IDrugLogic drugLogic, IPharmacyLogic pharmacyLogic)
        {
            this._drugLogic = drugLogic;
            _pharmacyLogic = pharmacyLogic;
        }

        [HttpPost]
        public IActionResult Create([FromBody] DrugModel drugModel)
        {
            if (_pharmacyLogic.ExistsDrug(drugModel.DrugCode, drugModel.PharmacyId))
            {
                return BadRequest("DrugCode already exists in the pharmacy");
            }
            Drug drug = ModelsMapper.ToEntity(drugModel);
            Drug drugCreated = _drugLogic.Create(drug);
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

