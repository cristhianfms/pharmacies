using System;
using System.Collections.Generic;

namespace Domain
{
    public class Pharmacy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Drug> Drugs { get; set; }
        public List <User> Employees { get; set; }
        public User Owner { get; set; }

    }
}
