using Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain;

public class SolicitudeItem
{
    private int _drugQuantity;
    public int Id { get; set; }
    public int DrugQuantity 
    { 
        get
        {
            return _drugQuantity;
        } 
        set
        {
            if (!formatValidation(value))
            {
                throw new ValidationException("Incorrect format");
            } else
            {
                _drugQuantity = value;
            }
        } 
    }
    public string DrugCode { get; set; }
    public override bool Equals(object obj)
    {
        return obj is SolicitudeItem solicitudeItem &&
            solicitudeItem.DrugQuantity == DrugQuantity &&
            solicitudeItem.DrugCode == DrugCode;
    }

    private static bool formatValidation(int value)
    {
        return value > 0 && 
            value.ToString().Length > 0;
    }
}

