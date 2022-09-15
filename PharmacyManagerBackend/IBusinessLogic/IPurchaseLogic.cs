using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Domain.Dtos;

namespace IBusinessLogic
{
    public interface IPurchaseLogic
    {
        PurchaseDto Create(PurchaseDto purchase);
    }
}
