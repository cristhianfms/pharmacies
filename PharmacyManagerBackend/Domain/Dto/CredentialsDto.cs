using System;
using Exceptions;

namespace Domain.Dtos;

public class CredentialsDto
{
    private string _userName;
    private string _password;

    public string UserName
    {
        get
        {
            return _userName;
        }
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ValidationException("username can't be empty");
            }
            
            _userName = value;
        }
    }

    public string Password
    {
        get
        {
            return _password;
        }
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ValidationException("password can't be empty");
            }
            
            _password = value;
        }
    }
}
