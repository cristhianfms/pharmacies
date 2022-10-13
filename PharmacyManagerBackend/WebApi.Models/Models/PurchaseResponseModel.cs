using System;
using System.Collections.Generic;

namespace WebApi.Models;

public class PurchaseResponseModel
{
    public int Id { get; set; }
    public string UserEmail { get; set; }
    public DateTime CreatedDate { get; set; }
    public double Price { get; set; }
    public string Code { get; set; }
    public List<PurchaseItemModel> Items { get; set; }
}
