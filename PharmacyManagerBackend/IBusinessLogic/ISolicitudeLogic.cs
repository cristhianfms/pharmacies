using Domain;
using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBusinessLogic
{
    public interface ISolicitudeLogic
    {
        Solicitude Create(Solicitude solicitude);
        List<Solicitude> GetSolicitudes(QuerySolicitudeDto querySolicitudeDto);
        
    }
}
