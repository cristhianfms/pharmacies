using BusinessLogic;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("pharmacies")]
    public class PharmacyController : ControllerBase 
    {
        private PharmacyLogic _pharmacyLogic;

        public PharmacyController(PharmacyLogic pharmacyLogic)
        {
            this._pharmacyLogic = pharmacyLogic;
        }

        [HttpPost]
        public IActionResult Create(PharmacyModel pharmacyModel)
        {
            Pharmacy pharmacy = ModelsMapper.ToEntity(pharmacyModel);
            Pharmacy pharmacyCreated = _pharmacyLogic.Create(pharmacy);
            PharmacyModel pharmacyCreatedModel = ModelsMapper.ToModel(pharmacyCreated);
            
            return Ok(pharmacyCreatedModel);
        }
    }
}