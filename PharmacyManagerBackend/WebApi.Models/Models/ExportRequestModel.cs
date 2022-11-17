namespace WebApi.Models;

public class ExportRequestModel
{
    public string Name { get; set; }
    public List<ExportPropertyRequestModel> Props { get; set; }
}