using System;
using Domain;
using IDataAccess;

namespace DataAccess
{
    public class UserRepository : IUserRepository
    {
        public User Create(User entity)
        {
            throw new NotImplementedException();
        }

        public User FindUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
