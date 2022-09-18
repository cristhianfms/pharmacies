using System;
using Domain;
using Domain.Dtos;
using Exceptions;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic
{
    public class SessionLogic: ISessionLogic
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly UserLogic _userLogic;
        public SessionLogic(ISessionRepository sessionRepository, UserLogic userLogic)
        {
            this._sessionRepository = sessionRepository;
            this._userLogic= userLogic;
        }

        public TokenDto Create(CredentialsDto credentialsDto)
        {
            credentialsDto.ValidateNotNullCredentials();

            User registeredUser = _userLogic.GetUserByUserName(credentialsDto.UserName);
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
