using Exceptions;

namespace Domain;
public class Pharmacy
{
    private string _name;
    public int Id { get; set; }
    public string Name 
    { 
        get 
        {
            return _name;
        }
        set
        {
            if (String.IsNullOrEmpty(value) || value.Length > 50)
            {
                throw new ValidationException("pharmacy name can not be null or gratter than 50 characters");
            }
            _name = value;

        }
    }
    public string? Address { get; set; }
    public virtual List<Drug> Drugs { get; set; }
    public virtual List <User> Employees { get; set; }
    public User Owner { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Pharmacy pharmacy &&
               Id == pharmacy.Id &&
               Name == pharmacy.Name &&
               Address == pharmacy.Address &&
               EqualityComparer<List<Drug>>.Default.Equals(Drugs, pharmacy.Drugs) &&
               EqualityComparer<List<User>>.Default.Equals(Employees, pharmacy.Employees) &&
               EqualityComparer<User>.Default.Equals(Owner, pharmacy.Owner);
    }
}
