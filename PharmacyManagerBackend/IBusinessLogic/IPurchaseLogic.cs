using Domain;
using Domain.Dtos;

namespace IBusinessLogic;

public interface IPurchaseLogic
{
    Purchase Create(Purchase purchase);
    PurchaseReportDto GetPurchasesReport(QueryPurchaseDto queryPurchaseDto);
    IEnumerable<PurchaseItemStatusDto> GetPurchaseStatus(string purchaseCode);
    Purchase Update(int id, Purchase purchase);
}

