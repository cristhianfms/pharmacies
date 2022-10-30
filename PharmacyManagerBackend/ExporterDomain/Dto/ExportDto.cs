namespace ExporterDomain.Dto;

public class ExportDto
{
    public string Name { get; set; }
    public List<ExportPropertyDto> Props { get; set; }
}