using System;
using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository
{
    public PurchaseRepository(DbContext dbContext) : base(dbContext)
    {
    }
}

