using System.Collections.Generic;
using System.Linq;
using Domain.Dtos;
using WebApi.Models;

namespace WebApi.Utils;

public static class PurchaseModelsMapper
{
    public static PurchaseDto ToEntity(PurchaseRequestModel purchaseRequestModel)
    {
        List<PurchaseItemDto> purchaseItems = purchaseRequestModel.Items.Select(i => ToEntity(i)).ToList();
        return new PurchaseDto
        {
            UserEmail = purchaseRequestModel.UserEmail,
            Items = purchaseItems
        };
    }

    private static PurchaseItemDto ToEntity(PurchaseItemModel purchaseItemModel)
    {
        return new PurchaseItemDto
        {
            Quantity = purchaseItemModel.Quantity,
            DrugCode = purchaseItemModel.DrugCode
        };
    }


    public static PurchaseResponseModel ToModel(PurchaseDto purchaseDto)
    {
        List<PurchaseItemModel> purchaseItems = purchaseDto.Items.Select(i => ToModel(i)).ToList();
        return new PurchaseResponseModel
        {
            Id = purchaseDto.Id,
            UserEmail = purchaseDto.UserEmail,
            Items = purchaseItems,
            CreatedDate = purchaseDto.CreatedDate,
            Price = purchaseDto.Price
        };
    }

    private static PurchaseItemModel ToModel(PurchaseItemDto purchaseItem)
    {
        return new PurchaseItemModel
        {
            Quantity = purchaseItem.Quantity,
            DrugCode = purchaseItem.DrugCode,
            PharmacyName = purchaseItem.PharmacyName
        };
    }

    public static PurchaseReportModel ToModel(PurchaseReportDto purchaseReport)
    {
        List<PurchaseResponseModel> purchaseModels = purchaseReport.Purchases.Select(p => ToModel(p)).ToList();
        return new PurchaseReportModel
        {
            TotalPrice = purchaseReport.TotalPrice,
            Purchases = purchaseModels
        };
    }
}
