using System.Collections.Generic;
using Domain;

namespace WebApi.Models;

public class PurchaseItemModel
{
    public string DrugCode { get; set; }
    public int Quantity { get; set; }
    public string PharmacyName { get; set; }
    public PurchaseState State { get; set; }
    
    public override bool Equals(object obj)
    {
        return obj is PurchaseItemModel item &&
               item.DrugCode == DrugCode &&
               item.Quantity == Quantity &&
               item.PharmacyName == PharmacyName &&
               item.State == State;
    }
}
