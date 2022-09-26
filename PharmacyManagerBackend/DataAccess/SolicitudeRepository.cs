using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SolicitudeRepository : BaseRepository<Solicitude>, ISolicitudeRepository
    {
        public SolicitudeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public void Update(int solicitudId)
        {
            throw new NotImplementedException();
        }
    }
}
