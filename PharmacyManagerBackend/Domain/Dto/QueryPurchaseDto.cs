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
            try
            {
                _dateFrom = DateTime.Parse(value);
                checkTimeLine();
            }
            catch (FormatException)
            {
                throw new ValidationException("DateFrom: invalid format");
            }
        }
    }

    public string? DateTo
    {
        get { return _dateTo.ToString(); }
        set
        {
            try
            {
                _dateTo = DateTime.Parse(value);
                checkTimeLine();
            }
            catch (FormatException)
            {
                throw new ValidationException("DateTo: invalid format");
            }
        }
        
    }
    
    public DateTime? GetParsedDateFrom()
    {
        return _dateFrom != null ? _dateFrom : DateTime.MinValue;
    }
    
    public DateTime? GetParsedDateTo()
    {
        return _dateTo != null ? _dateTo : DateTime.MaxValue;
    }
    
    
    private void checkTimeLine()
    {
        if (_dateFrom != null && _dateTo != null && _dateFrom > _dateTo)
        {
            throw new ValidationException("DateTo must be after DateFrom");
        }
    }
}