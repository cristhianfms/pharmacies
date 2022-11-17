using System.Collections.Generic;
using Domain;

namespace WebApi.Models;

public class PurchaseItemPutModel
{
    public string DrugCode { get; set; }
    public PurchaseState State { get; set; }
}
