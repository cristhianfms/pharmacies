using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class PurchaseItemReportDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }

        public override bool Equals(object? obj)
        {
            PurchaseItemReportDto purchaseItemReport = obj as PurchaseItemReportDto;
            return this.Quantity == purchaseItemReport.Quantity && this.Name == purchaseItemReport.Name
                && this.Amount == purchaseItemReport.Amount;
        }
    }
}
