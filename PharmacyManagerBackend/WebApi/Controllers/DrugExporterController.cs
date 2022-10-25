using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Utils;

namespace WebApi.Controllers;

[ApiController]
[Route("api/drug-exporters")]
public class DrugExporterController : ControllerBase
{
    private readonly IExporterManager _exporterManager;

    public DrugExporterController(IExporterManager exporterManager)
    {
        this._exporterManager = exporterManager;
    }
    
    [HttpGet]
    public IActionResult GetExporters()
    {
        List<ExportResponseModel> exporters = _exporterManager.GetAllExporters()
            .Select(exporter => ExportModelMapper.ToModel(exporter)).ToList();
        return Ok(exporters);
    }

    [HttpPost("export")]
    public IActionResult ExportDrugs([FromBody] ExportRequestModel exportModel)
    {
        var exportDto = ExportModelMapper.ToEntity(exportModel);
        _exporterManager.ExportDrugs(exportDto);
        return Ok();
    }
}