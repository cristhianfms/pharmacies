using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DrugInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symptoms { get; set; }
        public string Presentation { get; set; }
        public float QuantityPerPresentation { get; set; }
        public string UnitOfMeasurement { get; set; }
        
    }
}
