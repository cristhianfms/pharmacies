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
    private DrugLogic _drugLogic;
    private Context _context;

    public PurchaseLogic(IPurchaseRepository purchaseRepository,
        PharmacyLogic pharmacyLogic,
        DrugLogic drugLogic,
        Context currentContext)
    {
        this._purchaseRepository = purchaseRepository;
        this._pharmacyLogic = pharmacyLogic;
        this._drugLogic = drugLogic;
        this._context = currentContext;
    }

    public Purchase Create(Purchase purchase)
    {
        Pharmacy pharmacy = GetPharmacyByName(purchase.Pharmacy.Name);
        CheckAndBindExistentDrugs(pharmacy, purchase.Items);
        CheckDrugsStock(purchase.Items);
        UpdateDrugStock(purchase.Items);
        double totalPrice = CalculateTotalPrice(pharmacy, purchase.Items);

        purchase.Pharmacy = pharmacy;
        purchase.TotalPrice = totalPrice;
        purchase.Date = DateTime.Now;

        return _purchaseRepository.Create(purchase);
    }

    public PurchaseReportDto GetPurchasesReport(QueryPurchaseDto queryPurchaseDto)
    {
        Pharmacy pharmacyOfCurrentUser = _context.CurrentUser.Pharmacy;
        IEnumerable<Purchase> purchases = _purchaseRepository.GetAll(p =>
            p.PharmacyId == pharmacyOfCurrentUser.Id &&
            p.Date >= queryPurchaseDto.GetParsedDateFrom() &&
            p.Date <= queryPurchaseDto.GetParsedDateTo());

        double totalPrice = 0;
        foreach (var purchase in purchases)
        {
            totalPrice += purchase.TotalPrice;
        }

        PurchaseReportDto purchaseReport = new PurchaseReportDto()
        {
            Purchases = purchases,
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

    private void CheckStock(Drug drug, int purchaseItemQuantity)
    {
        if (drug.Stock < purchaseItemQuantity)
        {
            throw new ValidationException($"not enough stock for drug {drug.DrugCode}");
        }
    }

    private void UpdateDrugStock(List<PurchaseItem> purchaseItems)
    {
        foreach (var purchaseItem in purchaseItems)
        {
            Drug drug = purchaseItem.Drug;
            drug.Stock -= purchaseItem.Quantity;
            _drugLogic.Update(drug.Id, drug);
        }
    }

    private void CheckDrugsStock(List<PurchaseItem> purchaseItems)
    {
        foreach (var purchaseItem in purchaseItems)
        {
            CheckStock(purchaseItem.Drug, purchaseItem.Quantity);
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

    private void CheckAndBindExistentDrugs(Pharmacy pharmacy, List<PurchaseItem> purchaseItems)
    {
        foreach (var purchaseItem in purchaseItems)
        {
            Drug drug = GetDrug(pharmacy, purchaseItem.Drug.DrugCode);
            purchaseItem.Drug = drug;
        }
    }

    public virtual void DrugExistsInPurchase(Drug drug)
    {
        IEnumerable<Purchase> purchases = _purchaseRepository.GetAll();

        foreach (Purchase p in purchases)
            if (p.PharmacyId == drug.PharmacyId)
            {
                foreach (PurchaseItem pi in p.Items)
                    if (pi.DrugId == drug.Id)
                    {
                        throw new ValidationException("This drug cannot be deleted" +
                    "because it's part of a purchase");
                    }
            }
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
}