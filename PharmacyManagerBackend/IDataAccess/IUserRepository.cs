using System;
using Domain;

namespace IDataAccess
{
    public interface IUserRepository: IBaseRepository<User>
    {
        User FindUserByUserName(string userName);
    }
}
