using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class PurchaseReportModel
    {
        public double TotalPrice { get; set; }
        public List<PurchaseResponseModel> Purchases { get; set; }
    }
}