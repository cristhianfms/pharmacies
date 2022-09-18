using System;
using Domain;
using IDataAccess;

namespace DataAccess
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
    }
}
