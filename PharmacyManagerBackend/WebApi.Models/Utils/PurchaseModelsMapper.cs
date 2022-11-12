
using Domain;
using Domain.Dtos;

namespace WebApi.Models.Utils;

public static class PurchaseModelsMapper
{
    public static PurchaseResponseModel ToModel(Purchase purchaseDto)
    {
        List<PurchaseItemModel> purchaseItems = purchaseDto.Items.Select(i => ToModel(i)).ToList();
        return new PurchaseResponseModel
        {
            Id = purchaseDto.Id,
            UserEmail = purchaseDto.UserEmail,
            Items = purchaseItems,
            CreatedDate = purchaseDto.Date,
            Price = purchaseDto.TotalPrice,
            Code = purchaseDto.Code,
        };
    }

    private static PurchaseItemModel ToModel(PurchaseItem purchaseItem)
    {
        return new PurchaseItemModel
        {
            Quantity = purchaseItem.Quantity,
            DrugCode = purchaseItem.Drug.DrugCode,
            PharmacyName = purchaseItem.Pharmacy.Name,
            State = purchaseItem.State
        };
    }

    private static PurchaseItemStatusModel ToModel(PurchaseItemStatusDto purchaseItem)
    {
        return new PurchaseItemStatusModel
        {
            DrugCode = purchaseItem.DrugCode,
            State = purchaseItem.State
        };
    }

    private static PurchaseItemReportModel ToModel(PurchaseItemReportDto purchaseItem)
    {
        return new PurchaseItemReportModel
        {
            Name = purchaseItem.Name,
            Quantity = purchaseItem.Quantity,
            Amount = purchaseItem.Amount
        };
    }

    public static PurchaseReportModel ToModel(PurchaseReportDto purchaseReport)
    {
        List<PurchaseItemReportModel> purchaseItemModels = purchaseReport.Purchases.Select(p => ToModel(p)).ToList();
        return new PurchaseReportModel
        {
            TotalPrice = purchaseReport.TotalPrice,
            Purchases = purchaseItemModels
        };
    }

    public static Purchase ToEntity(PurchaseRequestModel purchaseRequestModel)
    {
        List<PurchaseItem> purchaseItems = purchaseRequestModel.Items.Select(i => ToEntity(i)).ToList();
        return new Purchase()
        {
            UserEmail = purchaseRequestModel.UserEmail,
            Items = purchaseItems
        };
    }

    private static PurchaseItem ToEntity(PurchaseItemModel purchaseItemModel)
    {
        return new PurchaseItem
        {
            Quantity = purchaseItemModel.Quantity,
            Drug = new Drug()
            {
                DrugCode = purchaseItemModel.DrugCode
            },
            Pharmacy = new Pharmacy()
            {
                Name = purchaseItemModel.PharmacyName
            }
        };
    }

    public static Purchase ToEntity(PurchasePutModel purchasePutModel)
    {
        List<PurchaseItem> purchaseItems = purchasePutModel.Items.Select(i => ToEntity(i)).ToList();
        return new Purchase()
        {
            Items = purchaseItems
        };
    }

    private static PurchaseItem ToEntity(PurchaseItemPutModel purchaseItemPutModel)
    {
        return new PurchaseItem
        {
            Drug = new Drug()
            {
                DrugCode = purchaseItemPutModel.DrugCode
            },
            State = purchaseItemPutModel.State
        };
    }

    public static IEnumerable<PurchaseItemStatusModel> ToModelList(IEnumerable<PurchaseItemStatusDto> purchaseItems)
    {
        List<PurchaseItemStatusModel> purchaseItemModels = new List<PurchaseItemStatusModel>();

        foreach (var pi in purchaseItems)
            purchaseItemModels.Add(ToModel(pi));

        return purchaseItemModels;
    }
    
    public static IEnumerable<PurchaseResponseModel> ToModelList(IEnumerable<Purchase> purchases)
    {
        List<PurchaseResponseModel> purchaseModels = new List<PurchaseResponseModel>();

        foreach (var p in purchases)
            purchaseModels.Add(ToModel(p));

        return purchaseModels;
    }
}
