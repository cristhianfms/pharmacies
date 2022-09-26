using Domain;
namespace WebApi.Models;

public class DrugModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symptoms { get; set; }
    public string Presentation { get; set; }
    public float QuantityPerPresentation { get; set; }
    public string UnitOfMeasurement { get; set; }
    public string DrugCode { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public bool NeedsPrescription { get; set; }
    public int PharmacyId { get; set; }
}

