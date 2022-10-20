using Domain.Dto;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Utils;
using System.Linq;

namespace WebApi.Controllers;

[ApiController]
[Route("api/drug-exporters")]
public class DrugExporterController : ControllerBase
{
    private readonly IExporterManager _exporterManager;
    
    [HttpGet]
    public IActionResult GetExporters()
    {
        List<ExportResponseModel> exporters = _exporterManager.GetAllExporters()
            .Select(exporter => ExportModelMapper.ToModel(exporter));
        return Ok(exporters);
    }

    [HttpPost("export")]
    public IActionResult ExportDrugs([FromBody] ExportModel exportModel)
    {
        var exportDto = ExportModelMapper.ToEntity(exportModel);
        _exporterManager.ExportDrugs(exportDto);
        return Ok();
    }
}