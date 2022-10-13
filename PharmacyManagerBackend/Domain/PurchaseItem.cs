using Exceptions;

namespace Domain;

public class PurchaseItem
{
    private int _quantity;

    public int Id { get; set; }
    public int DrugId { get; set; }
    public Drug Drug { get; set; }
    public int PharmacyId { get; set; }
    public Pharmacy Pharmacy { get; set; }
    public PurchaseState State { get; set; }
    public int Quantity
    {
        get { return _quantity; }
        set
        {
            if (value < 1)
            {
                throw new ValidationException("quantity must be more or equal than 1");
            }

            _quantity = value;
        }
    }
}