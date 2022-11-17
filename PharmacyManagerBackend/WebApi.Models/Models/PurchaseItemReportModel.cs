using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class PurchaseItemReportModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }
}
