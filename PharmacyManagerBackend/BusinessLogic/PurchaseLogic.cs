using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.Dtos;
using Exceptions;
using IBusinessLogic;
using IDataAccess;
using ValidationException = Exceptions.ValidationException;

namespace BusinessLogic;

public class PurchaseLogic : IPurchaseLogic
{
    private IPurchaseRepository _purchaseRepository;
    private PharmacyLogic _pharmacyLogic;

    public PurchaseLogic(IPurchaseRepository purchaseRepository, PharmacyLogic pharmacyLogic)
    {
        this._purchaseRepository = purchaseRepository;
        this._pharmacyLogic = pharmacyLogic;
    }
    
    public Purchase Create(Purchase purchase)
    {
        Pharmacy pharmacy = GetPharmacyByName(purchase.Pharmacy.Name);
        
        return _purchaseRepository.Create(purchase);
    }
    public PurchaseReportDto GetPurchasesReport(QueryPurchaseDto queryPurchaseDto)
    {
        throw new NotImplementedException();
    }
    
    private Pharmacy GetPharmacyByName(string pharmacyName)
    {
        Pharmacy pharmacy;
        try
        {
            pharmacy = _pharmacyLogic.GetPharmacyByName(pharmacyName);
        }
        catch (ResourceNotFoundException e)
        {
            throw new ValidationException("not existent pharmacy");
        }
        
        return pharmacy;
    }
}