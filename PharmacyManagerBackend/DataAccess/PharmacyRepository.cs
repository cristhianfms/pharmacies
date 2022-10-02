using System;
using Domain;
using Exceptions;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class PharmacyRepository : BaseRepository<Pharmacy>, IPharmacyRepository
{
    private readonly DbContext _context;
    private readonly DbSet<Pharmacy> _table;
    public PharmacyRepository(DbContext dbContext) : base(dbContext)
    {
        this._context = dbContext;
        this._table = _context.Set<Pharmacy>();
    }
    public override Pharmacy GetFirst(Func<Pharmacy, bool> expresion)
    {
        IEnumerable<Pharmacy> entities = this._table.
            Include(d => d.Drugs).
            Where(expresion);
        Pharmacy entityToReturn;
        try
        {
            entityToReturn = entities.First(expresion);
        }
        catch(InvalidOperationException)
        {
            throw new ResourceNotFoundException("resource not found");
        }

        return entityToReturn;
    }
}

