using System;
using Domain;
using Domain.AuthDomain;
using Domain.Dtos;
using Exceptions;
using IAuthLogic;
using IBusinessLogic;
using IDataAccess;

namespace AuthLogic;
public class SessionLogic : ISessionLogic
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IUserLogic _userLogic;
    public SessionLogic(ISessionRepository sessionRepository, IUserLogic userLogic)
    {
        this._sessionRepository = sessionRepository;
        this._userLogic = userLogic;
    }

    public TokenDto Create(CredentialsDto credentialsDto)
    {
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

    public Session Get(Guid token)
    {
        Session session = _sessionRepository.GetFirst(s => token.Equals(s.Token));

        return session;
    }

    private Session createNewSession(User registeredUser)
    {
        Session userSession;
        try
        {
            userSession = _sessionRepository.GetFirst(u => u.Id == registeredUser.Id);
        }
        catch (ResourceNotFoundException)
        {
            {
                Session session = new Session
                {
                    UserId = registeredUser.Id,
                    Token = Guid.NewGuid()
                };
                userSession = _sessionRepository.Create(session);
            }
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

