namespace WebApi.Models
{
    public class DrugInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symptoms { get; set; }
        public string Presentation { get; set; }
        public float QuantityPerPresentation { get; set; }
        public string UnitOfMeasurement { get; set; }
    }
}
