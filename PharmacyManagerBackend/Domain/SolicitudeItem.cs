using System;
using System.Collections.Generic;
using System.Text;

namespace Domain;

public class SolicitudeItem
{
    public int Id { get; set; }
    public int DrugQuantity { get; set; }
    public string DrugCode { get; set; }
    public override bool Equals(object obj)
    {
        return obj is SolicitudeItem solicitudeItem &&
            solicitudeItem.DrugQuantity == DrugQuantity &&
            solicitudeItem.DrugCode == DrugCode;
    }
}

