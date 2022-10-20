namespace Domain.Dto;

public class ExportDto
{
    //Todo: validate fields
    public string ExporterName { get; set; }
    public List<ExportPropertyDto> Props { get; set; }
}