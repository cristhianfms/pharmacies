namespace WebApi.Models;

public class SolicitudeItemModel
{
    public SolicitudeItemModel()
    {

    }

    public int DrugQuantity { get; set; }
    public string DrugCode { get; set; }
    public override bool Equals(object obj)
    {
        return obj is SolicitudeItemModel solicitudeItemModel &&
            solicitudeItemModel.DrugQuantity == DrugQuantity &&
            solicitudeItemModel.DrugCode == DrugCode;
    }
}

