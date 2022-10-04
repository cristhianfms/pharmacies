using System;
using Domain.AuthDomain;
using Domain.Dtos;

namespace IAuthLogic;

public interface ISessionLogic
{
    TokenDto Create(CredentialsDto credentialsDto);
    Session Get(Guid token);
}

