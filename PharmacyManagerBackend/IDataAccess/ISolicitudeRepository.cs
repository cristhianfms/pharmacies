﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataAccess
{
    public interface ISolicitudeRepository : IBaseRepository<Solicitude>
    {
        void Update(int solicitudId);
    }
}