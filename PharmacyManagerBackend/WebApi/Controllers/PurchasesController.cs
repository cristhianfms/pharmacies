using Domain;
using Domain.Dtos;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class PurchasesController : ControllerBase
    {
        private IPurchaseLogic _purchaseLogic;

        public PurchasesController(IPurchaseLogic purchaseLogic)
        {
            this._purchaseLogic = purchaseLogic;
        }

        [HttpPost]
        public IActionResult Create([FromBody] PurchaseRequestModel purchaseRequestModel)
        {
            PurchaseDto purchase =  ModelsMapper.ToEntity(purchaseRequestModel);
            PurchaseDto purhcaseCreated = _purchaseLogic.Create(purchase);
            PurchaseResponseModel purchaseResponseModel = ModelsMapper.ToModel(purhcaseCreated);
            
            return Ok(purchaseResponseModel);
        }
    }
}
