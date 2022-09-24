using System;
using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class PharmacyRepository : BaseRepository<Pharmacy>, IPharmacyRepository
{
    public PharmacyRepository(DbContext dbContext) : base(dbContext)
    {
    }
}

