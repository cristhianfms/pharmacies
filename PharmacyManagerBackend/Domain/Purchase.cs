using System;
using System.Collections.Generic;

namespace Domain
{
    public class Purchase
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string UserEmail { get; set; }
        public List<PurchaseItem> Items { get; set; }
    }
}