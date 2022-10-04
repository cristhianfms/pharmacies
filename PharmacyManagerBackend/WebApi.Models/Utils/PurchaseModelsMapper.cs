
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
            PharmacyName = purchaseDto.Pharmacy.Name
        };
    }

    private static PurchaseItemModel ToModel(PurchaseItem purchaseItem)
    {
        return new PurchaseItemModel
        {
            Quantity = purchaseItem.Quantity,
            DrugCode = purchaseItem.Drug.DrugCode
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
    
    public static Purchase ToEntity(PurchaseRequestModel purchaseRequestModel)
    {
        List<PurchaseItem> purchaseItems = purchaseRequestModel.Items.Select(i => ToEntity(i)).ToList();
        return new Purchase()
        {
            Pharmacy = new Pharmacy()
            {
                Name = purchaseRequestModel.PharmacyName
            },
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
            }
        };
    }
}
