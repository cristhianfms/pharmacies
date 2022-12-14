namespace WebApi.Models;

public class DrugRequestModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symptoms { get; set; }
    public string Presentation { get; set; }
    public float QuantityPerPresentation { get; set; }
    public string UnitOfMeasurement { get; set; }
    public string DrugCode { get; set; }
    public double Price { get; set; }
    public bool NeedsPrescription { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is DrugRequestModel model &&
               Id == model.Id &&
               Name == model.Name &&
               Symptoms == model.Symptoms &&
               Presentation == model.Presentation &&
               QuantityPerPresentation == model.QuantityPerPresentation &&
               UnitOfMeasurement == model.UnitOfMeasurement &&
               DrugCode == model.DrugCode &&
               Price == model.Price &&
               NeedsPrescription == model.NeedsPrescription; 
    }
}

