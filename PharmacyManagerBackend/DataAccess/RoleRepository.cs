using System;
using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(DbContext dbContext) : base(dbContext)
    {
    }
}

