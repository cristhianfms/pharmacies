using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos;

public class QuerySolicitudeDto
{
    public string DateFrom { get; set; }
    public string DateTo { get; set; }
    public string DrugCode { get; set; }
    public string State { get; set; }

}

