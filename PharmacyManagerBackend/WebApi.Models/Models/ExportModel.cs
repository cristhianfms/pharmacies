namespace WebApi.Models;

public class ExportModel
{
    public string ExporterName { get; set; }
    public List<ExportPropertyRequestModel> Props { get; set; }
}