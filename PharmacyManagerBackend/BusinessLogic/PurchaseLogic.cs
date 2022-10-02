using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.Dtos;
using Exceptions;
using IBusinessLogic;
using IDataAccess;
using System.Linq;
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
        foreach (var purchaseItem in purchase.Items)
        {
            Drug drug = GetDrug(pharmacy, purchaseItem.Drug.DrugCode);
            CheckStock(drug, purchaseItem.Quantity);
        }

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
    
    private Drug GetDrug(Pharmacy pharmacy, string drugDrugCode)
    {
        Drug drug;
        try
        {
            drug = _pharmacyLogic.GetDrug(pharmacy.Id, drugDrugCode);
        }
        catch(ResourceNotFoundException)
        {
            throw new ValidationException($"{drugDrugCode} not exist in pharmacy {pharmacy.Name}");
        }

        return drug;
    }
    
    private void CheckStock(Drug drug, int purchaseItemQuantity)
    {
        if (drug.Stock < purchaseItemQuantity)
        {
            throw new ValidationException($"not enough stock for drug {drug.DrugCode}");
        }
    }
}