using Domain;
using Exceptions;
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
        private readonly DbContext _context;
        private readonly DbSet<Solicitude> _table;
        public SolicitudeRepository(DbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
            this._table = _context.Set<Solicitude>();
        }

        public override IEnumerable <Solicitude> GetAll(Func<Solicitude, bool> expresion = null)
        {
            IEnumerable<Solicitude> entities = this._table.
            Include(s => s.Items).
            Include(s => s.Employee).
            Where(expresion);
            List <Solicitude> entitiesToReturn;
            try
            {
                entitiesToReturn = entities.Where(expresion).ToList();
            }
            catch (InvalidOperationException)
            {
                throw new ResourceNotFoundException("resource not found");
            }

            return entitiesToReturn;
        }

        public override Solicitude GetFirst(Func<Solicitude, bool> expresion)
        {
            IEnumerable<Solicitude> entities = this._table
                .Include(s => s.Items)
                .Include(s => s.Employee)
                .Where(expresion);
            Solicitude entityToReturn;
            try
            {
                entityToReturn = entities.First(expresion);
            }
            catch (InvalidOperationException)
            {
                throw new ResourceNotFoundException("resource not found");
            }

            return entityToReturn;
        }
    }
}
