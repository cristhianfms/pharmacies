namespace WebApi.Models;

public class DrugGetModel
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
    public string PharmacyName { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is DrugGetModel model &&
               Id == model.Id &&
               Name == model.Name &&
               Symptoms == model.Symptoms &&
               Presentation == model.Presentation &&
               QuantityPerPresentation == model.QuantityPerPresentation &&
               UnitOfMeasurement == model.UnitOfMeasurement &&
               DrugCode == model.DrugCode &&
               Price == model.Price &&
               Stock == model.Stock &&
               NeedsPrescription == model.NeedsPrescription &&
               PharmacyId == model.PharmacyId;
    }
}

