using System;
using System.Collections.Generic;
using Domain;
using Exceptions;
using IBusinessLogic;
using IDataAccess;
using System.Linq;
using Domain.Dtos;

namespace BusinessLogic;

public class UserLogic
{
    private readonly IUserRepository _userRepository;
    private readonly InvitationLogic _invitationLogic;

    public UserLogic() { }

    public UserLogic(IUserRepository userRepository, InvitationLogic invitationLogic)
    {
        this._userRepository = userRepository;
        this._invitationLogic = invitationLogic;
    }

    public virtual User Create(User user)
    {
        User createdUser = _userRepository.Create(user);

        return createdUser;
    }



    public virtual User GetUserByUserName(string userName)
    {
        return _userRepository.GetFirst(u => u.UserName == userName);
    }


}

