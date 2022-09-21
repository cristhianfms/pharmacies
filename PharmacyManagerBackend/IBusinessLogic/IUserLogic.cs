using System;
using Domain;
using Domain.Dtos;

namespace IBusinessLogic;
public interface IUserLogic
{
    User Create(UserDto userDto);
}

