using System;
using Domain;
using Exceptions;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class InvitationRepository : BaseRepository<Invitation>, IInvitationRepository
{
    private readonly DbContext _context;
    private readonly DbSet<Invitation> _table;
    public InvitationRepository(DbContext dbContext) : base(dbContext)
    {
        this._context = dbContext;
        this._table = _context.Set<Invitation>();
    }
    
    public override Invitation GetFirst(Func<Invitation, bool> expresion)
    {
        IEnumerable<Invitation> entities = this._table
            .Include(i => i.Role)
            .Include(i => i.Pharmacy)
            .Where(expresion);
        Invitation entityToReturn;
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
    
    public override IEnumerable<Invitation> GetAll(Func<Invitation, bool> expresion = null)
    {
        IEnumerable<Invitation> entities;

        if (expresion == null)
        {
            entities = _table.
                Include(i => i.Pharmacy);
        }
        else
        {

            entities = _table
                .Include(i => i.Pharmacy)
                .Where(expresion);
        }

        return entities;
    }
}

