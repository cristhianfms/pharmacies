using System;
using Domain.AuthDomain;
using Exceptions;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class SessionRepository : BaseRepository<Session>, ISessionRepository
{
    private readonly DbContext _context;
    private readonly DbSet<Session> _table;
    public SessionRepository(DbContext dbContext) : base(dbContext)
    {
        this._context = dbContext;
        this._table = _context.Set<Session>();
    }
    
    public override Session GetFirst(Func<Session, bool> expresion)
    {
        IEnumerable<Session> entities = this._table
            .Include(i => i.User)
            .ThenInclude(u => u.Role)
            .Include(u => u.User.Pharmacy)
            .Where(expresion);
        Session entityToReturn;
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
