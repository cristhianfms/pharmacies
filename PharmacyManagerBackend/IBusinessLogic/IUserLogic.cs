using System;
using Domain;

namespace IBusinessLogic
{
    public interface IUserLogic
    {
        User Create(UserDto userDto);
    }
}
