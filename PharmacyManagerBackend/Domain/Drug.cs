using Exceptions;
namespace Domain
{
    public class Drug
    {
        public int Id { get; set; }
        private string drugCode;
        private int stock;
        public string DrugCode
        {
            get { return drugCode; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ValidationException("Drug code can't be empty");

                drugCode = value;

            }
        }
        public double Price { get; set; }
        public int Stock {
            get { return stock; }
            set
            {
                if (value<0)
                    throw new ValidationException("Stock must be greater than 0");

                stock = value;

            }
        }
        public bool NeedsPrescription { get; set; }
        public int DrugInfoId { get; set; }
        public DrugInfo DrugInfo { get; set; }
        public int PharmacyId { get; set; }
        public override bool Equals(object obj)
        {
            return this.DrugCode.Equals(((Drug)obj).DrugCode);
        }
    }
}