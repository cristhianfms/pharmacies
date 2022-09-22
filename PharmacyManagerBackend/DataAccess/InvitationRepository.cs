using System;
using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class InvitationRepository : BaseRepository<Invitation>, IInvitationRepository
{
    public InvitationRepository(DbContext dbContext) : base(dbContext)
    {
    }
}

