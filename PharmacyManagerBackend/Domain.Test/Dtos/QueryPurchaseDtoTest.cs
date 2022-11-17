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
        string dateTo = "2022-09-30T23:59:59";
        QueryPurchaseDto queryPurchaseDto = new QueryPurchaseDto()
        {
            DateTo = dateTo,
            DateFrom = dateFrom
        };
        
        Assert.AreEqual(DateTime.Parse(dateFrom), queryPurchaseDto.GetParsedDateFrom());
        Assert.AreEqual(DateTime.Parse(dateTo), queryPurchaseDto.GetParsedDateTo());
    }
    
    [TestMethod]
    public void DatesMinAndMaxValueOk()
    {
        QueryPurchaseDto queryPurchaseDto = new QueryPurchaseDto(){ };
        
        Assert.AreEqual(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01), queryPurchaseDto.GetParsedDateFrom());
        Assert.AreEqual(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59), queryPurchaseDto.GetParsedDateTo());
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
    
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void DateToAfterDateFromShouldFail()
    {
        string dateTo = "2022-09-01T00:00:00";
        string dateFrom = "2022-09-30T00:00:00";
        QueryPurchaseDto queryPurchaseDto = new QueryPurchaseDto()
        {
            DateTo = dateTo,
            DateFrom = dateFrom
        };
        
        Assert.AreEqual(DateTime.Parse(dateFrom), queryPurchaseDto.GetParsedDateFrom());
        Assert.AreEqual(DateTime.Parse(dateTo), queryPurchaseDto.GetParsedDateTo());
    }

}