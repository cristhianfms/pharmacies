using ExporterDomain.Dto;

namespace WebApi.Models.Utils;

public class ExportModelMapper
{
    public static ExportDto ToEntity(ExportRequestModel exportModel)
    {
        List<ExportPropertyDto> props = exportModel.Props.Select(p => ToEntity(p)).ToList();
        return new ExportDto()
        {
            Name = exportModel.Name,
            Props = props
        };
    }

    private static ExportPropertyDto ToEntity(ExportPropertyRequestModel exportPropertyRequestModel)
    {
        return new ExportPropertyDto()
        {
            Type = exportPropertyRequestModel.Type,
            Key = exportPropertyRequestModel.Key,
            Value = exportPropertyRequestModel.Value
        };
    }

    public static ExportResponseModel ToModel(ExportDto exportDto)
    {
        List<ExportPropertyResponseModel> props = exportDto.Props.Select(p => ToModel(p)).ToList();

        return new ExportResponseModel()
        {
            Name = exportDto.Name,
            Props = props
        };
    }

    private static ExportPropertyResponseModel ToModel(ExportPropertyDto exportPropertyDto)
    {
        return new ExportPropertyResponseModel()
        {
            Type = exportPropertyDto.Type,
            Key = exportPropertyDto.Key
        };
    }
}