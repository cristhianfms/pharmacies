using Exceptions;

namespace Domain;
public class Purchase
{
    private string _userEmail;
    public int Id { get; set; }
    public double TotalPrice { get; set; }
    public DateTime Date { get; set; }
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
        }
    }
    
    public List<PurchaseItem> Items { get; set; }
}