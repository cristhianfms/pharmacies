using Domain.Dto;
using System.Linq;

namespace WebApi.Models.Utils;

public class ExportModelMapper
{
    public static ExportDto ToEntity(ExportModel exportModel)
    {
        List<ExportPropertyDto> props = exportModel.Props.Select(p => ToEntity(p)).ToList();
        return new ExportDto()
        {
            ExporterName = exportModel.ExporterName,
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


    public static ExportPropertyResponseModel ToModel(ExportPropertyDto exportPropertyDto)
    {
        return new ExportPropertyResponseModel()
        {
            Type = exportPropertyDto.Type,
            Key = exportPropertyDto.Key
        };
    }
}