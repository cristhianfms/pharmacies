using Exceptions;

namespace Domain;
public class Purchase
{
    private string _userEmail;
    private List<PurchaseItem> _items;
    public int Id { get; set; }
    public double TotalPrice { get; set; }
    public DateTime Date { get; set; }
    public int PharmacyId { get; set; }
    public Pharmacy Pharmacy { get; set; }
    public string UserEmail
    {
        get
        {
            return _userEmail;
        }
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ValidationException("user email can not be null");
            }

            _userEmail = value;
        }
    }

    public List<PurchaseItem> Items
    {
        get
        {
            return _items;
        }
        set
        {
            if (value.Count < 1)
            {
                throw new ValidationException("purchase must contain at least one item");
            }
            _items = value;
        }
    }
    
    public override bool Equals(object? obj)
    {
        return obj is Purchase purchase &&
               Id == purchase.Id &&
               TotalPrice == purchase.TotalPrice &&
               Date == purchase.Date &&
               Pharmacy == purchase.Pharmacy &&
               UserEmail == purchase.UserEmail &&
               EqualityComparer<List<PurchaseItem>>.Default.Equals(Items, purchase.Items);
    }
}