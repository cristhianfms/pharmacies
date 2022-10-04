using System.Collections.Generic;

namespace WebApi.Models;

public class PurchaseRequestModel
{
    public string UserEmail { get; set; }
    public string PharmacyName { get; set; }
    public List<PurchaseItemModel> Items { get; set; }
}
