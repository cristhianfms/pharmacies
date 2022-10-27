namespace WebApi.Models;

public class ExportResponseModel
{
    public string Name { get; set; }
    public List<ExportPropertyResponseModel> Props { get; set; }
}