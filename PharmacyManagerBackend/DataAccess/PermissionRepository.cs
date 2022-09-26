using System;
using Domain;
using Domain.AuthDomain;
using Exceptions;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
{
    private readonly DbContext _context;
    private readonly DbSet<Permission> _table;
    public PermissionRepository(DbContext dbContext) : base(dbContext)
    {
        this._context = dbContext;
        this._table = _context.Set<Permission>();
    }
    
    public override Permission GetFirst(Func<Permission, bool> expresion)
    {
        IEnumerable<Permission> entities = this._table
            .Include(p => p.PermissionRoles)
            .ThenInclude(pr => pr.Role)
            .Where(expresion);
        
        Permission entityToReturn;
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

