using Exceptions;
namespace Domain
{
    public class Drug
    {
        public int Id { get; set; }
        private string drugCode;
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
        public int Stock { get; set; }
        public bool NeedsPrescription { get; set; }
        public DrugInfo DrugInfo { get; set; }
        public int DrugInfoId { get; set; }

        public override bool Equals(object obj)
        {
            return this.DrugCode.Equals(((Drug)obj).DrugCode);
        }
    }
}