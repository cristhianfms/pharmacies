using Domain;
namespace WebApi.Models;

public class DrugModel
{
    public int Id { get; set; }
    public string DrugCode { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public bool NeedsPrescription { get; set; }
}

