using Domain;
using System;
using System.Collections.Generic;

namespace WebApi.Models;

public class SolicitudeResponseModel
{
    public int Id { get; set; }
    public State State { get; set; }
    public DateTime Date { get; set; }
    public List<SolicitudeItemModel> SolicitudeItems { get; set; }
    public string EmployeeUserName { get; set; }
    public override bool Equals(object obj)
    {
        return obj is SolicitudeResponseModel solicitudeResponseModel &&
                solicitudeResponseModel.Id == Id &&
                solicitudeResponseModel.State == State &&
                solicitudeResponseModel.Date == Date &&
            solicitudeResponseModel.EmployeeUserName == EmployeeUserName;

    }

}

