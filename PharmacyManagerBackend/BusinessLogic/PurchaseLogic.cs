using Domain;
using Domain.Dtos;
using Exceptions;
using IBusinessLogic;
using IDataAccess;
using System;
using System.Linq;

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

    public IEnumerable<PurchaseItemStatusDto> GetPurchaseStatus(string purchaseCode)
    {
        if (!IsExistantCode(purchaseCode))
            throw new ValidationException("The code doesn't belong to a purchase");

        Purchase purchase = _purchaseRepository.GetFirst(p => p.Code == purchaseCode);
        List<PurchaseItemStatusDto> result = new List<PurchaseItemStatusDto>();
        foreach (var pi in purchase.Items)
        {
            PurchaseItemStatusDto itemDto = new PurchaseItemStatusDto
            {
                DrugCode = pi.Drug.DrugCode,
                State = Enum.GetName(typeof(PurchaseState), pi.State)
            };
            result.Add(itemDto);
        }
        return result;
    }


    public PurchaseReportDto GetPurchasesReport(QueryPurchaseDto queryPurchaseDto)
    {
        Pharmacy pharmacyOfCurrentUser = _context.CurrentUser.Pharmacy;
        IEnumerable<Purchase> purchases = _purchaseRepository.GetAll(p =>
            p.Items.Any(i => i.Pharmacy.Id == pharmacyOfCurrentUser.Id) &&
            p.Date >= queryPurchaseDto.GetParsedDateFrom() &&
            p.Date <= queryPurchaseDto.GetParsedDateTo());

        double totalPrice = 0;
        List<PurchaseItemReportDto> purchaseItemReports = new List<PurchaseItemReportDto>();

        foreach (var purchase in purchases)
        {
            List<PurchaseItem> purchaseItems = purchase.Items.FindAll(i => i.PharmacyId == pharmacyOfCurrentUser.Id);
            totalPrice += CalculateTotalPrice(purchaseItems);
            purchaseItemReports = ProcessItemsForReport(purchaseItems, purchaseItemReports);           
        }
        PurchaseReportDto purchaseReport = new PurchaseReportDto()
        {
            Purchases = purchaseItemReports,
            TotalPrice = totalPrice
        };

        return purchaseReport;

    }

    private List<PurchaseItemReportDto> ProcessItemsForReport(List<PurchaseItem> purchaseItems, List<PurchaseItemReportDto> result)
    {
        foreach (var purchaseItem in purchaseItems)
        {
            var name = purchaseItem.Drug.DrugCode + " - " + purchaseItem.Drug.DrugInfo.Name;
            var quantity = purchaseItem.Quantity;
            var unitaryPrice = purchaseItem.Drug.Price;
            var amount = quantity * unitaryPrice;

            PurchaseItemReportDto purchaseItemDto = result.FirstOrDefault(pi => pi.Name== name);

            if (purchaseItemDto == null)
            {
                PurchaseItemReportDto purchaseItemReportDto = new PurchaseItemReportDto()
                {
                    Name = name,
                    Quantity = quantity,
                    Amount = amount
                };
                result.Add(purchaseItemReportDto);
            }
            else
            {
                purchaseItemDto.Quantity += quantity;
                purchaseItemDto.Amount += amount;
            }
        }

        return result;
    }

    public Purchase Update(int id, Purchase purchase)
    {
        Pharmacy pharmacyOfCurrentUser = _context.CurrentUser.Pharmacy;
        Purchase currentPurchase = GetPurchase(id);
        List<PurchaseItem> currentUserPurchaseItems = currentPurchase.Items.FindAll(i => i.PharmacyId == pharmacyOfCurrentUser.Id);

        foreach (var itemToUpdate in purchase.Items)
        {
            PurchaseItem existentItem = TryGetItemFromPharmacyItems(itemToUpdate, currentUserPurchaseItems);
            TryUpdateStatusAndStock(existentItem, itemToUpdate.State);
        }

        _purchaseRepository.Update(currentPurchase);

        return new Purchase()
        {
            Id = id,
            Items = currentPurchase.Items.FindAll(i => i.PharmacyId == pharmacyOfCurrentUser.Id).ToList()
        };
    }

    public Purchase Get(string code)
    {
        return _purchaseRepository.GetFirst(p => p.Code == code);
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
            var quantity = purchaseItem.Quantity;
            var unitaryPrice = purchaseItem.Drug.Price;
            totalPrice += quantity * unitaryPrice;
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
            purchaseItem.Pharmacy = pharmacy;
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
        Drug? drug = pharmacy.Drugs.Find(d => d.DrugCode == drugCode && d.IsActive);

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

    private Purchase GetPurchase(int purchaseId)
    {
        Purchase purchase;
        try
        {
            purchase = _purchaseRepository.GetFirst(p => p.Id == purchaseId);
        }
        catch (ResourceNotFoundException e)
        {
            throw new ResourceNotFoundException("Purchase doesn't exist");
        }

        return purchase;
    }

    private PurchaseItem TryGetItemFromPharmacyItems(PurchaseItem item, List<PurchaseItem> currentPharmacyItems)
    {
        PurchaseItem existentItem = currentPharmacyItems.Find(i => i.Drug.DrugCode == item.Drug.DrugCode);
        if (existentItem == null)
        {
            throw new ValidationException($"{item.Drug.DrugCode} can't be updated");
        }

        return existentItem;
    }

    private void TryUpdateStatusAndStock(PurchaseItem purchaseItem, PurchaseState newState)
    {
        if (purchaseItem.State.Equals(PurchaseState.ACCEPTED) || purchaseItem.State.Equals(PurchaseState.REJECTED))
        {
            throw new ValidationException($"Status of {purchaseItem.Drug.DrugCode} can't be updated");
        }

        if (newState.Equals(PurchaseState.ACCEPTED))
        {
            CheckStock(purchaseItem.Drug, purchaseItem.Quantity);
            Drug drug = purchaseItem.Drug;
            drug.Stock -= purchaseItem.Quantity;
            _drugLogic.Update(drug.Id, drug);
        }

        purchaseItem.State = newState;
    }
}