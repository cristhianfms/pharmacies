﻿using System;
using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository
{
    
    private readonly DbContext _context;
    private readonly DbSet<Purchase> _table;
    public PurchaseRepository(DbContext dbContext) : base(dbContext)
    {
        this._context = dbContext;
        this._table = _context.Set<Purchase>();
    }
    
    public virtual IEnumerable<Purchase> GetAll(Func<Purchase, bool> expresion = null)
    {
        IEnumerable<Purchase> entities;

        if (expresion == null)
        {
            entities = _table
                .Include(p => p.Items)
                .ThenInclude(i => i.Drug);
        }
        else
        {

            entities = _table
                .Include(p => p.Items)
                .ThenInclude(i => i.Drug)
                .Where(expresion);
        }

        return entities;
    }
    
}
