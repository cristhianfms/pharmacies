
namespace Domain.Dtos
{
    public class QueryDrugDto
    {
        public string? DrugName { get; set; }
        public bool? WithStock { get; set; }

        public QueryDrugDto()
        {
            WithStock = false;
        }
    }
}
