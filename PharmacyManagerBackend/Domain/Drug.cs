namespace Domain
{
 public class Drug{

    public int Id { get; set; }
    public string DrugCode { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public bool NeedsPrescription { get; set; }
    public DrugInfo DrugInfo { get; set; }

    public override bool Equals(object obj)
    {
     return this.DrugCode.Equals(((Drug)obj).DrugCode);
    }
  }
}