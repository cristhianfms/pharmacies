using ExporterDomain.Dto;

namespace IBusinessLogic
{
    public interface IExporterManager
    {
        List<ExportDto> GetAllExporters();
        void ExportDrugs(ExportDto exportDto);
    }
}

