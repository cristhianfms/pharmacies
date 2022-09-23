namespace Domain;
public class Pharmacy
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public virtual List<Drug> Drugs { get; set; }
    public virtual List <User> Employees { get; set; }
    public User Owner { get; set; }
}
