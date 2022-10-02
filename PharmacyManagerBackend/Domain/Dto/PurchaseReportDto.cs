using System;
using System.Collections.Generic;

namespace Domain.Dtos;

public class PurchaseReportDto
{
    public double TotalPrice { get; set; }
    public IEnumerable<Purchase> Purchases { get; set; }
}
