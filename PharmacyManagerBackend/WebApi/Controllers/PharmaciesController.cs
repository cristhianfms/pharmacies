using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Models;
using WebApi.Models.Utils;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PharmaciesController : ControllerBase
{
    private IPharmacyLogic _pharmacyLogic;

    public PharmaciesController(IPharmacyLogic pharmacyLogic)
    {
        this._pharmacyLogic = pharmacyLogic;
    }

    [HttpPost]
    [ServiceFilter(typeof(AuthorizationAttributeFilter))]
    public IActionResult Create([FromBody] PharmacyModel pharmacyModel)
    {
        Pharmacy pharmacy = PharmacyModelsMapper.ToEntity(pharmacyModel);
        Pharmacy pharmacyCreated = _pharmacyLogic.Create(pharmacy);
        PharmacyModel pharmacyCreatedModel = PharmacyModelsMapper.ToModel(pharmacyCreated);

        return Ok(pharmacyCreatedModel);
    }

    [HttpGet]
    [ServiceFilter(typeof(AuthorizationAttributeFilter))]
    public IActionResult Get()
    {
        IEnumerable<PharmacyModel> pharmacyCreatedModel = PharmacyModelsMapper.ToModelList(_pharmacyLogic.GetAll());

        return Ok(pharmacyCreatedModel);
    }
}
