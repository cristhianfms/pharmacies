using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class PurchaseItemStatusModel
    {
        public string DrugCode { get; set; }
        public string State { get; set; }

        public override bool Equals(object obj)
        {
            return obj is PurchaseItemStatusModel item &&
                   item.DrugCode == DrugCode &&
                   item.State == State;
        }
    }
}
