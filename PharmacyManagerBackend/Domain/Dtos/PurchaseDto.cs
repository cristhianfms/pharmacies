using System;
using System.Collections.Generic;

namespace Domain.Dtos
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Price { get; set; }
        public List<PurchaseItemDto> Items { get; set; }
    }
}