using Domain;
using Domain.Dtos;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic;

public class PurchaseLogic : IPurchaseLogic
{
    private IPurchaseRepository _purchaseRepository;

    public PurchaseLogic(IPurchaseRepository purchaseRepository)
    {
        this._purchaseRepository = purchaseRepository;
    }
    
    public Purchase Create(Purchase purchase)
    {
        return _purchaseRepository.Create(purchase);
    }

    public PurchaseReportDto GetPurchasesReport(QueryPurchaseDto queryPurchaseDto)
    {
        throw new NotImplementedException();
    }
}