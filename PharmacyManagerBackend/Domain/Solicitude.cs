using System;
using System.Collections.Generic;
using System.Text;

namespace Domain;

public class Solicitude
{
    public int Id { get; set; }
    public State State { get; set; }
    public DateTime Date { get; set; }
    public List<SolicitudeItem> Items { get; set; }
    public User Employee { get; set; }


    public override bool Equals(object obj)
    {
        return obj is Solicitude solicitude &&
                solicitude.Id == Id &&
                solicitude.State == State &&
                solicitude.Date == Date &&
            solicitude.Employee.UserName == Employee.UserName;

    }
}

