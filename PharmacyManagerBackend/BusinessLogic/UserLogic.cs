using System;
using System.Collections.Generic;
using Domain;
using Exceptions;
using IBusinessLogic;
using IDataAccess;
using System.Linq;
using Domain.Dtos;

namespace BusinessLogic;

public class UserLogic : IUserLogic
{
    private readonly IUserRepository _userRepository;

    public UserLogic(IUserRepository userRepository)
    {
        this._userRepository = userRepository;
    }

    public virtual User Create(User user)
    {
        User createdUser = _userRepository.Create(user);

        return createdUser;
    }

    public virtual User GetFirst(Func<User, bool> expresion)
    {
        User fetchedUser = _userRepository.GetFirst(expresion);
        return fetchedUser;
    }


}

