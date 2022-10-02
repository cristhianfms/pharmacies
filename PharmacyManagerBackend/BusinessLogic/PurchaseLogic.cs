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
    private DrugLogic _drugLogic;
    private Context _context;

    public PurchaseLogic(IPurchaseRepository purchaseRepository, PharmacyLogic pharmacyLogic, DrugLogic drugLogic)
    {
        this._purchaseRepository = purchaseRepository;
        this._pharmacyLogic = pharmacyLogic;
        this._drugLogic = drugLogic;
    }

    public void SetContext(Context context)
    {
        _context = context;
    }

    public Purchase Create(Purchase purchase)
    {
        Pharmacy pharmacy = GetPharmacyByName(purchase.Pharmacy.Name);
        CheckPurchaseStock(pharmacy, purchase.Items);
        UpdateDrugStock(pharmacy, purchase.Items);
        double totalPrice = CalculateTotalPrice(pharmacy, purchase.Items);

        purchase.TotalPrice = totalPrice;

        return _purchaseRepository.Create(purchase);
    }
    public PurchaseReportDto GetPurchasesReport(QueryPurchaseDto queryPurchaseDto)
    { 
        Pharmacy pharmacyOfCurrentUser = _context.CurrentUser.Pharmacy;

        IEnumerable<Purchase> purchases = _purchaseRepository.GetAll(p => p.PharmacyId == pharmacyOfCurrentUser.Id);
        double totalPrice = 0;
        foreach (var purchase in purchases)
        {
            totalPrice += purchase.TotalPrice;
        }

        PurchaseReportDto purchaseReport = new PurchaseReportDto()
        {
            Purchases =  purchases,
            TotalPrice = totalPrice
        };

        return purchaseReport;
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

    private Drug GetDrug(Pharmacy pharmacy, string drugCode)
    {
        Drug? drug = pharmacy.Drugs.Find(d => d.DrugCode == drugCode);

        if (drug == null)
        {
            throw new ValidationException($"{drugCode} not exist in pharmacy {pharmacy.Name}");
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

    private void UpdateDrugStock(Pharmacy pharmacy, List<PurchaseItem> purchaseItems)
    {
        foreach (var purchaseItem in purchaseItems)
        {
            Drug drug = GetDrug(pharmacy, purchaseItem.Drug.DrugCode);
            drug.Stock -= purchaseItem.Quantity;
            _drugLogic.Update(drug.Id, drug);
        }
    }

    private void CheckPurchaseStock(Pharmacy pharmacy, List<PurchaseItem> purchaseItems)
    {
        foreach (var purchaseItem in purchaseItems)
        {
            Drug drug = GetDrug(pharmacy, purchaseItem.Drug.DrugCode);
            CheckStock(drug, purchaseItem.Quantity);
        }
    }
    
    private double CalculateTotalPrice(Pharmacy pharmacy, List<PurchaseItem> purchaseItems)
    {
        double totalPrice = 0;
        foreach (var purchaseItem in purchaseItems)
        {
            Drug drug = GetDrug(pharmacy, purchaseItem.Drug.DrugCode);
            totalPrice += drug.Price;
        }

        return totalPrice;
    }
}