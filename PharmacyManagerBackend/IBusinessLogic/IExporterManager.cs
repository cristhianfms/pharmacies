using Domain.Dto;

namespace IBusinessLogic
{
    public interface IExporterManager
    {
        List<string> GetAllExporters();
        void ExportDrugs(ExportDto exportDto);
        List<ExportPropertyDto> GetAllProperties(string exporterName);
    }
}

