using Domain;
using Domain.Dtos;

namespace IBusinessLogic;

public interface IPurchaseLogic
{
    Purchase Create(Purchase purchase);
    PurchaseReportDto GetPurchasesReport(QueryPurchaseDto queryPurchaseDto);
    Purchase Update(int id, Purchase purchase);
}

