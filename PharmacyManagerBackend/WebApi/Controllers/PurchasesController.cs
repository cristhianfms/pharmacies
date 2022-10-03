using Domain;
using Domain.Dtos;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public IActionResult GetPurchasesReport([FromQuery] QueryPurchaseDto queryPurchaseDto)
    {
        PurchaseReportDto purchaseReport = _purchaseLogic.GetPurchasesReport(queryPurchaseDto);
        PurchaseReportModel purchaseReportModel = PurchaseModelsMapper.ToModel(purchaseReport);

        return Ok(purchaseReportModel);
    }
}

