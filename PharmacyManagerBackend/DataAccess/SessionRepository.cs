using System;
using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class SessionRepository : BaseRepository<Session>, ISessionRepository
{
    public SessionRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
