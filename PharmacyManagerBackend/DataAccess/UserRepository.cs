using System;
using Domain;
using IDataAccess;

namespace DataAccess
{
    public class UserRepository : IUserRepository
    {
        public User FindUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
