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
            }
            catch (FormatException)
            {
                throw new ValidationException("DateTo: invalid format");
            }
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