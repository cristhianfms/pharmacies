using Domain.Dtos;
using Exceptions;

namespace BusinessLogic.Test.Dtos;

[TestClass]
public class QueryPurchaseDtoTest
{
    [TestMethod]
    public void DatesOk()
    {
        string dateFrom = "2022-09-01T00:00:00";
        string dateTo = "2022-09-30T00:00:00";
        QueryPurchaseDto queryPurchaseDto = new QueryPurchaseDto()
        {
            DateTo = dateTo,
            DateFrom = dateFrom
        };
        
        Assert.AreEqual(DateTime.Parse(dateFrom), queryPurchaseDto.GetParsedDateFrom());
        Assert.AreEqual(DateTime.Parse(dateTo), queryPurchaseDto.GetParsedDateTo());
    }
    
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void DateToInvalidFormatShouldFail()
    {
        string dateTo = "date-bad-format";
        QueryPurchaseDto queryPurchaseDto = new QueryPurchaseDto()
        {
            DateTo = dateTo
        };
    }
    
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void DateFromInvalidFormatShouldFail()
    {
        string dateFrom = "date-bad-format";
        QueryPurchaseDto queryPurchaseDto = new QueryPurchaseDto()
        {
            DateFrom = dateFrom
        };
    }
}