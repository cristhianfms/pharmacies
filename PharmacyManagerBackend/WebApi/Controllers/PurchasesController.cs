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

    [HttpGet]
    [ServiceFilter(typeof(AuthorizationAttributeFilter))]
    public IActionResult GetPurchasesReport([FromQuery] QueryPurchaseDto queryPurchaseDto)
    {
        PurchaseReportDto purchaseReport = _purchaseLogic.GetPurchasesReport(queryPurchaseDto);
        PurchaseReportModel purchaseReportModel = PurchaseModelsMapper.ToModel(purchaseReport);

        return Ok(purchaseReportModel);
    }

    [HttpGet("{code}")]
    public IActionResult GetPurchaseStatus(string code)
    {
        IEnumerable<PurchaseItemStatusModel> purchaseItemModel = PurchaseModelsMapper.ToModelList(_purchaseLogic.GetPurchaseStatus(code));
    
        return Ok(purchaseItemModel);
    }
}

