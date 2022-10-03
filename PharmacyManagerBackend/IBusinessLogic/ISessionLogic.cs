using System;
using Domain.AuthDomain;
using Domain.Dtos;

namespace IBusinessLogic;

public interface ISessionLogic
{
    TokenDto Create(CredentialsDto credentialsDto);
    Session Get(Guid token);
}

