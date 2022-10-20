using Domain.Dto;
using IBusinessLogic;

namespace BusinessLogic;

public class ExporterManager : IExporterManager
{
    public List<string> GetAllExporters()
    {
        throw new NotImplementedException();
    }

    public void ExportDrugs(ExportDto exportDto)
    {
        throw new NotImplementedException();
    }

    public List<ExportPropertyDto> GetAllProperties(string exporterName)
    {
        throw new NotImplementedException();
    }
}