using System;
using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class Sessionrepository : BaseRepository<Session>, ISessionRepository
{
    public Sessionrepository(DbContext dbContext) : base(dbContext)
    {
    }
}
