using ExporterDomain;
using ExporterDomain.Dto;

namespace ExporterInterface;

public interface IExporter
{
      string GetName();
      List<ExportPropertyDto> GetProperties();
      void ExportDrugs(List<Drug> drugs, List<ExportPropertyDto> props);
}
