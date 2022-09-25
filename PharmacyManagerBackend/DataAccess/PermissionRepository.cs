using System;
using Domain;
using Domain.AuthDomain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
{
    public PermissionRepository(DbContext dbContext) : base(dbContext)
    {
    }
}

