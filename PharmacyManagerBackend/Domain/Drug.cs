namespace Domain
{
    public class Drug
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public bool NeedsPrescription { get; set; }
    }
}