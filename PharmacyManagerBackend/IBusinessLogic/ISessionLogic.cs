using System;
using Domain.Dtos;

namespace IBusinessLogic
{
    public interface ISessionLogic
    {
        TokenDto Create(CredentialsDto credentialsDto);
    }
}
