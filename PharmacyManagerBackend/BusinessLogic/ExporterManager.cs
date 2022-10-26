using System.Reflection;
using Domain;
using Domain.Dtos;
using Exceptions;
using ExporterDomain.Dto;
using ExporterInterface;
using IBusinessLogic;

namespace BusinessLogic;

public class ExporterManager : IExporterManager
{
    private readonly IDrugLogic _drugLogic;
    
    public ExporterManager(IDrugLogic drugLogic)
    {
        _drugLogic = drugLogic;
    }
    
    public List<ExportDto> GetAllExporters()
    {
        return GetExporters()
            .Select(e => new ExportDto()
            {
                Name = e.GetName(),
                Props = e.GetProperties()
            }).ToList();
    }

    public void ExportDrugs(ExportDto exportDto)
    {
        List<IExporter> exporters = GetExporters();
        IExporter? desiredImplementation = null;

        foreach (IExporter exporter in exporters)
        {
            if (exporter.GetName() == exportDto.Name)
            {
                desiredImplementation = exporter;
                break;
            }
        }

        if (desiredImplementation == null)
        {
            throw new ResourceNotFoundException("Exporter doesn't exist");
        }
            

        List<Drug> allDomainDrugs = _drugLogic.GetAll(new QueryDrugDto(){}).ToList();
        List<ExporterDomain.Drug> allDrugsToExport = allDomainDrugs.Select(d =>
            MapDrugDomainToExporterDrug(d)).ToList();
        
        desiredImplementation.ExportDrugs(allDrugsToExport, exportDto.Props);
    }

    private List<IExporter> GetExporters()
    {
        List<IExporter> exporters = new List<IExporter>();
        string exportersPath = "./Exporters";
        string[] filePaths = Directory.GetFiles(exportersPath);

        foreach (string filePath in filePaths)
        {
            if (filePath.EndsWith(".dll"))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                Assembly assembly = Assembly.LoadFile(fileInfo.FullName);

                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IExporter).IsAssignableFrom(type) && !type.IsInterface)
                    {
                        IExporter exporter = (IExporter)Activator.CreateInstance(type);
                        if (exporter != null)
                            exporters.Add(exporter);
                    }
                }
            }
        }

        return exporters;
    }

    private ExporterDomain.Drug MapDrugDomainToExporterDrug(Drug drug)
    {
        return new ExporterDomain.Drug
        {
            Id = drug.Id,
            DrugCode = drug.DrugCode,
            NeedsPrescription = drug.NeedsPrescription,
            Name = drug.DrugInfo.Name,
            PharmacyId = drug.PharmacyId,
            Presentation = drug.DrugInfo.Presentation,
            Price = drug.Price,
            QuantityPerPresentation = drug.DrugInfo.QuantityPerPresentation,
            UnitOfMeasurement = drug.DrugInfo.UnitOfMeasurement,
            Stock = drug.Stock,
            Symptoms = drug.DrugInfo.Symptoms,
        };
    }
}