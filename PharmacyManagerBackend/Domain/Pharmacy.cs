using System;
using System.Collections.Generic;

namespace Domain
{
    public class Pharmacy
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Drug> Drugs { get; set; }
    }
}
