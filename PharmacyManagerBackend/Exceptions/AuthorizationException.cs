namespace Exceptions;
public class AuthorizationException : Exception
{
    public AuthorizationException(string message)
        : base(message)
    {
    }
    public AuthorizationException(string message, Exception inner)
        : base(message, inner)
    {
    }
}