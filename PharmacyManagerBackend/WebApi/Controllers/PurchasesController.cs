using Domain;
using Domain.Dtos;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Models;
using WebApi.Models.Utils;

namespace WebApi.Controllers;

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
        Purchase purchase = PurchaseModelsMapper.ToEntity(purchaseRequestModel);
        Purchase purhcaseCreated = _purchaseLogic.Create(purchase);
        PurchaseResponseModel purchaseResponseModel = PurchaseModelsMapper.ToModel(purhcaseCreated);

        return Ok(purchaseResponseModel);
    }
    
    [HttpPut("{purchaseId}")]
    [ServiceFilter(typeof(AuthorizationAttributePublicFilter))]
    public IActionResult Update(int purchaseId, [FromBody] PurchasePutModel purchasePutModel)
    {
        Purchase purchaseToUpdate = PurchaseModelsMapper.ToEntity(purchasePutModel);
        Purchase purchaseUpdated = _purchaseLogic.Update(purchaseId, purchaseToUpdate);
        PurchaseResponseModel purchaseUpdatedModel = PurchaseModelsMapper.ToModel(purchaseUpdated);

        return Ok(purchaseUpdatedModel);
    }

    [HttpGet("report")]
    [ServiceFilter(typeof(AuthorizationAttributeFilter))]
    public IActionResult GetPurchasesReport([FromQuery] QueryPurchaseDto queryPurchaseDto)
    {
        PurchaseReportDto purchaseReport = _purchaseLogic.GetPurchasesReport(queryPurchaseDto);
        PurchaseReportModel purchaseReportModel = PurchaseModelsMapper.ToModel(purchaseReport);

        return Ok(purchaseReportModel);
    }

    [HttpGet("{code}")]
    public IActionResult GetPurchase(string code)
    {
        Purchase purchase = _purchaseLogic.Get(code);
        PurchaseResponseModel purchaseResponseModel = PurchaseModelsMapper.ToModel(purchase);

        return Ok(purchaseResponseModel);
    }
    
    [HttpGet]
    [ServiceFilter(typeof(AuthorizationAttributeFilter))]
    public IActionResult GetAllPurchases()
    {
        IEnumerable<Purchase> purchases = _purchaseLogic.GetAll();
        IEnumerable<PurchaseResponseModel> purchaseModels = PurchaseModelsMapper.ToModelList(purchases);

        return Ok(purchaseModels);
    }
}

