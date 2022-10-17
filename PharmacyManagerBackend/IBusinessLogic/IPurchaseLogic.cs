using Domain;
using Domain.Dtos;

namespace IBusinessLogic;

public interface IPurchaseLogic
{
    Purchase Create(Purchase purchase);
    PurchaseReportDto GetPurchasesReport(QueryPurchaseDto queryPurchaseDto);
    IEnumerable<PurchaseItem> GetPurchaseStatus(string code);
    Purchase Update(int id, Purchase purchase);
}

