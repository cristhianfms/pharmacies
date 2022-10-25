using Domain.Utils;
using Exceptions;

namespace Domain;
public class Purchase
{
    private string _userEmail;
    private List<PurchaseItem> _items;
    public int Id { get; set; }
    public double TotalPrice { get; set; }
    public DateTime Date { get; set; }
    public string Code { get; set; }
    public string UserEmail
    {
        get
        {
            return _userEmail;
        }
        set
        {
            FormatValidator.CheckValidEmailFormat(value);
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
               UserEmail == purchase.UserEmail &&
               Items.SequenceEqual(purchase.Items);
    }
}