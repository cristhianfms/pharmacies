using System;
using BusinessLogic.Dtos;
using Domain;
using Exceptions;
using IDataAccess;

namespace BusinessLogic
{
    public class SessionLogic
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        public virtual TokenDto Create(CredentialsDto credentialsDto)
        {
            return null;
        }
    }
}
