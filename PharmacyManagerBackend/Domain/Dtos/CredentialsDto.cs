using System;
using Exceptions;

namespace Domain.Dtos
{
    public class CredentialsDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public void ValidateNotNullCredentials()
        {
            if (String.IsNullOrEmpty(this.UserName) || String.IsNullOrEmpty(this.Password))
            {
                throw new ValidationException("username and password can't be null or empty");
            }
        }

    }
}