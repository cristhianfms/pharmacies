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
            .Include(i => i.DrugInfo).Where(expresion).Where(d => d.IsActive == true);
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

        public override IEnumerable<Drug> GetAll(Func<Drug, bool> expresion = null)
        {
            IEnumerable<Drug> entities = this._table
                .Include(i => i.DrugInfo).Where(d => d.IsActive == true);

            if(expresion != null)
                entities = this._table
                .Include(i => i.DrugInfo).Where(expresion);


            return entities;
        }

    }
}
