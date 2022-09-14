using System;
using Domain;

namespace IDataAccess
{
    public interface IUserRepository
    {
        User FindUserByUserName(string userName);
    }
}
