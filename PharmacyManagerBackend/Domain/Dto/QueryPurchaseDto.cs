using Exceptions;

namespace Domain.Dtos;

public class QueryPurchaseDto
{
    private DateTime? _dateFrom;
    private DateTime? _dateTo;

    public string? DateFrom
    {
        get { return _dateFrom.ToString(); }
        set
        {
            _dateFrom = DateTime.Parse(value);
        }
    }

    public string? DateTo
    {
        get { return _dateTo.ToString(); }
        set
        {
            _dateTo = DateTime.Parse(value);
        }
        
    }
    
    public DateTime? GetParsedDateFrom()
    {
        return _dateFrom;
    }
    
    public DateTime? GetParsedDateTo()
    {
        return _dateTo;
    }
}