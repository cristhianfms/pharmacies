using System.Text.Json;
using ExporterDomain;
using ExporterDomain.Dto;
using ExporterInterface;

namespace JsonExporter;

public class JsonExporterImp : IExporter
{
    public string GetName()
    {
        return "json-exporter";
    }

    public List<ExportPropertyDto> GetProperties()
    {
        return new List<ExportPropertyDto>()
        {
            new ExportPropertyDto()
            {
                Type = "string",
                Key = "FileName",
            }
        };
    }

    public void ExportDrugs(List<Drug> drugs, List<ExportPropertyDto> props)
    {
        string path = props.Find(p => p.Key == "FileName").Value;
        string json = JsonSerializer.Serialize(drugs);
        File.WriteAllText(path, json);
    }
}
