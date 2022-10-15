using System.Collections.Generic;
using Domain;

namespace WebApi.Models;

public class PurchaseItemModel
{
    public string DrugCode { get; set; }
    public int Quantity { get; set; }
    public string PharmacyName { get; set; }
    public PurchaseState State { get; set; }
}
