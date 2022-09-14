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
        public SessionLogic(ISessionRepository sessionRepository, IUserRepository userRepository)
        {
            this._sessionRepository = sessionRepository;
            this._userRepository = userRepository;
        }

        public virtual TokenDto Create(CredentialsDto credentialsDto)
        {
            credentialsDto.ValidateNotNullCredentials();

            User registeredUser = _userRepository.FindUserByUserName(credentialsDto.UserName);
            checkIfUserIsNotNull(registeredUser);
            checkIfPassowrdIsCorrect(registeredUser, credentialsDto);

            Session userSession = createNewSession(registeredUser);

            TokenDto userToken = new TokenDto()
            {
                Token = userSession.Token
            };

            return userToken;
        }

        private Session createNewSession(User registeredUser)
        {
            Session userSession = _sessionRepository.FindSessionByUserId(registeredUser.Id);
            if (userSession == null)
            {
                Session session = new Session
                {
                    UserId = registeredUser.Id,
                    Token = Guid.NewGuid()
                };
                userSession = _sessionRepository.Create(session);
            }

            return userSession;
        }

        private void checkIfPassowrdIsCorrect(User registeredUser, CredentialsDto credentialsDto)
        {
            if (registeredUser.Password != credentialsDto.Password)
            {
                throw new AuthenticationException("Invalid credentials");
            }
        }

        private void checkIfUserIsNotNull(User registeredUser)
        {
            if (registeredUser == null)
            {
                throw new AuthenticationException("Invalid credentials");
            }
        }
    }
}
