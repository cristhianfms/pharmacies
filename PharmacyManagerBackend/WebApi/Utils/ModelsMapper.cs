using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Dtos;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Utils;

public static class ModelsMapper
{
    public static Pharmacy ToEntity(PharmacyModel pharmacyModel)
    {
        return new Pharmacy
        {
            Name = pharmacyModel.Name,
            Address = pharmacyModel.Address
        };
    }

    public static PharmacyModel ToModel(Pharmacy pharmacy)
    {
        return new PharmacyModel
        {
            Name = pharmacy.Name,
            Address = pharmacy.Address
        };
    }

    public static CredentialsDto ToEntity(CredentialsModel credentialsModel)
    {
        return new CredentialsDto
        {
            UserName = credentialsModel.UserName,
            Password = credentialsModel.Password
        };
    }

    public static TokenModel ToModel(TokenDto tokenDto)
    {
        return new TokenModel
        {
            Token = tokenDto.Token
        };
    }

    public static Solicitude ToEntity(SolicitudeRequestModel solicitudeRequestModel)
    {
        List<SolicitudeItem> solicitudeItems = solicitudeRequestModel.SolicitudeItems.Select(i => ToEntity(i)).ToList();
        return new Solicitude()
        {

            Items = solicitudeItems,
        };
    }

    private static SolicitudeItem ToEntity(SolicitudeItemModel solicitudeItemModel)
    {
        return new SolicitudeItem()
        {
            DrugQuantity = solicitudeItemModel.DrugQuantity,
            DrugCode = solicitudeItemModel.DrugCode,
        };
    }
    public static Solicitude ToEntity(SolicitudeResponseModel solicitudeResponseModel)
    {
        List<SolicitudeItem> solicitudeItems = solicitudeResponseModel.SolicitudeItems.Select(i => ToEntity(i)).ToList();
        return new Solicitude()
        {

            Items = solicitudeItems,
        };
    }

    public static SolicitudeResponseModel ToModel(Solicitude solicitude)
    {
        List<SolicitudeItemModel> solicitudeItems = solicitude.Items.Select(i => ToModel(i)).ToList();
        return new SolicitudeResponseModel()
        {
            Id = solicitude.Id,
            State = solicitude.State,
            Date = solicitude.Date,
            EmployeeUserName = solicitude.Employee.UserName,
            SolicitudeItems = solicitudeItems
        };
    }

    private static SolicitudeItemModel ToModel(SolicitudeItem solicitudeItem)
    {
        return new SolicitudeItemModel()
        {
            DrugQuantity = solicitudeItem.DrugQuantity,
            DrugCode = solicitudeItem.DrugCode
        };
    }
    public static List<SolicitudeResponseModel> ToModelList(List<Solicitude> solicitudes)
    {
        List<SolicitudeResponseModel> solicitudeResponseModels = new List<SolicitudeResponseModel>();
        foreach (Solicitude _solicitude in solicitudes)
        {
            solicitudeResponseModels.Add(ToModel(_solicitude));
        }
        return solicitudeResponseModels;
    }

    public static Solicitude ToEntity(SolicitudePutModel solicitudePutModel)
    {
        return new Solicitude()
        {
            State = Enum.Parse<State>(solicitudePutModel.State)
        };
    }
    public static Drug ToEntity(DrugResponseModel drugModel)
    {
        return new Drug
        {
            Id = drugModel.Id,
            DrugCode = drugModel.DrugCode,
            NeedsPrescription = drugModel.NeedsPrescription,
            Price = drugModel.Price,
            Stock = drugModel.Stock
        };
    }

    public static DrugInfo ToEntityAsociated(DrugResponseModel drugModel)
    {
        return new DrugInfo
        {
            Id = drugModel.Id,
            Name = drugModel.Name,
            Symptoms = drugModel.Symptoms,
            Presentation = drugModel.Presentation,
            QuantityPerPresentation = drugModel.QuantityPerPresentation,
            UnitOfMeasurement = drugModel.UnitOfMeasurement
        };
    }

    public static DrugModel ToModel(Drug drug)
    {
        return new DrugModel
        {
            Id = drug.Id,
            DrugCode = drug.DrugCode,
            NeedsPrescription = drug.NeedsPrescription,
            Price = drug.Price,
            Stock = drug.Stock
        };
    }

    public static DrugInfoModel ToModel(DrugInfo drugInfo)
    {
        return new DrugInfoModel
        {
            Id = drugInfo.Id,
            Name = drugInfo.Name,
            Symptoms = drugInfo.Symptoms,
            Presentation = drugInfo.Presentation,
            QuantityPerPresentation = drugInfo.QuantityPerPresentation,
            UnitOfMeasurement = drugInfo.UnitOfMeasurement
        };
    }
}
