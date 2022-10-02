using System;
using Microsoft.EntityFrameworkCore;
using IDataAccess;
using Domain;
using Domain.AuthDomain;
using Exceptions;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess
{
    public class DrugRepository : BaseRepository<Drug>, IDrugRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Drug> _table;

        public DrugRepository(DbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
            this._table = _context.Set<Drug>();
        }

        public override Drug GetFirst(Func<Drug, bool> expresion)
        {
            IEnumerable<Drug> entities = this._table
            .Include(i => i.DrugInfo).Where(expresion);
            Drug entityToReturn;
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
