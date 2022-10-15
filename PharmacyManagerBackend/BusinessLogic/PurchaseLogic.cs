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
        CheckAndBindExistentDrugs(purchase.Items);
        SetPendingState(purchase.Items);
        CheckDrugsStock(purchase.Items);
        double totalPrice = CalculateTotalPrice(purchase.Items);
        
        purchase.TotalPrice = totalPrice;
        purchase.Date = DateTime.Now;
        purchase.Code = GenerateNewInvitationCode();

        return _purchaseRepository.Create(purchase);
    }

    public PurchaseReportDto GetPurchasesReport(QueryPurchaseDto queryPurchaseDto)
    { 
        Pharmacy pharmacyOfCurrentUser = _context.CurrentUser.Pharmacy;
        IEnumerable<Purchase> purchases = _purchaseRepository.GetAll(p => 
            //TODO: Analizar esto de farmacia
            //p.PharmacyId == pharmacyOfCurrentUser.Id &&
            p.Date >= queryPurchaseDto.GetParsedDateFrom() &&
            p.Date <= queryPurchaseDto.GetParsedDateTo());
        
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
    
    private double CalculateTotalPrice(List<PurchaseItem> purchaseItems)
    {
        double totalPrice = 0;
        foreach (var purchaseItem in purchaseItems)
        {
            Pharmacy pharmacy = GetPharmacyByName(purchaseItem.Pharmacy.Name);
            Drug drug = GetDrug(pharmacy, purchaseItem.Drug.DrugCode);
            totalPrice += drug.Price;
        }

        return totalPrice;
    }
    
    private void CheckAndBindExistentDrugs(List<PurchaseItem> purchaseItems)
    {
        foreach (var purchaseItem in purchaseItems)
        {
            Pharmacy pharmacy = GetPharmacyByName(purchaseItem.Pharmacy.Name);
            Drug drug = GetDrug(pharmacy, purchaseItem.Drug.DrugCode);
            purchaseItem.Drug = drug;
        }
    }
    
    private void SetPendingState(List<PurchaseItem> purchaseItems)
    {
        foreach (var purchaseItem in purchaseItems)
        {
            purchaseItem.State = PurchaseState.PENDING;
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
    
    private string GenerateNewInvitationCode()
    {
        Random generator = new Random();
        String code;
        do
        {
            code = generator.Next(0, 1000000).ToString("D6");
        } while (IsExistantCode(code));

        return code;
    }

    private bool IsExistantCode(string code)
    {
        bool invitationExists = true;
        try
        {
            _purchaseRepository.GetFirst(i => i.Code == code);
        }
        catch (ResourceNotFoundException e)
        {
            invitationExists = false;
        }

        return invitationExists;
    }
}